using System.Diagnostics;

namespace ChessEvaluator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string enginePath = Path.Combine(
                AppContext.BaseDirectory,
                "Engines",
                "stockfish.exe"
            );

            if (!File.Exists(enginePath))
            {
                Console.WriteLine("Stockfish not found!");
                return;
            }

            using Process engine = new Process();

            engine.StartInfo.FileName = enginePath;
            engine.StartInfo.UseShellExecute = false;
            engine.StartInfo.RedirectStandardInput = true;
            engine.StartInfo.RedirectStandardOutput = true;
            engine.StartInfo.CreateNoWindow = true;

            engine.Start();

            // Initialize the engine
            engine.StandardInput.WriteLine("uci");
            ReadUntil(engine, "uciok");

            // Check whether the engine is ready
            engine.StandardInput.WriteLine("isready");
            ReadUntil(engine, "readyok");

            Console.WriteLine("Stockfish is ready!");

            // Set up the starting position
            engine.StandardInput.WriteLine("position startpos");

            // Analyze to depth 12
            engine.StandardInput.WriteLine("go depth 12");

            string result = ReadUntil(engine, "bestmove");

            Console.WriteLine(result);

            // Shut down Stockfish
            engine.StandardInput.WriteLine("quit");
            engine.WaitForExit();
        }

        static string ReadUntil(Process engine, string prefix)
        {
            string? line;

            while ((line = engine.StandardOutput.ReadLine()) != null)
            {
                if (line.StartsWith(prefix))
                {
                    return line;
                }
            }

            throw new Exception("Stockfish stopped responding.");
        }
    }
}