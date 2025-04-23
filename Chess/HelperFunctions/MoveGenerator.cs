public static class MoveGenerator {
    static readonly PieceType[] promotablePieces = [PieceType.King, PieceType.Bishop, PieceType.Rook, PieceType.Queen];
    /// <summary>
    /// Call Get Moves For All Pieces of a Color
    /// </summary>
    /// <param name="chess"></param>
    /// <param name="color"></param>
    public static void GetMoves(Game chess, Color color) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Piece? piece = chess.board[i, j];
                if (piece != null && piece.color == color) {
                    GetPossibleMoves(chess, piece);
                }
            }
        }
    }

    static bool isValidEnPassant(Game chess, Piece piece, int newX, int direction) {
        string lastMove = piece.color == Color.White ? chess.PastMoves[^1].Item2 : chess.PastMoves[^1].Item1;

        if (lastMove != $"{AlgebraicNotation.RowMap[newX]}{piece.posY + 1}") return false;

        for (int i = 0; i < chess.PastMoves.Count - 1; i++) {
            string pastMove = piece.color == Color.White ? chess.PastMoves[i].Item2 : chess.PastMoves[i].Item1;
            if (pastMove == $"{AlgebraicNotation.RowMap[newX]}{piece.posY + direction + 1}") {
                return false;
            }
        }
        
        return true;
    }

    static void GetPawnMove(Game chess, Piece piece) {
        int direction = piece.color == Color.White ? 1 : -1;
        int startRow = piece.color == Color.White ? 1 : 6;
        int enPassantRow = piece.color == Color.White ? 5 : 4;
        int endRow = piece.color == Color.White ? 7 : 0;

        // prevent out of range error
        if (piece.posY + direction < 0 || piece.posY + direction >= 8) return;

        if (chess.board[piece.posY + direction, piece.posX] == null) {
            if (piece.posY == endRow) {
                for (int i = 0; i < promotablePieces.Length; i++) {
                    Move newMove = new Move(piece, piece.posY, piece.posX, piece.posY + direction, piece.posX, false, null, false, false, false, true, promotablePieces[i]);
                    Moves.AddMove(chess, newMove);
                }
            } else {
                Move newMove = new Move(piece, piece.posY, piece.posX, piece.posY + direction, piece.posX);
                Moves.AddMove(chess, newMove);
            }

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
                    if (piece.posY == endRow) {
                        for (int i = 0; i < promotablePieces.Length; i++) {
                            Move newMove = new Move(piece, piece.posY, piece.posX, piece.posY + direction, piece.posX, true, capturedPiece, false, false, false, true, promotablePieces[i]);
                            Moves.AddMove(chess, newMove);
                        }
                    } else {
                        Move newMove = new Move(piece, piece.posY, piece.posX, newY, newX, true, capturedPiece);
                        Moves.AddMove(chess, newMove);
                    }
                } else if (capturedPiece == null && enPassantCapturePiece != null && enPassantCapturePiece.type == PieceType.Pawn && enPassantCapturePiece.color != piece.color && piece.posY == enPassantRow) {
                    if (isValidEnPassant(chess, piece, newX, direction)) {
                        Move newMove = new Move(piece, piece.posY, piece.posX, newY, newX, true, enPassantCapturePiece, false, false, true);
                        Moves.AddMove(chess, newMove);
                    }
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

    static public void GetPossibleMoves(Game chess, Piece piece) {
        if (piece.color != chess.color) return;

        if (piece.type == PieceType.King) GetCastleMove(chess, piece);
        if (piece.type == PieceType.Pawn) GetPawnMove(chess, piece);
        if (piece.type != PieceType.Pawn) GetNonPawnMoves(chess, piece);
    }

    static void GetCastleMove(Game chess, Piece king) {
        if (HasMoved(chess, king)) return;

        int y = king.color == Color.White ? 7 : 0;

        Piece? rookShort = chess.board[y, 0];
        if (rookShort != null && rookShort.type == PieceType.Rook && !HasMoved(chess, rookShort)) {
            if (IsPathClear(chess, king, true)) {
                if (IsSafeForKing(chess, king, new int[] {2, 1})) {
                    Move newMove = new Move(king, king.posY, king.posX, king.posY, king.posX - 2, false, null, true, true);
                    Moves.AddMove(chess, newMove);
                }
            }
        }

        Piece? rookLong = chess.board[y, 7];
        if (rookLong != null && rookLong.type == PieceType.Rook && !HasMoved(chess, rookLong)) {
            if (IsPathClear(chess, king, false)) {
                if (IsSafeForKing(chess, king, new int[] {4, 5})) {
                    Move newMove = new Move(king, king.posY, king.posX, king.posY, king.posX - 2, false, null, true, false);
                    Moves.AddMove(chess, newMove);
                }
            }
        }
    }

    static bool HasMoved(Game chess, Piece piece) {
        foreach (var pastMove in chess.PastMoves) {
            if (pastMove.Item1.Contains($"{AlgebraicNotation.RowMap[piece.posX]}{piece.posY + 1}") || pastMove.Item2.Contains($"{AlgebraicNotation.RowMap[piece.posX]}{piece.posY + 1}")) {
                return true;
            }
        }

        return false;
    }

    static bool IsPathClear(Game chess, Piece king, bool isShort) {
        if (isShort) {
            for (int i = 1; i <= 2; i++) {
                int x = king.posX + i;
                if (x < 0 || x > 7) return false;
                if (chess.board[king.posY, king.posX - i] != null) return false;
            }

            return true;
        } else {
            for (int i = 1; i <= 2; i++) {
                int x = king.posX + i;
                if (x < 0 || x > 7) return false;
                if (chess.board[king.posY, king.posX + i] != null) return false;
            }

            return true;
        }
    }

    static bool IsSafeForKing(Game chess, Piece king, int[] pathX) {
        int startX = king.posX;
        foreach (int x in pathX) {
            if (chess.board[king.posY, x] != null) return false;
            chess.board[king.posY, king.posX] = null;
            king.posX = x;
            chess.board[king.posY, x] = king;
            if (Check.CheckCheck(chess, king.color)) {
                chess.board[king.posY, king.posX] = null;
                king.posX = startX;
                chess.board[king.posY, startX] = king;
                return false;
            }
        }
        chess.board[king.posY, king.posX] = null;
        king.posX = startX;
        chess.board[king.posY, startX] = king;
        return true;
    }
}