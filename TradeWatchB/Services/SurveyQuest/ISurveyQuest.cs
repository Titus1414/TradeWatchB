using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.SurveyQuest
{
    public interface ISurveyQuest
    {
        Task<string> PostSurveyQuesy(int uid, string baseURL, PostSurveyQuestDto dto);
        Task<Tuple<List<GetPostSurveyQueDto>, List<SurveyAnsDto>>> GetPostSurvey(int uid,int PSId);
        Task<List<GetPostSurveyTotatQA>> GetPostSurveyRE(string fltr, int uid, int Cid);
        Task<string> PostAnswers(int uid, QuestListDto dto);
    }
}
