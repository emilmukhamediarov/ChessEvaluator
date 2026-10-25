using ChessEvaluator.Engines;
using ChessEvaluator.Models;
using ChessEvaluator.Parsers;
using ChessEvaluator.Replay;
using ChessLite.Parsing;
using System.Diagnostics;
using System.Text.Json;

namespace ChessEvaluator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using HttpClient httpClient = new();

            ChessComClient client = new(httpClient);

            string json = await client.GetGamesAsync("lioigor", 2026, 10);

            ChessComGameParser parser = new();
            List<ChessGame> games = parser.Parse(json);

            GameReplay replay = new(games[^1].Pgn!);

            Console.WriteLine(games[^1].White.Username + " vs " + games[^1].Black.Username);

            for (int i = 0; i < 10; i++)
            {
                replay.Next();
            }

            StockfishEngine engine = new("Engines/stockfish.exe");

            Console.Write("Analysis depth: ");
            int depth = int.Parse(Console.ReadLine()!);

            Console.Write("Variations number: ");
            int multiPv = int.Parse(Console.ReadLine()!);

            EngineAnalysis analysis = await engine.AnalyzeAsync(replay.CurrentFen, depth, multiPv);

            foreach (EngineVariation variation in analysis.Variations)
            {
                Console.WriteLine(string.Join(", ", variation.Moves));
            }

        }

        }
    }
