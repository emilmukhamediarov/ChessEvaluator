using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEvaluator.Models
{
    internal class EngineAnalysis
    {
        public string BestMove { get; set; }
        public List<EngineVariation> Variations { get; set; }

        public EngineAnalysis(string bestMove, List<EngineVariation> variations)
        {
            BestMove = bestMove;
            Variations = variations;
        }
    }
}
