public static class AlgebraicNotation {
    static readonly Dictionary<PieceType, char?> PieceName = new Dictionary<PieceType, char?> {
        {PieceType.Pawn, null}, {PieceType.Knight, 'N'}, {PieceType.Bishop, 'B'}, {PieceType.Rook, 'R'}, {PieceType.Queen, 'Q'}, {PieceType.King, 'K'}
    };
    public static readonly Dictionary<int, char> RowMap = new Dictionary<int, char> {
        {0, 'h'}, {1, 'g'}, {2, 'f'}, {3, 'e'}, {4, 'd'}, {5, 'c'}, {6, 'b'}, {7, 'a'}
    };


    /// <summary>
    /// Return the algebraic notation of a given move
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public static string ToAlgebraicNotation(Move move, Game chess) {
        if (move.isCastle == true) {
            return move.isShortCastle ? "O-O" : "O-O-O";
        }

        string algebraicNotation = "";
        char? piece = PieceName[move.piece.type];
        char row = RowMap[move.endX];
        int col = move.endY + 1;
        char? capture = move.isCapture ? 'x' : null;
        char? pawnStartRow = null;
        if (move.piece.type == PieceType.Pawn && move.isCapture == true) {
            pawnStartRow = RowMap[move.startX];
        }
        char? check = move.isCheck ? '+' : null;
        char? promotion = move.isPromotion ? '=' : null;
        char? checkMate = move.isCheckMate ? '#' : null;
        char? promotedPiece = move.isPromotion ? PieceName[move.promotionPiece] : null;

        string disambiguate = "";
        if (move.piece.type == PieceType.Knight || move.piece.type == PieceType.Rook || move.piece.type == PieceType.Bishop || move.piece.type == PieceType.Queen) {
            disambiguate = DisambiguateMove(move, chess);
        }

        algebraicNotation += $"{piece}{disambiguate}{pawnStartRow}{capture}{row}{col}{check}{promotion}{promotedPiece}{checkMate}";


        return algebraicNotation;

    }

    static string DisambiguateMove(Move move, Game chess) {
        int? row = null;
        int? col = null;

        foreach (var (dx, dy, repeatable) in Moves.PieceMap[move.piece.type]) {
            int newX = move.endX + dx;
            int newY = move.endY + dy;

            while (newX < 8 && newX >= 0 && newY < 8 && newY >= 0 && (newX != move.startX || newY != move.startY)) {
                Piece? landingSquare = chess.board[newY, newX];

                if (landingSquare != null && landingSquare.type == move.piece.type && landingSquare.color == move.piece.color) {
                    if (newX != move.startX) {
                        row = move.startX;
                    } else {
                        col = move.startY;
                    }
                }

                if (landingSquare == null && repeatable) {
                    newY += dy;
                    newX += dx;
                }

                else break;
                }
        }

        char? rowChar = row != null ? RowMap[(int)row] : null;
        int? colChar = col != null ? col + 1 : null;

        

        return $"{rowChar}{colChar}";
    }
}