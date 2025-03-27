public static class Board {
    static char[] chessSquares = { '□', '■' };
    readonly static Dictionary<(PieceType, Color), char> CharacterMap = new Dictionary<(PieceType, Color), char>
    {
        { (PieceType.Pawn, Color.White), '♟' }, { (PieceType.Knight, Color.White), '♞' },
        { (PieceType.Bishop, Color.White), '♝' }, { (PieceType.Rook, Color.White), '♜' },
        { (PieceType.Queen, Color.White), '♛' }, { (PieceType.King, Color.White), '♚' },

        { (PieceType.Pawn, Color.Black), '♙' }, { (PieceType.Knight, Color.Black), '♘' },
        { (PieceType.Bishop, Color.Black), '♗' }, { (PieceType.Rook, Color.Black), '♖' },
        { (PieceType.Queen, Color.Black), '♕' }, { (PieceType.King, Color.Black), '♔' },
    };


    static void FillBoard(Piece[,] board) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Piece piece = new Piece();
                if (i == 1 || i == 6) {
                    Color color = i == 1 ? Color.White : Color.Black;
                    piece.SetPiece(PieceType.Pawn, color, i, j);
                } else if (i == 0 || i == 7) {
                    Color color = i == 0 ? Color.White : Color.Black;
                    PieceType pieceType;
                    if (j == 0 || j == 7) pieceType = PieceType.Rook;
                    else if (j == 1 || j == 6) pieceType = PieceType.Knight;
                    else if (j == 2 || j == 5) pieceType = PieceType.Bishop;
                    else if (j == 3) pieceType = PieceType.King;
                    else pieceType = PieceType.Queen;

                    piece.SetPiece(pieceType, color, i, j);
                } else {
                    piece.SetPiece(PieceType.None, Color.None, i, j);
                }
                board[i, j] = piece;
            }
        }
    }



    public static void ShowBoard(Piece[,] board, Color color) {
        if (color == Color.White) {
            for (int i = 7; i >= 0; i--) {
                for (int j = 7; j >= 0; j--) {
                    if (board[i, j].type != PieceType.None) {
                        char unicodeChar = CharacterMap[(board[i, j].type, board[i, j].color)];
                        Console.Write($" {unicodeChar} ");
                    } else {
                        Console.Write($" {chessSquares[1 - (i + j) % 2]} ");
                    }
                }
                Console.WriteLine();
            }
        } else {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (board[i, j].type != PieceType.None) {
                        char unicodeChar = CharacterMap[(board[i, j].type, board[i, j].color)];
                        Console.Write($" {unicodeChar} ");
                    } else {
                        Console.Write($" {chessSquares[1 - (i + j) % 2]} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public static Piece[,] CreateBoard() {
        Piece[,] board = new Piece[8,8];

        FillBoard(board);

        return board;
    }
}