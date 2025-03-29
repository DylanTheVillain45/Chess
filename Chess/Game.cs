public class Game {
    public Dictionary<String, Move> MoveDictionary = new Dictionary<String, Move>();
    public Piece[,] board = new Piece[8,8];

    /// <summary>
    /// Game Loop
    /// </summary>
    public void NewGame() {
        Board.FillBoard(board);
        Color color = Color.White;
        Board.ShowBoard(board, color);

        while (true) {
            Moves.GetMoves(this, color);
            Board.ShowMoves(MoveDictionary);
            break;
        }


    }
}