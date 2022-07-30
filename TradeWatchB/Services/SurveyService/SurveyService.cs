using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        public readonly TradeWatchDBContext _context;
        public static IWebHostEnvironment _environment;
        private string serverKey = "AAAAn4VGTSw:APA91bE_Ze6Sh1i_NUi0Qr7vzhaXSz9ixFbGF9qiE6FzXKwb8VfrswfsE7SzZs7d62OTyxMr9Y6EsVIjCR6C22YieUh1EqRxaq4WRmxahlYPAx0ip-uNbdLQcUYshwLljkUR60Q8VSzV";
        private string senderId = "685135777068";
        private string webAddr = "https://fcm.googleapis.com/fcm/send";
        public SurveyService(TradeWatchDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public async Task<string> PostQuesttion(int uid, string baseURL, ForumSurveyDto dto)
        {

            IList<string> imgs = new List<string>();
            string joined = "";
            if (dto.Files != null)
            {
                foreach (var item in dto.Files)
                {
                    var filename1 = "";
                    Random rnd = new Random();
                    var rn = rnd.Next(111, 999);

                    if (!string.IsNullOrEmpty(item.FileName))
                    {
                        var ImagePath1 = rn + RemoveWhitespace(item.FileName.Replace(",", ""));
                        var pathh = "";
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\images\\Profile\\" + ImagePath1))
                        {
                            item.CopyTo(fileStream);
                            pathh = Path.Combine(_environment.WebRootPath, "/images/Profile/" + ImagePath1);
                            filename1 = ImagePath1;
                            fileStream.Flush();
                        }
                        string sd = baseURL + pathh;
                        imgs.Add(sd);
                    }
                }
                joined = string.Join(",", imgs);

                Fqa fqa = new Fqa();
                fqa.Files = joined;
                fqa.Title = dto.Title;
                fqa.Cid = dto.Cid;
                fqa.Description = dto.Description;
                fqa.Uid = uid;
                fqa.Date = DateTime.Now;
                fqa.IsActive = true;
                fqa.Likes = 0;
                fqa.DisLikes = 0;
                fqa.Share = 0;
                fqa.Comnts = 0;
                fqa.Views = 0;
                fqa.IsLike = 0;
                fqa.Date = DateTime.Now;
                _context.Fqas.Add(fqa);
                _context.SaveChanges();
            }
            else
            {
                Fqa fqa = new Fqa();
                fqa.Files = joined;
                fqa.Title = dto.Title;
                fqa.Cid = dto.Cid;
                fqa.Description = dto.Description;
                fqa.Uid = uid;
                fqa.Date = DateTime.Now;
                fqa.Likes = 0;
                fqa.DisLikes = 0;
                fqa.Share = 0;
                fqa.Comnts = 0;
                fqa.Views = 0;
                fqa.IsActive = true;
                fqa.IsLike = 0;
                _context.Fqas.Add(fqa);
                _context.SaveChanges();
            }

            return "Success";
        }
        public async Task<List<SurveyGetDto>> SurveyGetDto(SurveyForGetDto dto)
        {
            List<SurveyGetDto> we = new List<SurveyGetDto>();
            var see = _context.LikesAndDisLikes.Where(a => a.Uid == 1).ToList();
            if (dto.topics == "Latest")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId && (t1.Date <= DateTime.Now && t1.Date >= DateTime.Now.AddDays(-3))
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "MyQuest")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId && t2.Id == dto.Uid
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "Popular")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId
                         orderby t1.Likes descending, t1.Comnts descending
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? "",/* Comnts = t3.Comnts ?? "",*/ Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "Liked")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId
                         orderby t1.Likes descending
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "Disliked")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId
                         orderby t1.DisLikes descending
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? "", /*Comnts = t3.Comnts ?? "",*/ Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "Comments")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId
                         orderby t1.Comnts descending
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "Unanswer")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId && t1.Comnts == 0
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "MostReplies")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId
                         orderby t1.Comnts descending
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "Shared")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId
                         orderby t1.Share descending
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }
            else if (dto.topics == "MyResponded")
            {
                var dt = from t1 in _context.Fqas
                         join t2 in _context.Logins on t1.Uid equals t2.Id
                         //join t3 in _context.Comments on t1.Id equals t3.Fid into g
                         //from t3 in g.DefaultIfEmpty()
                         join t4 in _context.LikesAndDisLikes.Where(a => a.Uid == dto.Uid) on t1.Id equals t4.Fid into b
                         from t4 in b.DefaultIfEmpty()
                         where t1.Cid == dto.PairId && t2.Id == dto.Uid
                         orderby t1.IsLike descending, t1.Comnts descending
                         select new { t1.Date, t1.Id, Title = t1.Title ?? "", Description = t1.Description ?? "", Files = t1.Files ?? "", Name = t2.Name ?? "", Image = t2.Image ?? ""/*, Comnts = t3.Comnts ?? ""*/, Likes = t1.Likes ?? 0, DisLike = t1.DisLikes ?? 0, CountComnt = t1.Comnts ?? 0, CountView = t1.Views ?? 0, CountShare = t1.Share ?? 0, IsLike = t4.Likes ?? 0 };
                dt.ToList().Take(100);
                foreach (var item in dt)
                {
                    SurveyGetDto sd = new SurveyGetDto();
                    //sd.cmnt = item.Comnts;
                    sd.Desc = item.Description;
                    sd.CountDislike = Convert.ToInt32(item.DisLike);
                    sd.imgs = item.Files;
                    sd.CountLike = Convert.ToInt32(item.Likes);
                    sd.Name = item.Name;
                    sd.Title = item.Title;
                    sd.userImg = item.Image;
                    sd.CountShare = Convert.ToInt32(item.CountShare);
                    sd.CountComnt = Convert.ToInt32(item.CountComnt);
                    sd.CountView = Convert.ToInt32(item.CountView);
                    sd.IsLike = item.IsLike;
                    sd.Id = item.Id;
                    sd.Date = item.Date;
                    we.Add(sd);
                }
            }

            return we;
        }
        public async Task<List<CommentDto>> Getcomments(ComentUserDto dto)
        {
            //var dt = _context.Comments.Where(a => a.Fid == dto.Fid).OrderBy(a => a.Date).ToList();
            var dt = from t1 in _context.Comments
                     join t2 in _context.ComentLiks.Where(a => a.Uid == dto.Uid) on t1.Id equals t2.CnId into g
                     from t2 in g.DefaultIfEmpty()
                     where t1.Fid == dto.Fid
                     orderby t1.Date
                     select new { t1.Id, t1.Image, t1.Likes, t1.Uid, t1.Video, t1.Fid, t1.Dislike, t1.Date, t1.Comnts, IsLike = t2.Likes ?? 0 };
            dt.ToList().Take(100);

            if (dto.fltr == "MostLike")
            {
                dt = from t1 in _context.Comments
                     join t2 in _context.ComentLiks.Where(a => a.Uid == dto.Uid) on t1.Id equals t2.CnId into g
                     from t2 in g.DefaultIfEmpty()
                     where t1.Fid == dto.Fid
                     orderby t1.Likes descending
                     select new { t1.Id, t1.Image, t1.Likes, t1.Uid, t1.Video, t1.Fid, t1.Dislike, t1.Date, t1.Comnts, IsLike = t2.Likes ?? 0 };
                dt.ToList().Take(100);
                //dt = _context.Comments.Where(a => a.Fid == dto.Fid).OrderByDescending(a => a.Likes).Take(100).ToList();
            }
            List<CommentDto> se = new List<CommentDto>();
            foreach (var item in dt)
            {
                var user = _context.Logins.Where(a => a.Id == item.Uid).FirstOrDefault();

                CommentDto sd = new CommentDto();
                sd.userName = user.Name;
                sd.userImage = user.Image;
                sd.Id = item.Id;
                sd.comment = item.Comnts;
                sd.dislike = Convert.ToInt32(item.Dislike);
                sd.image = item.Image;
                sd.Islike = Convert.ToInt32(item.IsLike);
                sd.like = Convert.ToInt32(item.Likes);
                sd.Video = item.Video;
                sd.Date = item.Date;
                se.Add(sd);
            }

            return se;
        }
        public async Task<string> Likes(ComentUserDto dto)
        {

            var fqaDt = _context.Fqas.Where(a => a.Id == dto.Fid).FirstOrDefault();
            if (dto.IsLike == 1)
            {
                fqaDt.Likes = fqaDt.Likes + dto.IsLike;
            }
            else if (dto.IsLike == 2)
            {

                if (fqaDt.Likes > 0)
                {
                    fqaDt.Likes = fqaDt.Likes - 1;
                }
                else if (fqaDt.DisLikes > 0)
                {
                    fqaDt.DisLikes = fqaDt.DisLikes + 1;
                }
            }
            else if (dto.IsLike == 0)
            {
                if (fqaDt.Likes > 0)
                {
                    fqaDt.Likes = fqaDt.Likes - 1;
                }
                else if (fqaDt.DisLikes > 0)
                {
                    fqaDt.DisLikes = fqaDt.DisLikes - 1;
                }
            }
            if (fqaDt.Uid == dto.Uid)
            {
                fqaDt.IsLike = dto.IsLike;
            }
            else
            {
                fqaDt.IsLike = dto.IsLike;
            }

            _context.Fqas.Update(fqaDt);
            _context.SaveChanges();
            var dt = _context.LikesAndDisLikes.Where(a => a.Fid == dto.Fid && a.Uid == dto.Uid).FirstOrDefault();
            if (dt != null)
            {
                dt.Likes = dto.IsLike;
                _context.LikesAndDisLikes.Update(dt);
                _context.SaveChanges();
            }
            else
            {

                LikesAndDisLike sd = new LikesAndDisLike();
                sd.Likes = dto.IsLike;
                sd.Uid = dto.Uid;
                sd.Fid = dto.Fid;
                sd.Date = DateTime.Now;
                _context.LikesAndDisLikes.Add(sd);
                _context.SaveChanges();
            }
            var user = _context.Logins.Where(a => a.Id == dto.Uid).FirstOrDefault();

            if (dto.IsLike == 1)
            {
                var res = SendNotification("/topics/" + fqaDt.Uid, "Liked by " + user.Name, "Like your post by " + user.Name);
            }
            else if (dto.IsLike == 2)
            {
                var res = SendNotification("/topics/" + fqaDt.Uid, "Dislike by " + user.Name, "Like your post by " + user.Name);
            }
            //else if (dto.IsLike == 0)
            //{
            //    var res = SendNotification("/topics/" + fqaDt.Uid, "Unlike by " + user.Name, "Like your post by " + user.Name );
            //}

            return "Success";
        }
        public async Task<string> PostComent(int uid, string baseURL, ComentPostDto dto)
        {
            try
            {

                var filename1 = "";
                Random rnd = new Random();
                var rn = rnd.Next(111, 999);
                var imgPth = "";
                var vidPth = "";
                if (dto.image != null)
                {
                    var ImagePath1 = rn + RemoveWhitespace(dto.image.FileName.Replace(",", ""));
                    var pathh = "";
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\images\\Profile\\" + ImagePath1))
                    {
                        dto.image.CopyTo(fileStream);
                        pathh = Path.Combine(_environment.WebRootPath, "/images/Profile/" + ImagePath1);
                        filename1 = ImagePath1;
                        fileStream.Flush();
                    }
                    imgPth = baseURL + pathh;
                }
                if (dto.video != null)
                {
                    var ImagePath1 = rn + RemoveWhitespace(dto.video.FileName.Replace(",", ""));
                    var pathh = "";
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\images\\Profile\\" + ImagePath1))
                    {
                        dto.video.CopyTo(fileStream);
                        pathh = Path.Combine(_environment.WebRootPath, "/images/Profile/" + ImagePath1);
                        filename1 = ImagePath1;
                        fileStream.Flush();
                    }
                    vidPth = baseURL + pathh;
                }

                var dt = _context.Fqas.Where(a => a.Id == dto.Fid).FirstOrDefault();
                dt.Comnts = dt.Comnts + 1;
                _context.Fqas.Update(dt);
                _context.SaveChanges();
                Comment sd = new Comment();
                sd.Fid = dto.Fid;
                sd.Uid = uid;
                sd.Comnts = dto.coment;
                sd.Image = imgPth;
                sd.Video = vidPth;
                sd.IsLike = 0;
                sd.Likes = 0;
                sd.Dislike = 0;
                sd.Date = DateTime.Now;
                _context.Comments.Add(sd);
                _context.SaveChanges();
                
                var user = _context.Logins.Where(a => a.Id == uid).FirstOrDefault();

                var res = SendNotification("/topics/" + dt.Uid, "Comment by " + user.Name, "Comment at your post by " + user.Name);
                return "Success";

            }
            catch (Exception ex)
            {

                throw;
            }
            return "Error";
        }
        public async Task<string> ComentLike(int uid, CommentLikePost dto)
        {
            try
            {
                var comnDt = _context.Comments.Where(a => a.Id == dto.Cnid).FirstOrDefault();
                if (dto.IsLike == 1)
                {
                    comnDt.Likes = comnDt.Likes + 1;
                    if (comnDt.Uid == uid)
                    {
                        comnDt.IsLike = dto.IsLike;
                    }
                    else
                    {
                        comnDt.IsLike = dto.IsLike;
                    }
                }
                else if (dto.IsLike == 2)
                {
                    comnDt.Dislike = comnDt.Dislike + 1;
                    if (comnDt.Uid == uid)
                    {
                        comnDt.IsLike = dto.IsLike;
                    }
                    else
                    {
                        comnDt.IsLike = dto.IsLike;
                    }
                }
                _context.Comments.Update(comnDt);
                _context.SaveChanges();

                var dt = _context.ComentLiks.Where(a => a.Uid == uid && a.CnId == dto.Cnid).FirstOrDefault();
                if (dt != null)
                {
                    dt.Likes = dto.IsLike;
                    _context.ComentLiks.Update(dt);
                }
                else
                {

                    ComentLik sd = new ComentLik();
                    sd.CnId = dto.Cnid;
                    sd.Uid = uid;
                    sd.Likes = dto.IsLike;
                    _context.ComentLiks.Add(sd);
                }

                _context.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                throw;
            }
            return "Error";
        }
        public async Task<string> ViewForumPost(ViewForumDto dto)
        {
            var dt = _context.Fqas.Where(a => a.Id == dto.Fid).FirstOrDefault();
            dt.Views = dt.Views + 1;
            _context.Fqas.Update(dt);
            _context.SaveChanges();
            return "Success";
        }
        public async Task<string> SendNotification(string DeviceToken, string title, string msg)
        {
            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";

            var payload = new
            {
                to = DeviceToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = msg,
                    title = title,
                }
                //,
                //data = new
                //{
                //    tid = tid
                //}
            };

            await using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
