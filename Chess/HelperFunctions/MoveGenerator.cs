public static class MoveGenerator {
    /// <summary>
    /// Call Get Moves For All Pieces of a Color
    /// </summary>
    /// <param name="chess"></param>
    /// <param name="color"></param>
    public static void GetMoves(Game chess) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Piece? piece = chess.board[i, j];
                if (piece != null && piece.color == chess.color) {
                    GetPossibleMoves(chess, piece);
                }
            }
        }
    }

    static void GetPawnMove(Game chess, Piece piece) {
        int direction = piece.color == Color.White ? 1 : -1;
        int startRow = piece.color == Color.White ? 1 : 6;

        // prevent out of range error
        if (piece.posY + direction < 0 || piece.posY + direction >= 8) return;

        if (chess.board[piece.posY + direction, piece.posX] == null) {
            Move newMove = new Move(piece, piece.posY, piece.posX, piece.posY + direction, piece.posX);
            Moves.AddMove(chess, newMove);
            if (piece.posY == startRow && chess.board[piece.posY + direction * 2, piece.posX] == null) {
                Move newMove2 = new Move(piece, piece.posY, piece.posX, piece.posY + direction * 2, piece.posX);
                Moves.AddMove(chess, newMove2);
            }
        }
         
        for (int dx = -1 ; dx <= 1; dx += 2) {
            int newY = piece.posY + direction;
            int newX = piece.posX + dx;
            if (newX >= 0 && newX < 8) {
                Piece? capturedPiece = chess.board[newY, newX];
                Piece? enPassantCapturePiece = chess.board[piece.posY, newX];
                if (capturedPiece != null && capturedPiece.color != piece.color) {
                    Move newMove = new Move(piece, piece.posY, piece.posX, newY, newX, true, capturedPiece);
                    Moves.AddMove(chess, newMove);
                } else if (capturedPiece == null && enPassantCapturePiece != null && enPassantCapturePiece.type == PieceType.Pawn && enPassantCapturePiece.color != piece.color) {
                    // Add check if last move was it
                    Move newMove = new Move(piece, piece.posY, piece.posX, newY, newX, true, enPassantCapturePiece, false, false, false, true);
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
                Piece? landingSquare = chess.board[newY, newX];

                if (landingSquare == null) {
                    Move newMove = new Move(piece, piece.posY, piece.posX, newY, newX);
                    Moves.AddMove(chess, newMove);
                }

                else if (landingSquare.color != piece.color) {
                    Move newMove = new Move(piece, piece.posY, piece.posX, newY, newX, true, landingSquare);
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
        if (piece.color != chess.color) return;

        if (piece.type == PieceType.King) GetCastleMove(chess, piece);
        if (piece.type == PieceType.Pawn) GetPawnMove(chess, piece);
        if (piece.type != PieceType.Pawn) GetNonPawnMoves(chess, piece);
    }

}