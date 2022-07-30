using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using MailKit.Net.Smtp;
using TradeWatchB.Controllers;

namespace TradeWatchB.Services.AuthService
{
    public class AuthService : IAuth
    {
        public readonly TradeWatchDBContext _context;
        public AuthService(TradeWatchDBContext context)
        {
            _context = context;

        }
        public async Task<string> SendEmail(string to)
        {
            try
            {
                var dt = _context.Logins.Where(x => x.Email == to && x.LoginFrom == "Custom").FirstOrDefault();

                if (dt != null)
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Test Project", "titus.zaman@gmail.com"));
                    message.To.Add(new MailboxAddress("Titus", to));
                    message.Subject = "This Email by Trade Watch App ";
                    message.Body = new TextPart("plain")
                    {
                        Text = "https://kitchensmania.com/Home/PasswordRest?id= " + dt.Id + "&email=" + dt.Email
                    };
                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("titus.zaman@gmail.com", "rwxfyqsnsbgeaxqr");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    return to;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }
        public async Task<Login> Login(Login Dto)
        {

            try
            {
                if (Dto.LoginFrom == "Custom")
                {
                    var login = await _context.Logins.Where(x => x.Email == Dto.Email && x.Password == Dto.Password).FirstOrDefaultAsync();
                    if (login != null)
                    {
                        return login;
                    }
                }
                else
                {
                    var login = await _context.Logins.Where(x => x.UniqueId == Dto.UniqueId).FirstOrDefaultAsync();
                    if (login != null)
                    {
                        return login;
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> OTP(string number)
        {
            try
            {
                //var dt = _context.Logins.Where(a => a.PhoneNo == number).FirstOrDefault();
                //if (dt == null)
                //{
                    Random random = new Random();
                    var randomvalue = random.Next(1000, 9999).ToString();
                    var mesage = randomvalue;
                    var accountSid = "ACbfaf945f09e1c7446a661f212185fe79";
                    var authToken = "8774098652707cfe2b43a0d67854de21";
                    TwilioClient.Init(accountSid, authToken);

                    var splitnum = number.Substring(0, 5);

                    var message = MessageResource.Create(
                   body: mesage,
                   from: new Twilio.Types.PhoneNumber("+17603001473"),
                   to: new Twilio.Types.PhoneNumber(number)
                   );
                    var check = await _context.OtpCodes.AnyAsync(x => x.Number == number);
                    if (check)
                    {
                        var getcodes = await _context.OtpCodes.Where(x => x.Number == number).ToListAsync();
                        _context.OtpCodes.RemoveRange(getcodes);
                        await _context.SaveChangesAsync();
                    }
                    OtpCode code = new OtpCode()
                    {
                        Number = number,
                        Code = randomvalue,
                        Validity = DateTime.Now.AddMinutes(5),
                        IsActive = true,
                        Datetime = DateTime.Now

                    };
                    await _context.OtpCodes.AddAsync(code);
                    await _context.SaveChangesAsync();
                    return message.Sid;
                //}
                //return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> OTPCheck(OtpDto dto)
        {
            try
            {
                var check = await _context.OtpCodes.AnyAsync(x => x.Number == dto.number && x.Code == dto.otpCode);
                if (check)
                {
                    return "success";
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Login> Registration(Login user)
        {
            try
            {

                if (user.Id > 0)
                {

                }
                else
                {
                    if (user.LoginFrom == "Custom")
                    {
                        var sds = _context.Logins.Where(a => a.Email == user.Email).FirstOrDefault();
                        if (sds == null)
                        {
                            Login sd = new Login();

                            sd.Name = user.Name;
                            sd.LoginFrom = user.LoginFrom;
                            sd.PhoneNo = user.PhoneNo;
                            sd.Password = user.Password;
                            sd.Email = user.Email;
                            sd.IsActive = true;
                            sd.ReffralCode = user.ReffralCode;
                            sd.UniqueId = user.UniqueId;
                            sd.Country = user.Country;
                            await _context.Logins.AddAsync(sd);

                            await _context.SaveChangesAsync();
                            var userd = _context.Logins.Max(q => q.Id);
                            var userdata = _context.Logins.Where(a => a.Id == userd).FirstOrDefaultAsync();
                            return await userdata;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var sde = _context.Logins.Where(a => a.UniqueId == user.UniqueId).FirstOrDefault();
                        if (sde == null)
                        {
                            Login sd = new Login();

                            sd.Name = user.Name;
                            sd.LoginFrom = user.LoginFrom;
                            sd.Email = user.Email;
                            sd.IsActive = true;
                            sd.UniqueId = user.UniqueId;
                            sd.Image = user.Image;
                            await _context.Logins.AddAsync(sd);
                            await _context.SaveChangesAsync();
                            var userd = _context.Logins.Where(q => q.UniqueId == user.UniqueId).FirstOrDefault();
                            return userd;
                        }
                        else
                        {
                            var userd = _context.Logins.Where(q => q.UniqueId == user.UniqueId).FirstOrDefault();
                            return userd;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> SetPass(passResetDto dto)
        {
            var sd = _context.Logins.Where(a => a.Email == dto.email && a.LoginFrom == "Custom").FirstOrDefault();
            if (sd != null)
            {
                sd.Password = dto.cpass;


                _context.Logins.Update(sd);
                _context.SaveChanges();
                return "Success";
            }
            else
            {
                return "Errror";
            }

        }
    }
}
