namespace PFAB.Snake.Runner;

internal class Snake
{
    private readonly Queue<(int x, int y)> _body = new ();
    private Direction _direction;

    public Direction Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            TakeStep((Head.x + SnakeMoveVector.x, Head.y + SnakeMoveVector.y));
        }
    }

    public (int x, int y) Head => _body.Last();
    public IEnumerable<(int x, int y)> Body => _body.ToArray().Reverse().Skip(1);

    public Snake((int x, int y) startingPosition, Direction direction)
    {
        _body.Enqueue(startingPosition);
        Direction = direction;
    }

    public void AddSegment((int x, int y) position)
    {
        _body.Enqueue(position);
    }

    private void TakeStep((int x, int y) nextMove)
    {
        if(_body.Count > 4)
        {
            _ = _body.Dequeue();
        }
        _body.Enqueue(nextMove);
    }

    private (int x, int y) SnakeMoveVector => Direction switch
    {
        Direction.Up => (0, -1),
        Direction.Down => (0, 1),
        Direction.Left => (-1, 0),
        Direction.Right => (1, 0),
        _ => throw new ArgumentOutOfRangeException()
    };
}