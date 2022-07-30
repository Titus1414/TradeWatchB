﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class TradeWatchDBContext : DbContext
    {
        public TradeWatchDBContext()
        {
        }

        public TradeWatchDBContext(DbContextOptions<TradeWatchDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }
        public virtual DbSet<AllCurrency> AllCurrencies { get; set; }
        public virtual DbSet<BserealTimePair> BserealTimePairs { get; set; }
        public virtual DbSet<CcrealTimePair> CcrealTimePairs { get; set; }
        public virtual DbSet<ComentLik> ComentLiks { get; set; }
        public virtual DbSet<CommRealTimePair> CommRealTimePairs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<CryptoNews> CryptoNews { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Exchange> Exchanges { get; set; }
        public virtual DbSet<ForScheduler> ForSchedulers { get; set; }
        public virtual DbSet<ForexNews> ForexNews { get; set; }
        public virtual DbSet<ForexRealTimePair> ForexRealTimePairs { get; set; }
        public virtual DbSet<Fqa> Fqas { get; set; }
        public virtual DbSet<GoogleNews> GoogleNews { get; set; }
        public virtual DbSet<Hash> Hashes { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobParameter> JobParameters { get; set; }
        public virtual DbSet<JobQueue> JobQueues { get; set; }
        public virtual DbSet<LikesAndDisLike> LikesAndDisLikes { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NserealTimePair> NserealTimePairs { get; set; }
        public virtual DbSet<OtpCode> OtpCodes { get; set; }
        public virtual DbSet<PostSurvey> PostSurveys { get; set; }
        public virtual DbSet<RealTimePair> RealTimePairs { get; set; }
        public virtual DbSet<Schema> Schemas { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<SqAnsDoneByUser> SqAnsDoneByUsers { get; set; }
        public virtual DbSet<Sqanswer> Sqanswers { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<StockNews> StockNews { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<WatchList> WatchLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=kitchensmania.com;Database=TradeWatchDB;User Id=TradeWatch;Password=Ic2m8f*0; MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<AllCurrency>(entity =>
            {
                entity.Property(e => e.Country).HasMaxLength(250);

                entity.Property(e => e.Currency).HasMaxLength(150);

                entity.Property(e => e.Iso)
                    .HasMaxLength(50)
                    .HasColumnName("ISO");
            });

            modelBuilder.Entity<BserealTimePair>(entity =>
            {
                entity.ToTable("BSERealTimePairs");

                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.ChangeP).HasColumnName("change_p");

                entity.Property(e => e.Close).HasColumnName("close");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Gmtoffset).HasColumnName("gmtoffset");

                entity.Property(e => e.High).HasColumnName("high");

                entity.Property(e => e.Low).HasColumnName("low");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.PreviousClose).HasColumnName("previousClose");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<CcrealTimePair>(entity =>
            {
                entity.ToTable("CCRealTimePairs");

                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.ChangeP).HasColumnName("change_p");

                entity.Property(e => e.Close).HasColumnName("close");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Gmtoffset).HasColumnName("gmtoffset");

                entity.Property(e => e.High).HasColumnName("high");

                entity.Property(e => e.Low).HasColumnName("low");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.PreviousClose).HasColumnName("previousClose");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<ComentLik>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Cn)
                    .WithMany(p => p.ComentLiks)
                    .HasForeignKey(d => d.CnId)
                    .HasConstraintName("FK_ComentLiks_Comment");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.ComentLiks)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK_ComentLiks_Login");
            });

            modelBuilder.Entity<CommRealTimePair>(entity =>
            {
                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.ChangeP).HasColumnName("change_p");

                entity.Property(e => e.Close).HasColumnName("close");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Gmtoffset).HasColumnName("gmtoffset");

                entity.Property(e => e.High).HasColumnName("high");

                entity.Property(e => e.Low).HasColumnName("low");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.PreviousClose).HasColumnName("previousClose");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Dislike).HasColumnName("dislike");

                entity.Property(e => e.Image).HasMaxLength(550);

                entity.Property(e => e.Likes).HasColumnName("likes");

                entity.Property(e => e.Video).HasMaxLength(550);

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Fid)
                    .HasConstraintName("FK_Comment_FQA");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK_Comment_Login");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key, "CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CryptoNews>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("date");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.NewsUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("news_url");

                entity.Property(e => e.Sentiment)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("sentiment");

                entity.Property(e => e.SourceName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("source_name");

                entity.Property(e => e.Text)
                    .IsUnicode(false)
                    .HasColumnName("text");

                entity.Property(e => e.Tickers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("tickers");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Topics)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("topics");

                entity.Property(e => e.Type)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(150);

                entity.Property(e => e.FkCode).HasMaxLength(50);

                entity.Property(e => e.IconName).HasMaxLength(550);

                entity.HasOne(d => d.Ex)
                    .WithMany(p => p.Currencies)
                    .HasForeignKey(d => d.ExId)
                    .HasConstraintName("FK_Currencies_Exchanges");
            });

            modelBuilder.Entity<Exchange>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperatingMic)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OperatingMIC");
            });

            modelBuilder.Entity<ForScheduler>(entity =>
            {
                entity.ToTable("ForScheduler");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ForexNews>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("date");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.NewsUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("news_url");

                entity.Property(e => e.Sentiment)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("sentiment");

                entity.Property(e => e.SourceName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("source_name");

                entity.Property(e => e.Text)
                    .IsUnicode(false)
                    .HasColumnName("text");

                entity.Property(e => e.Tickers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("tickers");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Topics)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("topics");

                entity.Property(e => e.Type)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<ForexRealTimePair>(entity =>
            {
                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.ChangeP).HasColumnName("change_p");

                entity.Property(e => e.Close).HasColumnName("close");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Gmtoffset).HasColumnName("gmtoffset");

                entity.Property(e => e.High).HasColumnName("high");

                entity.Property(e => e.Low).HasColumnName("low");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.PreviousClose).HasColumnName("previousClose");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<Fqa>(entity =>
            {
                entity.ToTable("FQA", "TradeWatch");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.Fqas)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("FQA_Cid_FK");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.Fqas)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FQA_Uid_FK");
            });

            modelBuilder.Entity<GoogleNews>(entity =>
            {
                entity.ToTable("GoogleNews", "TradeWatch");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.PubDate).HasColumnName("pubDate");

                entity.Property(e => e.Source).HasColumnName("source");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<LikesAndDisLike>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.LikesAndDisLikes)
                    .HasForeignKey(d => d.Fid)
                    .HasConstraintName("FK_LikesAndDisLikes_FQA");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.LikesAndDisLikes)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK_LikesAndDisLikes_Login");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LoginFrom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReffralCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Notify).HasColumnName("notify");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Ccid)
                    .HasConstraintName("Notifications_Ccid_FK");

                entity.HasOne(d => d.Comm)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.CommId)
                    .HasConstraintName("Notifications_CommId_FK");

                entity.HasOne(d => d.Forex)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Forexid)
                    .HasConstraintName("Notifications_Forexid_FK");

                entity.HasOne(d => d.Stk)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Stkid)
                    .HasConstraintName("Notifications_Stkid_FK");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("Notifications_Uid_FK");
            });

            modelBuilder.Entity<NserealTimePair>(entity =>
            {
                entity.ToTable("NSERealTimePairs");

                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.ChangeP).HasColumnName("change_p");

                entity.Property(e => e.Close).HasColumnName("close");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Gmtoffset).HasColumnName("gmtoffset");

                entity.Property(e => e.High).HasColumnName("high");

                entity.Property(e => e.Low).HasColumnName("low");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.PreviousClose).HasColumnName("previousClose");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<OtpCode>(entity =>
            {
                entity.ToTable("OtpCode");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Validity).HasColumnType("datetime");
            });

            modelBuilder.Entity<PostSurvey>(entity =>
            {
                entity.ToTable("PostSurvey");

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UntilActive).HasColumnType("datetime");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.PostSurveys)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("FK_PostSurvey_Currencies");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.PostSurveys)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK_PostSurvey_Login");
            });

            modelBuilder.Entity<RealTimePair>(entity =>
            {
                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.ChangeP).HasColumnName("change_p");

                entity.Property(e => e.Close).HasColumnName("close");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Gmtoffset).HasColumnName("gmtoffset");

                entity.Property(e => e.High).HasColumnName("high");

                entity.Property(e => e.Low).HasColumnName("low");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.PreviousClose).HasColumnName("previousClose");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<SqAnsDoneByUser>(entity =>
            {
                entity.ToTable("SqAnsDoneByUser");

                entity.HasOne(d => d.Ans)
                    .WithMany(p => p.SqAnsDoneByUsers)
                    .HasForeignKey(d => d.AnsId)
                    .HasConstraintName("FK_SqAnsDoneByUser_SQAnswer");

                entity.HasOne(d => d.QidNavigation)
                    .WithMany(p => p.SqAnsDoneByUsers)
                    .HasForeignKey(d => d.Qid)
                    .HasConstraintName("FK_SqAnsDoneByUser_SurveyQuestions");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.SqAnsDoneByUsers)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK_SqAnsDoneByUser_Login");
            });

            modelBuilder.Entity<Sqanswer>(entity =>
            {
                entity.ToTable("SQAnswer");

                entity.Property(e => e.Ans).HasMaxLength(150);

                entity.Property(e => e.Sqid).HasColumnName("SQId");

                entity.HasOne(d => d.Sq)
                    .WithMany(p => p.Sqanswers)
                    .HasForeignKey(d => d.Sqid)
                    .HasConstraintName("FK_SQAnswer_SurveyQuestions");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<StockNews>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasMaxLength(70)
                    .HasColumnName("date");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(550)
                    .HasColumnName("image_url");

                entity.Property(e => e.NewsUrl).HasColumnName("news_url");

                entity.Property(e => e.Sentiment)
                    .HasMaxLength(250)
                    .HasColumnName("sentiment");

                entity.Property(e => e.SourceName).HasColumnName("source_name");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Tickers)
                    .HasMaxLength(250)
                    .HasColumnName("tickers");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Topics)
                    .HasMaxLength(250)
                    .HasColumnName("topics");

                entity.Property(e => e.Type)
                    .HasMaxLength(250)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.Property(e => e.AnsType).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(550);

                entity.Property(e => e.Psid).HasColumnName("PSId");

                entity.Property(e => e.Quest).HasMaxLength(550);

                entity.HasOne(d => d.Ps)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.Psid)
                    .HasConstraintName("FK_SurveyQuestions_PostSurvey");
            });

            modelBuilder.Entity<WatchList>(entity =>
            {
                entity.ToTable("WatchList", "TradeWatch");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.WatchLists)
                    .HasForeignKey(d => d.Ccid)
                    .HasConstraintName("WatchList_Ccid_FK");

                entity.HasOne(d => d.Comm)
                    .WithMany(p => p.WatchLists)
                    .HasForeignKey(d => d.CommId)
                    .HasConstraintName("WatchList_CommId_FK");

                entity.HasOne(d => d.Forex)
                    .WithMany(p => p.WatchLists)
                    .HasForeignKey(d => d.Forexid)
                    .HasConstraintName("WatchList_Forexid_FK");

                entity.HasOne(d => d.Stk)
                    .WithMany(p => p.WatchLists)
                    .HasForeignKey(d => d.Stkid)
                    .HasConstraintName("WatchList_Stkid_FK");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.WatchLists)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("WatchList_Uid_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
