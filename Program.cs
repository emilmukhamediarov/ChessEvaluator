using System.Diagnostics;
using System.Text.Json;
using ChessEvaluator.Models;
using ChessEvaluator.Parsers;

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

            ChessComGameParser parser = new();

            List<ChessGame> games = parser.Parse(json);

            foreach (ChessGame game in games)
            {
                Console.WriteLine($"{game.White.Username} vs {game.Black.Username}");
                Console.WriteLine($"{game.White.Rating} - {game.Black.Rating}");
                Console.WriteLine(game.EndTime);
            }

        }

        }
    }
