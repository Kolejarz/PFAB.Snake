namespace PFAB.Snake.Runner;

internal class Snake
{
    private readonly Queue<(int x, int y)> _body = new ();

    public Direction Direction { get; set; }
    public (int x, int y) Head => _body.Peek();
    public (int x, int y)[] Body => _body.Skip(1).ToArray();

    public Snake((int x, int y) startingPosition, Direction direction)
    {
        Direction = direction;
        _body.Enqueue(startingPosition);
    }

    public void TakeStep((int x, int y) nextMove)
    {
        if(_body.Count > 3)
        {
            _ = _body.Dequeue();
        }
        _body.Enqueue(nextMove);
    }
}