using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.ProfileService
{
    public interface IProfileService
    {
        Task<string> EditProfile(ProfileDto dto);
        Task<Login> GetProfile(int Id);
        Task<string> UploadFile(int id, string baseURL, FileUpload dto);
    }
}
