using System.Diagnostics;
using System.Text.Json;

namespace ChessEvaluator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using HttpClient httpClient = new();

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("ChessEvaluator/1.0 (contact: emile.programmer@gmail.com)");

            ChessComClient client = new(httpClient);

            string json = await client.GetGamesAsync(
                "EmilMuhamedyarov", 2026, 10
            );

            using JsonDocument document = JsonDocument.Parse(json);

            JsonElement games = document.RootElement.GetProperty("games");

            await File.WriteAllTextAsync("games.json", json);

            foreach (JsonElement game in games.EnumerateArray())
            {

                long endTimestamp = game.GetProperty("end_time").GetInt64();

                DateTimeOffset endDate = DateTimeOffset.FromUnixTimeSeconds(endTimestamp);



                string? white = game.GetProperty("white")
                                    .GetProperty("username")
                                    .GetString();

                int whiteElo = game.GetProperty("white")
                                    .GetProperty("rating")
                                    .GetInt32();

                string? whiteResult = game.GetProperty("white")
                                    .GetProperty("result")
                                    .GetString();

                string? black = game.GetProperty("black")
                                    .GetProperty("username")
                                    .GetString();

                int blackElo = game.GetProperty("black")
                                    .GetProperty("rating")
                                    .GetInt32();

                string? blackResult = game.GetProperty("black")
                                    .GetProperty("result")
                                    .GetString();

                Console.WriteLine(endDate);
                Console.WriteLine(white);
                Console.WriteLine(whiteElo);
                Console.WriteLine(whiteResult);
                Console.WriteLine(black);
                Console.WriteLine(blackElo);
                Console.WriteLine(blackResult);
                Console.WriteLine("");

            }

        }
    }
}