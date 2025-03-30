public static class Moves {
    public static readonly Dictionary<PieceType, List<(int, int, bool)>> PieceMap = new Dictionary<PieceType, List<(int, int, bool)>> {
        {PieceType.Knight, new List<(int, int, bool)> {(1, 2, false), (2, 1, false), (-1, 2, false), (-2, 1, false), (1, -2, false), (2, -1, false), (-1, -2, false), (-2, -1, false)}},
        {PieceType.Bishop, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true)}},
        {PieceType.Rook, new List<(int, int, bool)> {(1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {PieceType.Queen, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true), (1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {PieceType.King, new List<(int, int, bool)> {(1, 1, false), (-1, 1, false), (1, -1, false), (-1, -1, false), (1, 0, false), (-1, 0, false), (0, 1, false), (0, -1, false)}},
    };

    

    public static void AddMove(Game chess, Move move) {
        CommitMove(chess, move);

        bool checkFilter = Check.CheckCheck(chess, move.pieceColor);

        if (checkFilter) return;

        Color opponentColor = move.pieceColor == Color.White ? Color.Black : Color.White;
        bool isCheck = Check.CheckCheck(chess, opponentColor);

        if (isCheck) {
            Dictionary<String, Move> temp = chess.MoveDictionary;
            MoveGenerator.GetMoves(chess, opponentColor);
            if (chess.MoveDictionary.Count == 0) {
                move.isCheckMate = true;
            }
            chess.MoveDictionary = temp;
        }

        UnCommitMove(chess, move);
        

        string algebraicNotation = AlgebraicNotation.ToAlgebraicNotation(move, chess);


        if (chess.MoveDictionary.ContainsKey(algebraicNotation) == false) {
            chess.MoveDictionary.Add(algebraicNotation, move);
        }
    }

    static void CommitMove(Game chess, Move move) {
        Piece endSquare = chess.board[move.endY, move.endX];
        Piece startSquare = chess.board[move.startY, move.startX];

        startSquare.type = PieceType.None;
        startSquare.color = Color.None;

        endSquare.type = move.piece;
        endSquare.color = move.pieceColor;

        if (move.isEnpassant) {
            chess.board[move.startY, move.endX].type = PieceType.None;
            chess.board[move.startY, move.endX].color = Color.None;
        }
    }

    static void UnCommitMove(Game chess, Move move) {
        Piece endSquare = chess.board[move.endY, move.endX];
        Piece startSquare = chess.board[move.startY, move.startX];
        Color opponentColor = move.pieceColor == Color.White ? Color.Black : Color.White;

        startSquare.type = move.piece;
        startSquare.color = move.pieceColor;

        endSquare.type = move.capturedPiece;
        if (move.isCapture) endSquare.color = opponentColor;
        else endSquare.color = Color.None;
       

        if (move.isEnpassant) {
            chess.board[move.startY, move.endX].type = PieceType.Pawn;
            chess.board[move.startY, move.endX].color = opponentColor;

            endSquare.type = PieceType.None;
            endSquare.color = Color.None;
        }
    }
}