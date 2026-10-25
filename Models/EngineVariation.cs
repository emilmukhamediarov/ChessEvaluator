using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEvaluator.Models
{
    internal class EngineVariation
    {
        public int Rank { get; set; }
        public string ScoreType { get; set; }
        public int ScoreValue { get; set; }
        public List<string> Moves { get; set; }   
        
        public EngineVariation(int  rank, string scoreType, int scoreValue, List<string> moves)
        {
            Rank = rank;
            ScoreType = scoreType;
            ScoreValue = scoreValue;
            Moves = moves;
        }
    }

}
