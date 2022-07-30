using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using System.Security.Claims;
using TradeWatchB.Services.AuthService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _service;
        private readonly TradeWatchDBContext _context;
        public readonly IConfiguration _config;
        public static string email ;
        public AuthController(IAuth service, TradeWatchDBContext context, IConfiguration config)
        {
            _service = service;
            _context = context;
            _config = config;
        }
        [HttpPost]
        [Route("Otp")]
        public async Task<IActionResult> OTP([FromBody] string number)
        {
            try
            {
                var result = await _service.OTP(number);
                if (result != null)
                {
                    return Ok("ok");

                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("OtpCheck")]
        public async Task<IActionResult> OTPChk([FromBody] OtpDto dto)
        {
            try
            {
                var result = await _service.OTPCheck(dto);
                if (result != "error")
                {
                    return Ok("ok");
                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Login Dto)
        {

            var result = await _service.Login(Dto);
            if (result != null)
            {
                var claims = new[]
                 {
                          new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                          new Claim(ClaimTypes.Name, result.Name),

                          new Claim("ID",result.Id.ToString())
                   };

                var key = new SymmetricSecurityKey(Encoding.UTF8.
                    GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(5),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });

            }
            return BadRequest(new { res = result });
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Login Dto)
        {
            try
            {
                var result = await _service.Registration(Dto);
                if (result != null)
                {
                    var claims = new[]
                     {
                          new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                          new Claim(ClaimTypes.Name, result.Name),

                          new Claim("ID",result.Id.ToString())
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.
                        GetBytes(_config.GetSection("AppSettings:Token").Value));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(5),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(token)
                    });

                }
                return BadRequest(new { res = result });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("LoadData")]
        public async Task<IActionResult> LoadDat([FromBody] List<Exchange> dto)
        {
            try
            {
                foreach (var item in dto)
                {
                    Exchange sd = new Exchange();
                    sd.Name = item.Name;
                    sd.Code = item.Code;
                    sd.Country = item.Country;
                    sd.OperatingMic = item.OperatingMic;
                    sd.Currency = item.Currency;

                    _context.Exchanges.Add(sd);
                }

                
                 _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }
        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEM([FromBody] string to)
        {
            try
            {
                var result =_service.SendEmail(to);
                if (result != null)
                {
                    email = to;
                    return Ok("Success");
                }
                else
                {
                    email = "";
                    return BadRequest("Error");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
