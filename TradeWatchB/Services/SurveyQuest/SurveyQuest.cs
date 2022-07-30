using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.SurveyQuest
{
    public class SurveyQuest : ISurveyQuest
    {
        public readonly TradeWatchDBContext _context;
        public static IWebHostEnvironment _environment;
        private string serverKey = "AAAAn4VGTSw:APA91bE_Ze6Sh1i_NUi0Qr7vzhaXSz9ixFbGF9qiE6FzXKwb8VfrswfsE7SzZs7d62OTyxMr9Y6EsVIjCR6C22YieUh1EqRxaq4WRmxahlYPAx0ip-uNbdLQcUYshwLljkUR60Q8VSzV";
        private string senderId = "685135777068";
        private string webAddr = "https://fcm.googleapis.com/fcm/send";
        public SurveyQuest(TradeWatchDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public async Task<string> PostSurveyQuesy(int uid, string baseURL, PostSurveyQuestDto dto)
        {
            List<IFormFile> sd = new List<IFormFile>();
            int indx = 0;
            if (!string.IsNullOrEmpty(dto.Title))
            {
                PostSurvey post = new PostSurvey();
                post.Uid = uid;
                post.Title = dto.Title;
                post.UntilActive = DateTime.Now.AddHours(dto.ActiveTill);
                post.IsActive = true;
                post.Cid = dto.Cid;
                post.QuestionCount = dto.Questions.Count;
                post.CompleteCount = 0;
                _context.PostSurveys.Add(post);
                _context.SaveChanges();
            }
            if (dto.Images != null)
            {
                foreach (var item in dto.Images)
                {
                    sd.Add(item);
                }
            }
            foreach (var itema in dto.Questions)
            {
                int dt = _context.PostSurveys.Max(a => a.Id);
                SurveyQuestion survey = new SurveyQuestion();
                survey.Psid = dt;
                survey.Quest = itema.Questions;
                survey.Views = 0;
                survey.AnsType = itema.AnsType;

                var filename1 = "";
                Random rnd = new Random();
                var rn = rnd.Next(111, 999);
                var imgPth = "";

                if (itema.IsImage == true)
                {

                    IFormFile ss = sd[indx];
                    indx++;

                    var ImagePath1 = rn + RemoveWhitespace(ss.FileName.Replace(",", ""));
                    var pathh = "";
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\images\\Profile\\" + ImagePath1))
                    {
                        ss.CopyTo(fileStream);
                        pathh = Path.Combine(_environment.WebRootPath, "/images/Profile/" + ImagePath1);
                        filename1 = ImagePath1;
                        fileStream.Flush();
                    }
                    imgPth = baseURL + pathh;
                }

                survey.Image = imgPth;
                _context.SurveyQuestions.Add(survey);
                _context.SaveChanges();

                foreach (var item in itema.Asns)
                {
                    int id = _context.SurveyQuestions.Max(a => a.Id);
                    Sqanswer sqanswer = new Sqanswer();
                    sqanswer.Sqid = id;
                    sqanswer.Ans = item;
                    sqanswer.IsChoose = 0;
                    _context.Sqanswers.Add(sqanswer);
                    _context.SaveChanges();
                }
            }
            return "Success"; ;
        }
        public async Task<Tuple<List<GetPostSurveyQueDto>, List<SurveyAnsDto>>> GetPostSurvey(int uid, int PSId)
        {
            var dt = _context.SurveyQuestions.Where(a => a.Psid == PSId).ToList();
            List<GetPostSurveyQueDto> gets = new List<GetPostSurveyQueDto>();
            List<SurveyAnsDto> Ansr = new List<SurveyAnsDto>();
            foreach (var item in dt)
            {
                GetPostSurveyQueDto sd = new GetPostSurveyQueDto();
                sd.questId = item.Id;
                sd.Image = item.Image;
                sd.Anstype = item.AnsType;
                sd.Question = item.Quest;
                var we = _context.Sqanswers.Where(a => a.Sqid == item.Id).ToList();
                foreach (var item1 in we)
                {
                    ///////////////Token
                    var ans = _context.SqAnsDoneByUsers.Where(a => a.AnsId == item1.Id && a.Uid == uid).FirstOrDefault();
                    var ansCount = _context.SqAnsDoneByUsers.Where(a => a.AnsId == item1.Id && a.Uid == uid).ToList();
                    SurveyAnsDto ds = new SurveyAnsDto();
                    ds.QuestId = item1.Sqid;
                    ds.AnsId = item1.Id;
                    ds.Ans = item1.Ans;
                    ds.IsChoose = ansCount.Count;
                    if (ans != null)
                    {
                        ds.DoneByMe = true;
                    }
                    else
                    {
                        ds.DoneByMe = false;
                    }
                    Ansr.Add(ds);
                }
                gets.Add(sd);
            }
            return new Tuple<List<GetPostSurveyQueDto>, List<SurveyAnsDto>>(gets, Ansr);
        }
        public async Task<List<GetPostSurveyTotatQA>> GetPostSurveyRE(string fltr, int uid, int Cid)
        {

            List<GetPostSurveyTotatQA> sdf = new List<GetPostSurveyTotatQA>();
            if (Cid > 0)
            {

                if (fltr == "Latest")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid && (t1.UntilActive <= DateTime.Now.AddDays(1) && t1.UntilActive >= DateTime.Now.AddDays(-4))
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }
                else if (fltr == "Popular")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid
                             orderby t1.CompleteCount descending
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }
                else if (fltr == "MyPol")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid && t1.Uid == uid
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }
                else if (fltr == "ThisWeek")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid && (t1.UntilActive <= DateTime.Now.AddDays(1) && t1.UntilActive >= DateTime.Now.AddDays(-8))
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }
                else if (fltr == "ThisMonth")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid && (t1.UntilActive <= DateTime.Now.AddDays(1) && t1.UntilActive >= DateTime.Now.AddMonths(-1))
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }
                else if (fltr == "ThisQuater")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid && (t1.UntilActive <= DateTime.Now.AddDays(1) && t1.UntilActive >= DateTime.Now.AddMonths(-3))
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }
                else if (fltr == "ThisSamester")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid && (t1.UntilActive <= DateTime.Now.AddDays(1) && t1.UntilActive >= DateTime.Now.AddMonths(-6))
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }
                else if (fltr == "6MonthOlder")
                {
                    var dt = from t1 in _context.PostSurveys
                             join t2 in _context.Logins on t1.Uid equals t2.Id
                             where t1.Cid == Cid && (t1.UntilActive <= DateTime.Now.AddMonths(-6) && t1.UntilActive >= DateTime.Now.AddMonths(-12))
                             select new { t1.Id, t2.Image, t1.Title, CompleteCount = t1.CompleteCount ?? 0, QuestionCount = t1.QuestionCount ?? 0 };
                    dt.ToList().Take(100);


                    foreach (var item in dt)
                    {
                        GetPostSurveyTotatQA sd = new GetPostSurveyTotatQA();
                        sd.Id = item.Id;
                        sd.Count = item.CompleteCount;
                        sd.title = item.Title;
                        sd.image = item.Image;
                        sd.QuestionsCount = item.QuestionCount;
                        sdf.Add(sd);
                    }
                }



            }
            return sdf;
        }
        public async Task<string> PostAnswers(int uid, QuestListDto dto)
        {
            int? toUid = 0;
            foreach (var item in dto.Qid)
            {
                var sdt = _context.SqAnsDoneByUsers.Where(a => a.Uid == uid && a.Qid == item.PSId).ToList();
                var user = _context.SqAnsDoneByUsers.Where(a => a.Qid == item.PSId).FirstOrDefault();
                toUid = user.Uid;
                if (sdt.Count > 0)
                {
                    _context.SqAnsDoneByUsers.RemoveRange(sdt);
                    _context.SaveChanges();
                }
            }

            foreach (var item1 in dto.Qid)
            {
                var dt = _context.PostSurveys.Where(a => a.Id == item1.PSId).FirstOrDefault();
                dt.CompleteCount = dt.CompleteCount + 1;
                _context.PostSurveys.Update(dt);
                _context.SaveChanges();

                foreach (var item in item1.AnsIds)
                {
                    SqAnsDoneByUser sq = new SqAnsDoneByUser();
                    sq.AnsId = item;
                    sq.Uid = uid;
                    sq.Qid = item1.PSId;
                    _context.SqAnsDoneByUsers.Add(sq);
                    _context.SaveChanges();
                    var dts = _context.Sqanswers.Where(a => a.Id == item).FirstOrDefault();
                    dts.IsChoose = dts.IsChoose + 1;
                    _context.Sqanswers.Update(dts);
                    _context.SaveChanges();
                }
            }
            var dta = _context.Logins.Where(a => a.Id == uid).FirstOrDefault();
            var res = SendNotification("/topics/" + toUid, "Post Survey by " + dta.Name, "Comment at your post by " + dta.Name);
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
