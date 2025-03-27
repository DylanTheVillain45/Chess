public class Move {
    public int startY;
    public int startX;
    public int endY;
    public int endX;

    public PieceType piece;
    public PieceType capturedPiece;
    public PieceType promotionPiece;

    public bool isCapture;
    public bool isCheck; 
    public bool isCastle;
    public bool isEnpassant;
    public bool isPromotion;

    public void SetMove(int startY, int startX, int endY, int endX, bool isCapture = false, PieceType capturedPiece = PieceType.None, bool isCheck = false, bool isCastle = false, bool isEnpassant = false, bool isPromotion = false, PieceType promotionPiece = PieceType.None) {
        this.startY = startY;
        this.startX = startX;
        this.endY = endY;
        this.endX = endX;

        this.isCapture = isCapture;
        this.capturedPiece = PieceType.None;
        this.isCheck = isCheck;
        this.isCastle = isCastle;
        this.isEnpassant = isEnpassant;
        this.isPromotion = isPromotion;
        this.promotionPiece = PieceType.None;
    }
}