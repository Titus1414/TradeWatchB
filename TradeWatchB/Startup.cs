using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Services.AuthService;
using TradeWatchB.Services.EodService;
using TradeWatchB.Services.News;
using TradeWatchB.Services.ProfileService;
using TradeWatchB.Services.SurveyQuest;
using TradeWatchB.Services.SurveyService;
using TradeWatchB.Services.WatchListService;
using TradeWatchB.Services.YouTube;

namespace TradeWatchB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddDbContext<TradeWatchDBContext>(options =>
                              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddScoped<IAuth, AuthService>();
            services.AddScoped<IEodService, EodService>();
            services.AddScoped<INewsService, NewsServices>();
            services.AddScoped<IYouTubeSearch, YouTubeSearch>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IWatchListService, WatchListService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISurveyQuest, SurveyQuest>();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.AddMvc();
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/signalServer"))) // for me my hub endpoint is ConnectionHub
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromDays(2);
            });
            // hangfire service  start
            services.AddHangfire(configuration => configuration
   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
   .UseSimpleAssemblyNameTypeSerializer()
   .UseRecommendedSerializerSettings()
   .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
   {
       CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
       SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
       QueuePollInterval = TimeSpan.Zero,
       UseRecommendedIsolationLevel = true,
       DisableGlobalLocks = true
   }));
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager jobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            jobManager.AddOrUpdate(
               "Run After 59 Mints",
               () => serviceProvider.GetService<IEodService>().GetRealTimePairs(),
                  "55/10 * * * *"
               );
            jobManager.AddOrUpdate(
               "Run After 59 Mints",
               () => serviceProvider.GetService<IWatchListService>().SendNotify(),
                  "20/10 * * * *"
               );
            jobManager.AddOrUpdate(
               "Run After 59 Mints",
               () => serviceProvider.GetService<IWatchListService>().SendFavNotify(),
                  "59/59 * * * *"
               );

            //jobManager.AddOrUpdate(
            //   "Run Each 2pm",
            //   () => serviceProvider.GetService<INewsService>().googleNews(),
            //      "0 14 * * *"
            //   );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
