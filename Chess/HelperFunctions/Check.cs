public static class Check {
    public static bool CheckCheck(Game chess, Color color) {
        Piece king = FindKing(chess, color);

        if (IsNonPawnCheck(chess, king) || IsPawnCheck(chess, king)) {
            return true;
        }
        

        return false;
    }

    public static Piece FindKing(Game chess, Color color) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Piece? landingSquare = chess.board[i, j];
                if (landingSquare != null && landingSquare.type == PieceType.King && landingSquare.color == color) {
                    return landingSquare;
                }
            }
        }
        throw new InvalidOperationException($"King of color {color} not found on the board!");
    }

    static bool IsPawnCheck(Game chess, Piece king) {
        int direction = king.color == Color.White ? 1 : -1;
        for (int i = -1; i <= 1; i += 2) {
            int newY = king.posY + direction;
            int newX = king.posX + i;
            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8) {
                Piece? landingSquare = chess.board[newY, newX];
                if (landingSquare != null && landingSquare.type == PieceType.Pawn && landingSquare.color != king.color) {
                    return true;
                }
            }
        }

        return false;
    }
    static bool IsNonPawnCheck(Game chess, Piece king) {
        PieceType[] checkingPieces = {PieceType.Knight, PieceType.Bishop, PieceType.Rook, PieceType.Queen, PieceType.King};
        foreach (PieceType piece in checkingPieces) {
            foreach (var (dx, dy, repeatable) in Moves.PieceMap[piece]) {
                int newY = king.posY + dy;
                int newX = king.posX + dx;

                while (newY >= 0 && newY < 8 && newX >= 0 && newX < 8) {
                    Piece? landingSquare = chess.board[newY, newX];
                    if (landingSquare != null) {
                        if (landingSquare.type == piece && landingSquare.color != king.color) {
                            return true;
                        }

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