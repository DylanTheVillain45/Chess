public static class Check {
    public static bool CheckCheck(Game chess, Color color) {
        Piece king = FindKing(chess, color);

        if (IsNonPawnCheck(chess, king, color)) {
            return true;
        }
        

        return false;
    }

    public static Piece FindKing(Game chess, Color color) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Piece landingSquare = chess.board[i, j];
                if (landingSquare.type == PieceType.King && landingSquare.color == color) {
                    return landingSquare;
                }
            }
        }
        throw new InvalidOperationException($"King of color {color} not found on the board!");
    }

    public static bool IsNonPawnCheck(Game chess, Piece king, Color color) {
        PieceType[] checkingPieces = {PieceType.Knight, PieceType.Bishop, PieceType.Rook, PieceType.Queen, PieceType.King};
        foreach (PieceType piece in checkingPieces) {
            foreach (var (dx, dy, repeatable) in Moves.PieceMap[piece]) {
                int newY = king.posY + dy;
                int newX = king.posX + dx;

                while (newY >= 0 && newY < 8 && newX >= 0 && newX < 8) {
                    Piece landingSquare = chess.board[newY, newX];

                    if (landingSquare.type == piece && landingSquare.color != king.color) {
                        return true;
                    }

                    if (landingSquare.color == king.color || landingSquare.type != piece) {
                        break;
                    }

                    if (repeatable) {
                        newY += dy;
                        newX += dx;
                    } else {
                        break;
                    }
                }
            }
        }
        return false;
    }
}