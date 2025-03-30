public class Game {
    public Dictionary<String, Move> MoveDictionary = new Dictionary<String, Move>();
    public List<(String, String)> PastMoves = new List<(string, string)>();
    public Piece[,] board = new Piece[8,8];

    /// <summary>
    /// Game Loop
    /// </summary>
    public void NewGame() {
        Board.FillBoard(board);
        Color color = Color.White;
        Board.ShowBoard(board, color);

        while (true) {
            MoveGenerator.GetMoves(this, color);
            Board.ShowMoves(MoveDictionary);
            break;
        }


    }
}