using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TradeWatchB.Models.DTO;
using TradeWatchB.Services.SurveyQuest;
using TradeWatchB.Services.SurveyService;

namespace TradeWatchB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly ISurveyQuest _surveyQuest;
        public SurveyController(ISurveyService surveyService, ISurveyQuest surveyQuest)
        {
            _surveyService = surveyService;
            _surveyQuest = surveyQuest;
        }
        [HttpPost]
        [Route("PostSurveyQuestion")]
        public async Task<IActionResult> UploadFile([FromForm] ForumSurveyDto dto)
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
                        var result = await _surveyService.PostQuesttion(id, baseURL, dto);
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
        [Route("GetSurveyData")]
        public async Task<IActionResult> GetData([FromBody] SurveyForGetDto dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    dto.Uid = Convert.ToInt32(name);
                    var result = await _surveyService.SurveyGetDto(dto);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("GetComentsData")]
        public async Task<IActionResult> GetComentData([FromBody] ComentUserDto dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    dto.Uid = Convert.ToInt32(name);
                    var result = await _surveyService.Getcomments(dto);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("PostComentsData")]
        public async Task<IActionResult> PostComentData([FromForm] ComentPostDto dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    string baseURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    var result = await _surveyService.PostComent(Convert.ToInt32(name), baseURL, dto);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("PostIsLikeData")]
        public async Task<IActionResult> PostIsLikeData([FromBody] ComentUserDto dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    string baseURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    dto.Uid = Convert.ToInt32(name);
                    var result = await _surveyService.Likes(dto);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("PostViewsData")]
        public async Task<IActionResult> PostViewsData([FromBody] ViewForumDto dto)
        {
            var result = await _surveyService.ViewForumPost(dto);
            return Ok(new { res = result });
        }
        [HttpPost]
        [Route("PostComentIsLikeData")]
        public async Task<IActionResult> PostComentIsLikeData([FromBody] CommentLikePost dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    string baseURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    var result = await _surveyService.ComentLike(Convert.ToInt32(name), dto);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("PostSurveyQuestData")]
        public async Task<IActionResult> PostSurveyQuData([FromForm] PostSurveyQuestDto dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    string baseURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    var result = await _surveyQuest.PostSurveyQuesy(Convert.ToInt32(name), baseURL, dto);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetSurveyQACount")]
        public async Task<IActionResult> GetQACount(string fltr, int Cid)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    var result = await _surveyQuest.GetPostSurveyRE(fltr, Convert.ToInt32(name), Cid);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetSurveyQuetions")]
        public async Task<IActionResult> GetSurQuest(int PSId)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    var result = await _surveyQuest.GetPostSurvey(Convert.ToInt32(name),PSId);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("PostSurveyQuest")]
        public async Task<IActionResult> PostSurveyQuData([FromBody] QuestListDto dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                if (name != null)
                {
                    var result = await _surveyQuest.PostAnswers(Convert.ToInt32(name), dto);
                    return Ok(new { res = result });
                }
            }
            return BadRequest();
        }
    }
}
