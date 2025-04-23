public class Move {
    public int startY;
    public int startX;
    public int endY;
    public int endX;

    public Piece piece;
    public Piece? capturedPiece;
    public PieceType promotionPiece;

    public bool isCapture;
    public bool isCheck; 
    public bool isShortCastle;
    public bool isCastle;
    public bool isEnpassant;
    public bool isPromotion;

    public bool isCheckMate;
    public bool isStaleMate;

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
    public Move(Piece piece, int startY, int startX, int endY, int endX, bool isCapture = false, Piece? capturedPiece = null, bool isCheck = false, bool isShortCastle = false, bool isEnpassant = false, bool isPromotion = false, PieceType promotionPiece = PieceType.None) {
        this.startY = startY;
        this.startX = startX;
        this.endY = endY;
        this.endX = endX;

        this.piece = piece;
        
        this.isCapture = isCapture;
        this.capturedPiece = capturedPiece;
        this.isCheck = isCheck;
        this.isShortCastle = isShortCastle;
        this.isEnpassant = isEnpassant;
        this.isPromotion = isPromotion;
        this.promotionPiece = promotionPiece;
    }
}