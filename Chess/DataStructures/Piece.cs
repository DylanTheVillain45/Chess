using System.Diagnostics.CodeAnalysis;

public enum PieceType { None, Pawn, Knight, Bishop, Rook, Queen, King};
public enum Color {Black, White}
public class Piece {
    public PieceType type;
    public Color color; 
    public int posY;
    public int posX;

    /// <summary>
    /// Set the piece data
    /// </summary>
    /// <param name="type"></param>
    /// <param name="color"></param>
    /// <param name="posY"></param>
    /// <param name="posX"></param>
    public void SetPiece(PieceType type, Color color, int posY, int posX) {
        this.type = type;
        this.color = color;
        this.posY = posY;
        this.posX = posX;
    }
}