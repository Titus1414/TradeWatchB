using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;
using TradeWatchB.Services.ProfileService;

namespace TradeWatchB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly TradeWatchDBContext _context;
        private readonly IProfileService _profileService;
        public ProfileController(TradeWatchDBContext context, IProfileService profileService)
        {
            _context = context;
            _profileService = profileService;
        }
        [HttpGet]
        [Route("GetProfile")]
        public async Task<IActionResult> GetProfileData()
        {

            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    var result = await _profileService.GetProfile(Convert.ToInt32(name));
                    return Ok(new { res = result });
                }
                return Unauthorized();
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("SaveProfile")]
        public async Task<IActionResult> SaveProfile([FromBody] ProfileDto dto)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                    if (name != null)
                    {
                        dto.Id = Convert.ToInt32(name);
                        var result = await _profileService.EditProfile(dto);
                        return Ok(new { res = result });
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadFile([FromForm] FileUpload dto)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                    if (name != null)
                    {
                        string baseURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                        int id = Convert.ToInt32(name);
                        var result = await _profileService.UploadFile(id,baseURL,dto);
                        return Ok(new { res = result });
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
