using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        public readonly TradeWatchDBContext _context;
        public static IWebHostEnvironment _environment;
        public ProfileService(TradeWatchDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<string> EditProfile(ProfileDto dto)
        {
            try
            {
                var filename1 = "";
                Random rnd = new Random();
                var rn = rnd.Next(111, 999);
                var loginData = await _context.Logins.FindAsync(dto.Id);
                loginData.Name = dto.Name;
                loginData.Email = dto.Email;
                loginData.PhoneNo = dto.Phone;
                _context.Logins.Update(loginData);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public async Task<Login> GetProfile(int Id)
        {
            var result = await _context.Logins.FindAsync(Id);
            return result;
        }
        public async Task<string> UploadFile(int id, string baseURL, FileUpload dto)
        {
            var filename1 = "";
            Random rnd = new Random();
            var rn = rnd.Next(111, 999);
            var loginData = await _context.Logins.FindAsync(id);
            if (dto.Image != null)
            {
                var ImagePath1 = rn + RemoveWhitespace(dto.Image.FileName);
                var pathh = "";
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\images\\Profile\\" + ImagePath1))
                {
                    dto.Image.CopyTo(fileStream);
                    pathh = Path.Combine(_environment.WebRootPath, "/images/Profile/" + ImagePath1);
                    filename1 = ImagePath1;
                    fileStream.Flush();
                }

                loginData.Image = baseURL + pathh;
                _context.Logins.Update(loginData);
                _context.SaveChanges();
                return "Success";
            }
            return "Erorr";
        }
    }
}
