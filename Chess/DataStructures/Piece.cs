public enum PieceType {None, Pawn, Knight, Bishop, Rook, Queen, King};
public enum Color {Black, White, None}

public class Piece {
    public PieceType type;
    public Color color; 
    public int posY;
    public int posX;
    public void SetPiece(PieceType type, Color color, int posY, int posX) {
        this.type = type;
        this.color = color;
        this.posY = posY;
        this.posX = posX;
    }

    public void GetPossibleMoves(Game chess) {

    }
}