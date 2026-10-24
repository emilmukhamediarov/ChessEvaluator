using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEvaluator.Models
{
    internal class GameParticipant
    {
        public string? Username { get; set; }
        public int? Rating { get; set; }
        public string? Result { get; set; }
    }
}
