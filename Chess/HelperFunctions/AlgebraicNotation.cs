public static class AlgebraicNotation {
    static readonly Dictionary<PieceType, char?> PieceName = new Dictionary<PieceType, char?> {
        {PieceType.Pawn, null}, {PieceType.Knight, 'N'}, {PieceType.Bishop, 'B'}, {PieceType.Rook, 'R'}, {PieceType.Queen, 'Q'}, {PieceType.King, 'K'}
    };
    static readonly Dictionary<int, char> RowMap = new Dictionary<int, char> {
        {0, 'a'}, {1, 'b'}, {2, 'c'}, {3, 'd'}, {4, 'e'}, {5, 'f'}, {6, 'g'}, {7, 'h'}
    };


    /// <summary>
    /// Return the algebraic notation of a given move
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public static string ToAlgebraicNotation(Move move, Game chess) {
        string algebraicNotation = "";
        char? piece = PieceName[move.piece];
        char row = RowMap[move.endX];
        int col = move.endY + 1;
        char? capture = move.isCapture ? 'x' : null;
        char? pawnStartRow = null;
        if (move.piece == PieceType.Pawn && move.isCapture == true) {
            pawnStartRow = RowMap[move.startX];
        }
        char? check = move.isCapture ? '+' : null;
        char? promotion = move.isPromotion ? '=' : null;
        char? promotedPiece = move.isPromotion ? PieceName[move.promotionPiece] : null;

        string disambiguate = DisambiguateMove(move, chess);

        algebraicNotation += $"{piece}{disambiguate}{pawnStartRow}{capture}{row}{col}{check}{promotion}{promotedPiece}";

        if (move.isCastle == true) {
            if (move.isShortCastle == true) {
                algebraicNotation = "O-O";
            } else {
                algebraicNotation = "O-O-O";
            }
        }

        return algebraicNotation;

    }

    static string DisambiguateMove(Move move, Game chess) {
        return "";
    }

}