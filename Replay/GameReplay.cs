using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Transactions;
using ChessLite.Parsing;

namespace ChessEvaluator.Replay
{
    internal class GameReplay
    {
        private readonly List<string> _positions = new();
        private int _currentMove = 0;

        public string CurrentFen
        {
            get
            {
                return _positions[_currentMove];
            }
        }
        public int CurrentMove
        {
            get
            {
                return _currentMove;
            }
        }
        public int TotalMoves
        {
            get
            {
                return _positions.Count - 1;
            }
        }

        public GameReplay(string pgn)
        {
            var record = Pgn.Parse(pgn);
            var game = record.CreateGame();

            _positions.Add(Fen.Format(game));

            for (int i =0; i < record.Mainline.Count; i++)
            {
                game.UndoMove();
                _positions.Add(Fen.Format(game));
            }

            _positions.Reverse();
        }

        public void Next()
        {
            if (_currentMove < TotalMoves)
            {
                _currentMove++;
            }
        }

        public void Previous()
        {
            if (_currentMove > 0)
            {
                _currentMove--;
            }
        }
    }
}
