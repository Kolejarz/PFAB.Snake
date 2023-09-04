using Spectre.Console;

namespace PFAB.Snake.Runner;

internal class Game
{
    private readonly int _width;
    private readonly int _height;
    private readonly int[,] _board;
    private readonly Snake _snake;

    public Game(int width, int height)
    {
        _width = width;
        _height = height;
        _board = new int[width, height];
        _snake = new Snake((width / 2, height / 2), Direction.Right);
    }

    public void Render()
    {
        var canvas = new Canvas(_width, _height);
        var panel = new Panel(canvas);

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                canvas.SetPixel(x, y, _board[x, y] == 0 ? Color.Grey15 : Color.Aqua);
            }
        }

        canvas.SetPixel(_snake.Head.x, _snake.Head.y, Color.Green);
        foreach (var (x, y) in _snake.Body)
        {
            canvas.SetPixel(x, y, Color.Red);
        }

        AnsiConsole.Write(panel);
    }

    private (int x, int y) SnakeMoveVector => _snake.Direction switch
    {
        Direction.Up => (0, -1),
        Direction.Down => (0, 1),
        Direction.Left => (-1, 0),
        Direction.Right => (1, 0),
        _ => throw new ArgumentOutOfRangeException()
    };
}