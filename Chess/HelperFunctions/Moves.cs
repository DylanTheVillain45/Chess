public static class Moves {
    public static readonly Dictionary<PieceType, List<(int, int, bool)>> PieceMap = new Dictionary<PieceType, List<(int, int, bool)>> {
        {PieceType.Knight, new List<(int, int, bool)> {(1, 2, false), (2, 1, false), (-1, 2, false), (-2, 1, false), (1, -2, false), (2, -1, false), (-1, -2, false), (-2, -1, false)}},
        {PieceType.Bishop, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true)}},
        {PieceType.Rook, new List<(int, int, bool)> {(1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {PieceType.Queen, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true), (1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {PieceType.King, new List<(int, int, bool)> {(1, 1, false), (-1, 1, false), (1, -1, false), (-1, -1, false), (1, 0, false), (-1, 0, false), (0, 1, false), (0, -1, false)}},
    };

    

    public static void AddMove(Game chess, Move move) {
        if (move.piece.color != chess.color) return;
        CommitMove(chess, move);

        bool checkFilter = Check.CheckCheck(chess, move.piece.color);

        if (checkFilter) {
            UnCommitMove(chess, move);
            return;
        } 

        Color opponentColor = move.piece.color == Color.White ? Color.Black : Color.White;

        bool isCheck = Check.CheckCheck(chess, opponentColor);

        if (isCheck) {
            Dictionary<string, Move> temp = chess.MoveDictionary;
            chess.color = opponentColor;
            MoveGenerator.GetMoves(chess);
            if (chess.MoveDictionary.Count == 0) {
                move.isCheckMate = true;
            } else {
                move.isCheck = true;
            }
            chess.color = move.piece.color;
            chess.MoveDictionary = temp;
        }

        UnCommitMove(chess, move);
        

        string algebraicNotation = AlgebraicNotation.ToAlgebraicNotation(move, chess);


        if (chess.MoveDictionary.ContainsKey(algebraicNotation) == false && move.piece.color == chess.color) {
            chess.MoveDictionary.Add(algebraicNotation, move);
        }
    }

    public static void CommitMove(Game chess, Move move) {
        move.piece.posY = move.endY;
        move.piece.posX = move.endX;

        chess.board[move.endY, move.endX] = move.piece;
        chess.board[move.startY, move.startX] = null;

        if (move.isEnpassant) {
            chess.board[move.startY, move.endX] = null;
        }
    }

    static void UnCommitMove(Game chess, Move move) {
        move.piece.posY = move.startY;
        move.piece.posX = move.startX;

        chess.board[move.startY, move.startX] = move.piece;

        if (move.isCapture) {
            if (move.isEnpassant) {
                chess.board[move.startY, move.endX] = move.capturedPiece;
            } else {
                chess.board[move.endY, move.endX] = move.capturedPiece;
            }

        }
                
        else {
            chess.board[move.endY, move.endX] = null;    
        } 
    }
}