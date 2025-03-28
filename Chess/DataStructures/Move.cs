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

    /// <summary>
    /// Set Move data
    /// </summary>
    /// <param name="startY"></param>
    /// <param name="startX"></param>
    /// <param name="endY"></param>
    /// <param name="endX"></param>
    /// <param name="isCapture"></param>
    /// <param name="capturedPiece"></param>
    /// <param name="isCheck"></param>
    /// <param name="isCastle"></param>
    /// <param name="isEnpassant"></param>
    /// <param name="isPromotion"></param>
    /// <param name="promotionPiece"></param>
    public Move(PieceType piece, int startY, int startX, int endY, int endX, bool isCapture = false, PieceType capturedPiece = PieceType.None, bool isCheck = false, bool isCastle = false, bool isEnpassant = false, bool isPromotion = false, PieceType promotionPiece = PieceType.None) {
        this.startY = startY;
        this.startX = startX;
        this.endY = endY;
        this.endX = endX;
        this.piece = piece;
        
        this.isCapture = isCapture;
        this.capturedPiece = capturedPiece;
        this.isCheck = isCheck;
        this.isCastle = isCastle;
        this.isEnpassant = isEnpassant;
        this.isPromotion = isPromotion;
        this.promotionPiece = promotionPiece;
    }
}