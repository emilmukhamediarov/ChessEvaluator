using ChessEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEvaluator.Parsers
{
    internal class StockfishOutputParser
    {
        public EngineAnalysis Parse(List<string> lines)
        {
            string? bestMove = null;
            List<EngineVariation> variations = new();

            foreach (string line in lines)
            {
                if (line.StartsWith("info"))
                {
                    string[] parts = line.Split(" ");
                    int rankIndex = Array.IndexOf(parts, "multipv");
                    int rank = int.Parse(parts[rankIndex + 1]);

                    int scoreIndex = Array.IndexOf(parts, "score");
                    string scoreType = parts[scoreIndex + 1];
                    int scoreValue = int.Parse(parts[scoreIndex + 2]);

                    int movesIndex = Array.IndexOf(parts, "pv");
                    List<string> moves = parts.Skip(movesIndex + 1).ToList();

                    EngineVariation variation = new(rank, scoreType, scoreValue, moves);
                    variations.Add(variation);
                }
                if (line.StartsWith("bestmove"))
                {
                    string[] parts = line.Split(" ");
                    int bestMoveIndex = Array.IndexOf(parts, "bestmove");
                    string best = parts[bestMoveIndex + 1];
                    bestMove = best;
                }
            }

            if (bestMove == null)
                throw new FormatException("Best move not found.");

            return new EngineAnalysis(bestMove, variations);
        }
    }
}
