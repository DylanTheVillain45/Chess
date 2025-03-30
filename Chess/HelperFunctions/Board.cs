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

    /// <summary>
    /// Fill Board with starting pieces
    /// </summary>
    /// <param name="board"></param>
    public static void FillBoard(Piece?[,] board) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Piece? piece = new Piece();
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
                    piece = null;
                }
                board[i, j] = piece;
            }
        }
    }


    /// <summary>
    /// Show board, different orientation depending on color
    /// </summary>
    /// <param name="board"></param>
    /// <param name="color"></param>
    public static void ShowBoard(Piece?[,] board, Color color) {
        Console.WriteLine("----------------------------");
        if (color == Color.White) {
            for (int i = 7; i >= 0; i--) {
                Console.Write($"{i + 1} ");
                for (int j = 7; j >= 0; j--) {
                    Piece? piece = board[i, j];
                    if (piece != null) {
                        char unicodeChar = CharacterMap[(piece.type, piece.color)];
                        // Console.Write($" {piece.type.ToString().ToCharArray()[0]}{piece.color.ToString().ToCharArray()[0]}{piece.posY}{piece.posX} ");
                        Console.Write($" {unicodeChar} ");
                    } else {
                        Console.Write($" {chessSquares[1 - (i + j) % 2]} ");
                    }
                }
                Console.Write(" |");
                Console.WriteLine();
            }
            Console.WriteLine("---a--b--c--d--e--f--g--h---");
        } else {
            for (int i = 0; i < 8; i++) {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < 8; j++) {
                    Piece? piece = board[i, j];
                    if (piece != null) {
                        char unicodeChar = CharacterMap[(piece.type, piece.color)];
                        // Console.Write($" {piece.type.ToString().ToCharArray()[0]}{piece.color.ToString().ToCharArray()[0]}{piece.posY}{piece.posX} ");
                        Console.Write($" {unicodeChar} ");
                    } else {
                        Console.Write($" {chessSquares[1 - (i + j) % 2]} ");
                    }
                }
                Console.Write(" |");
                Console.WriteLine();
            }
            Console.WriteLine("---h--g--f--e--d--c--b--a---");
        }
    }

    public static void ShowMoves(Dictionary<string, Move> moveDictionary) {
        foreach (var (moveNot, move) in moveDictionary) {
            Console.Write($"{moveNot}, ");
        }
        Console.WriteLine();
    }

    public static string GetPlayerMove(Game chess, bool canShow = true) {
        while(true) {
            string? move = Console.ReadLine();

            if (move != null) {
                if (chess.MoveDictionary.ContainsKey(move) || move == "end" || (move == "show" && canShow)) {
                    return (string)move;
                }
                else {
                    Console.WriteLine($"Try Again, {move} is not a vaild move");
                }
            }
        }
    }
}