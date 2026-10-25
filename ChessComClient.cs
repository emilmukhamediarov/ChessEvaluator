using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEvaluator
{
    internal class ChessComClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.chess.com/pub";

        public ChessComClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("ChessEvaluator/1.0 (contact: emile.programmer@gmail.com)");
        }

        public async Task<string> GetGamesAsync(string username, int year, int month)
        {
            string url = $"{BaseUrl}/player/{Uri.EscapeDataString(username)}/games/{year}/{month:D2}";
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}

