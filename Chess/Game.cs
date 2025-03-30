public class Game {
    public Dictionary<string, Move> MoveDictionary = new Dictionary<string, Move>();
    public List<(string, string)> PastMoves = new List<(string, string)>();
    public Piece?[,] board = new Piece?[8,8];
    public Color color;

    /// <summary>
    /// Game Loop
    /// </summary>
    public void NewGame() {
        Board.FillBoard(board);
        color = Color.White;

        while (true) {
            Console.WriteLine("\x1b[3J"); 
            Console.Clear();  
            Board.ShowBoard(board, color);
            Console.WriteLine();
            MoveDictionary.Clear();
            MoveGenerator.GetMoves(this);

            string playerNotationMove = Board.GetPlayerMove(this);
            if (playerNotationMove == "show") {
                Board.ShowMoves(MoveDictionary);
                playerNotationMove = Board.GetPlayerMove(this, false);
            }
            if (playerNotationMove == "end") break;

            Moves.CommitMove(this, MoveDictionary[playerNotationMove]);

            color = color == Color.White ? Color.Black : Color.White;
            MoveDictionary = new Dictionary<string, Move>();
            Console.ReadLine();
        }
    }
}