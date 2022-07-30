using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.SurveyService
{
    public interface ISurveyService
    {
        Task<string> PostQuesttion(int Uid,string baseURL, ForumSurveyDto dto);
        Task<List<SurveyGetDto>> SurveyGetDto(SurveyForGetDto dto);
        Task<List<CommentDto>> Getcomments(ComentUserDto dto);
        Task<string> Likes(ComentUserDto dto);
        Task<string> PostComent(int uid , string baseURL,ComentPostDto dto);
        Task<string> ComentLike(int uid, CommentLikePost dto);
        Task<string> ViewForumPost(ViewForumDto dto);
    }
}
