public enum PieceType {None, Pawn, Knight, Bishop, Rook, Queen, King};
public enum Color {Black, White, None}

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

    void GetPawnMove(Game chess) {
        int direction = this.color == Color.White ? 1 : -1;
        int startRow = this.color == Color.White ? 1 : 6;

        if (chess.board[posY + direction, posX].type == PieceType.None) {
            Move newMove = new Move(this.type, posY, posX, posY + direction, posX);
            Moves.AddMove(chess, newMove);
            if (posY == startRow && chess.board[posY + direction * 2, posX].type == PieceType.None) {
                Move newMove2 = new Move(this.type, posY, posX, posY + direction * 2, posX);
                Moves.AddMove(chess, newMove2);
            }
        }
         
        for (int dx = -1 ; dx <= 1; dx++) {
            int newY = this.posY + direction;
            int newX = this.posX + dx;
            if (newX >= 0 && newX < 8) {
                if (chess.board[newY, newX].type != PieceType.None && chess.board[newY, newX].color != this.color) {
                    Move newMove = new Move(this.type, posY, posX, newY, newX, true, chess.board[newY, newX].type);
                    Moves.AddMove(chess, newMove);
                } else if (chess.board[newY, newX].type == PieceType.None && chess.board[posY, newX].type == PieceType.Pawn && chess.board[posY, newX].color != this.color) {
                    // Add check if last move was it
                    Move newMove = new Move(this.type, posY, posX, newY, newX, true, PieceType.Pawn, false, false, true);
                    Moves.AddMove(chess, newMove);
                }
            }
        }
        
    }

    void GetNonPawnMoves(Game chess) {
        foreach (var (dx, dy, repeatable) in Moves.PieceMap[this.type]) {
            int newX = this.posX + dx;
            int newY = this.posY + dy;

            while (newX < 8 && newX >= 0 && newY < 8 && newY >= 0) {
                Piece landingSquare = chess.board[newY, newX];

                if (landingSquare.type == PieceType.None) {
                    Move newMove = new Move(this.type, posY, posX, newY, newX);
                    Moves.AddMove(chess, newMove);
                }

                else if (landingSquare.color != this.color) {
                    Move newMove = new Move(this.type, posY, posX, newY, newX, true, landingSquare.type);
                    Moves.AddMove(chess, newMove);
                    break;
                }

                else break;

                if (!repeatable) break;

                newY += dy;
                newX += dx;
            }
        }
    }

    public void GetPossibleMoves(Game chess) {
        if (this.type == PieceType.Pawn) GetPawnMove(chess);
        else GetNonPawnMoves(chess);
    }
}