using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.YouTube
{
    public class YouTubeSearch : IYouTubeSearch
    {
        public async Task<object> SearchByKeywword(string q)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKeys.YouTubeApiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = q;
            searchListRequest.MaxResults = 50;
            var searchListResponse = await searchListRequest.ExecuteAsync();
            List<YouTubeDto> videos = new List<YouTubeDto>();
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        YouTubeDto you = new YouTubeDto();
                        you.Title = searchResult.Snippet.Title;
                        you.VideoId = searchResult.Id.VideoId;
                        you.Url = searchResult.Snippet.Thumbnails.Default__.Url;
                        you.ChanelName = searchResult.Snippet.ChannelTitle;
                        you.ChanelId = searchResult.Snippet.ChannelId;
                        videos.Add(you);
                        break;
                }
            }
            return videos;
        }
    }
}
