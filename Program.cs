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

            StockfishEngine engine = new("Engines/stockfish.exe");

            while (true)
            {
                Console.WriteLine("===== CHESS EVALUATOR =====");
                while (true)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Please select one of the following options:");
                    Console.WriteLine("F - Find games");
                    Console.WriteLine("Q - Quit");

                    string? command = Console.ReadLine()?.ToLower().Trim();

                    switch (command)
                    {
                        case "f":
                            Console.Write("Chess.com Username: ");
                            string? username = Console.ReadLine();

                            Console.Write("Choose year YYYY: ");
                            bool validYear = int.TryParse(Console.ReadLine(), out int year);

                            Console.Write("Choose month MM: ");
                            bool validMonth = int.TryParse(Console.ReadLine(), out int month);

                            if(username == null || year == null || month == null)
                            {
                                Console.WriteLine("The information is incomplete.");
                                break;
                            }
                            string json = await client.GetGamesAsync(username, year, month);
                            ChessComGameParser parser = new();
                            List<ChessGame> games = parser.Parse(json);
                            if(games.Count == 0)
                            {
                                Console.WriteLine("No games found.");
                                break;
                            } else
                            {
                                while(true)
                                {
                                    int order = 1;
                                    for (int i = 0; i < games.Count; i++)
                                    {
                                        Console.WriteLine(order + ". " + games[i].White.Username + " vs " + games[i].Black.Username);
                                        order++;
                                    }
                                    Console.WriteLine("Select game or Q to quite: ");
                                    string? input = Console.ReadLine()?.ToLower().Trim();
                                    if (input == "q")
                                    {
                                        return;
                                    }
                                    if(!int.TryParse(input, out int selection))
                                    {
                                        Console.WriteLine("Please enter a valid number or q.");
                                        continue;
                                    }
                                    if (selection < 1 || selection > games.Count)
                                    {
                                        Console.WriteLine("Incorrect selection");
                                        continue;
                                    }
                                    else
                                    {
                                        GameReplay replay = new(games[selection - 1].Pgn!);
                                        while (true)
                                        {
                                            Console.WriteLine("Selected game: " + games[selection - 1].White.Username + " vs " + games[selection - 1].Black.Username);

                                            Console.WriteLine("");
                                            Console.WriteLine("Current position: " + replay.CurrentMove + "/" + replay.TotalMoves);
                                            Console.WriteLine("");
                                            Console.WriteLine("");
                                            Console.WriteLine("Please select one of the following options:");
                                            Console.WriteLine("N - Next position");
                                            Console.WriteLine("P - Previous position");
                                            Console.WriteLine("A - Analyze position");
                                            Console.WriteLine("B - Select another game");
                                            Console.WriteLine("Q - Quit");
                                            string? innerCommand = Console.ReadLine()?.ToLower().Trim();
                                            switch (innerCommand)
                                            {
                                                case "q":
                                                    return;

                                                case "b":
                                                    break;

                                                case "n":
                                                    replay.Next();
                                                    continue;

                                                case "p":
                                                    replay.Previous();
                                                    continue;

                                                case "a":
                                                    EngineAnalysis analysis = await engine.AnalyzeAsync(replay.CurrentFen, 15, 2);
                                                    for (int i = 0; i < analysis.Variations.Count; i++)
                                                        {
                                                        Console.WriteLine("Variation " + i + 1 + ": " + string.Join(", ", analysis.Variations[i].Moves));
                                                        }
                                                        continue;

                                                default:
                                                    Console.WriteLine("Incorrect selection.");
                                                    continue;
                                            }

                                            break;
                                        }
                                        
                                    }
                                }
                            }
                            break;

                        case "q":
                            return;

                        default:
                            Console.WriteLine("Unknown command.");
                            break;
                    }
                }
            }




            //for (int i = 0; i < 10; i++)
            //{
                // replay.Next();
           // }

           // StockfishEngine engine = new("Engines/stockfish.exe");

            //Console.Write("Analysis depth: ");
           // int depth = int.Parse(Console.ReadLine()!);

            //Console.Write("Variations number: ");
            //int multiPv = int.Parse(Console.ReadLine()!);

            // EngineAnalysis analysis = await engine.AnalyzeAsync(replay.CurrentFen, depth, multiPv);

            //foreach (EngineVariation variation in analysis.Variations)
            //{
                //Console.WriteLine(string.Join(", ", variation.Moves));
            //}

        }

        }
    }
