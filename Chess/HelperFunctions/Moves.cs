public static class Moves {
    public static readonly Dictionary<PieceType, List<(int, int, bool)>> PieceMap = new Dictionary<PieceType, List<(int, int, bool)>> {
        {PieceType.Knight, new List<(int, int, bool)> {(1, 2, false), (2, 1, false), (-1, 2, false), (-2, 1, false), (1, -2, false), (2, -1, false), (-1, -2, false), (-2, -1, false)}},
        {PieceType.Bishop, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true)}},
        {PieceType.Rook, new List<(int, int, bool)> {(1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {PieceType.Queen, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true), (1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {PieceType.King, new List<(int, int, bool)> {(1, 1, false), (-1, 1, false), (1, -1, false), (-1, -1, false), (1, 0, false), (-1, 0, false), (0, 1, false), (0, -1, false)}},
    };

    /// <summary>
    /// Call Get Moves For All Pieces of a Color
    /// </summary>
    /// <param name="chess"></param>
    /// <param name="color"></param>
    public static void GetMoves(Game chess, Color color) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (chess.board[i, j].type != PieceType.None && chess.board[i, j].color == color) {
                    chess.board[i, j].GetPossibleMoves(chess);
                }
            }
        }
    }

    public static void AddMove(Game chess, Move move) {
        // Console.WriteLine($"{move.piece} {move.startY} {move.startX} {move.endY} {move.endX}");

        // TODO : add check filtering and more logic

        string algebraicNotation = AlgebraicNotation.ToAlgebraicNotation(move);
        chess.MoveDictionary.Add(algebraicNotation, move);
    }

}