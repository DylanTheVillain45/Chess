using System;

class Program {
    static void Main(string[] args) {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Game chess = new Game();

        chess.NewGame();
    }
}