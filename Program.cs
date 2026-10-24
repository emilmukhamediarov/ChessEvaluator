using System.Diagnostics;

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

            Console.WriteLine(json);
        }
    }
}