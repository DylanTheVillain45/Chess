public class Game {
    public void NewGame() {

        Piece[,] board = Board.CreateBoard();
        Board.ShowBoard(board, Color.White);
        // Board.ShowBoard(board, Color.Black);


    }
}