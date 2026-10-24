using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ChessEvaluator.Models;

namespace ChessEvaluator.Parsers
{
    internal class ChessComGameParser
    {
        private GameParticipant ParseParticipant(JsonElement player)
        {
            GameParticipant participant = new GameParticipant();

            if (player.TryGetProperty("username", out var username)
                && username.ValueKind == JsonValueKind.String)
                participant.Username = username.GetString();

            if (player.TryGetProperty("rating", out var rating)
                && rating.ValueKind == JsonValueKind.Number)
                participant.Rating = rating.GetInt32();

            if (player.TryGetProperty("result", out var result)
                && result.ValueKind == JsonValueKind.String)
                participant.Result = result.GetString();

            return participant;
        }
        public List<ChessGame> Parse(string json)
        {
            List<ChessGame> result = new List<ChessGame>();

            using JsonDocument document = JsonDocument.Parse(json);

            JsonElement games = document.RootElement.GetProperty("games");

            foreach (JsonElement game in games.EnumerateArray())
            {
                ChessGame chessGame = new ChessGame
                {
                    White = ParseParticipant(game.GetProperty("white")),
                    Black = ParseParticipant(game.GetProperty("black")),
                    TimeControl = game.GetProperty("time_control").GetString(),
                    EndTime = DateTimeOffset.FromUnixTimeSeconds(game.GetProperty("end_time").GetInt64()),
                    Pgn = game.GetProperty("pgn").GetString(),
                    Url = game.GetProperty("url").GetString(),
                    Source = "Chess.com"
                };

                result.Add(chessGame);
            }

            return result;
        }
    }
}
