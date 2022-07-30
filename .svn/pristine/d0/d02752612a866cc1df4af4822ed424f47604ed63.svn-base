using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.AuthService
{
    public interface IAuth
    {
        Task<Login> Registration(Login user);
        Task<Login> Login(Login Dto);
        Task<string> OTP(string number);
        Task<string> OTPCheck(OtpDto dto);
        Task<string> SendEmail(string to);
        Task<string> SetPass(passResetDto dto);
    }
}
