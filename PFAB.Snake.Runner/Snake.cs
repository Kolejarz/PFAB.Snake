using PFAB.Snake.Runner.Primitives;

namespace PFAB.Snake.Runner;

internal class Snake
{
    private readonly Queue<Coordinate> _body = new ();
    private Direction _direction;

    public Direction Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            TakeStep(new Coordinate(Head.X + SnakeMoveVector.x, Head.Y + SnakeMoveVector.y));
        }
    }

    public Coordinate Head => _body.Last();
    public IEnumerable<Coordinate> Body => _body.ToArray().Reverse().Skip(1);

    public Snake(Coordinate startingPosition, Direction direction)
    {
        _body.Enqueue(startingPosition);
        Direction = direction;
    }

    public void AddSegment(Coordinate position)
    {
        _body.Enqueue(position);
    }

    public bool Occupies(Coordinate spotToCheck)
    {
        return _body.Contains(spotToCheck);
    }

    private void TakeStep(Coordinate nextMove)
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