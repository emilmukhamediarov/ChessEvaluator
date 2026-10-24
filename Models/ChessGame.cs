using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ChessEvaluator.Models;

namespace ChessEvaluator.Models
{
    internal class ChessGame
    {
        public required GameParticipant White {  get; set; }
        public required GameParticipant Black { get; set; }
        public string? TimeControl { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string? Pgn { get; set; }
        public string? Url { get; set; }
        public required string Source;
    }
}
