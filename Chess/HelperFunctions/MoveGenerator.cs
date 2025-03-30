public static class MoveGenerator {
    /// <summary>
    /// Call Get Moves For All Pieces of a Color
    /// </summary>
    /// <param name="chess"></param>
    /// <param name="color"></param>
    public static void GetMoves(Game chess, Color color) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (chess.board[i, j].type != PieceType.None && chess.board[i, j].color == color) {
                    GetPossibleMoves(chess, chess.board[i, j]);
                }
            }
        }
    }

    static bool IsPawnCheck(Game chess, Color color, int pawnY, int pawnX, int direction) {
        for (int i = -1; i <= 1; i += 2) {
            int newY = pawnY + direction;
            int newX = pawnX + i;
            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8) {
                if (chess.board[newY, newX].type == PieceType.King && chess.board[newY, newX].color != color) {
                    return true;
                }
            }
        }

        return false;

    }

    static void GetPawnMove(Game chess, Piece piece) {
        int direction = piece.color == Color.White ? 1 : -1;
        int startRow = piece.color == Color.White ? 1 : 6;

        // prevent out of range error
        if (piece.posY + direction < 0 || piece.posY + direction >= 8) return;

        if (chess.board[piece.posY + direction, piece.posX].type == PieceType.None) {
            Move newMove;
            if (IsPawnCheck(chess, piece.color, piece.posY + direction, piece.posX, direction)) {
                newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, piece.posY + direction, piece.posX, false, PieceType.None, true);
            } else {
                newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, piece.posY + direction, piece.posX);
            }
            Moves.AddMove(chess, newMove);
            if (piece.posY == startRow && chess.board[piece.posY + direction * 2, piece.posX].type == PieceType.None) {
                Move newMove2;
                if (IsPawnCheck(chess, piece.color, piece.posY + direction * 2, piece.posX, direction)) {
                    newMove2 = new Move(piece.type, piece.color, piece.posY, piece.posX, piece.posY + direction * 2, piece.posX, false, PieceType.None, true);
                } else {
                    newMove2 = new Move(piece.type, piece.color, piece.posY, piece.posX, piece.posY + direction * 2, piece.posX);
                }
                Moves.AddMove(chess, newMove2);
            }
        }
         
        for (int dx = -1 ; dx <= 1; dx++) {
            int newY = piece.posY + direction;
            int newX = piece.posX + dx;
            if (newX >= 0 && newX < 8) {
                if (chess.board[newY, newX].type != PieceType.None && chess.board[newY, newX].color != piece.color) {
                    Move newMove;
                    if (IsPawnCheck(chess, piece.color, newY, newX, direction)) {
                        newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, newY, newX, true, chess.board[newY, newX].type, true);
                    } else {
                        newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, newY, newX, true, chess.board[newY, newX].type);
                    }
                    Moves.AddMove(chess, newMove);
                } else if (chess.board[newY, newX].type == PieceType.None && chess.board[piece.posY, newX].type == PieceType.Pawn && chess.board[piece.posY, newX].color != piece.color) {
                    // Add check if last move was it
                    Move newMove;
                    if (IsPawnCheck(chess, piece.color, newY, newX, direction)) {
                        newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, newY, newX, true, chess.board[newY, newX].type, true, false, false, true);
                    } else {
                        newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, newY, newX, true, chess.board[newY, newX].type, false, false, false, true);
                    }
                    Moves.AddMove(chess, newMove);
                }
            }
        }
    }

    static void GetNonPawnMoves(Game chess, Piece piece) {
        foreach (var (dx, dy, repeatable) in Moves.PieceMap[piece.type]) {
            int newX = piece.posX + dx;
            int newY = piece.posY + dy;

            while (newX < 8 && newX >= 0 && newY < 8 && newY >= 0) {
                Piece landingSquare = chess.board[newY, newX];

                if (landingSquare.type == PieceType.None) {
                    Move newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, newY, newX);
                    Moves.AddMove(chess, newMove);
                }

                else if (landingSquare.color != piece.color) {
                    Move newMove = new Move(piece.type, piece.color, piece.posY, piece.posX, newY, newX, true, landingSquare.type);
                    Moves.AddMove(chess, newMove);
                    break;
                }

                else break;

                if (!repeatable) break;

                newY += dy;
                newX += dx;
            }
        }
    }

    static void GetCastleMove(Game chess, Piece piece) {
        
    }

    static public void GetPossibleMoves(Game chess, Piece piece) {
        if (piece.type == PieceType.King) GetCastleMove(chess, piece);
        if (piece.type == PieceType.Pawn) GetPawnMove(chess, piece);
        else GetNonPawnMoves(chess, piece);
    }

}