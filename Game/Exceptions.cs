namespace Game;

public class InvalidMoveException : Exception{
    public InvalidMoveException() : base("Invalid move selected.") {}
    public InvalidMoveException(string message) : base(message){}
}