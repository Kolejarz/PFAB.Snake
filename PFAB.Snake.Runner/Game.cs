using System.Security.Cryptography.X509Certificates;
using Spectre.Console;

namespace PFAB.Snake.Runner;

internal class Game
{
    private readonly int _width;
    private readonly int _height;
    private readonly int[,] _board;
    private readonly Snake _snake;
    private readonly IList<(int x, int y)> _apples = new List<(int x, int y)>();

    public int Score { get; private set; }

    public Game(int width, int height)
    {
        _width = width;
        _height = height;
        _board = new int[width, height];
        _snake = new Snake((width / 2, height / 2), Direction.Right);
        _apples.Add(RandomEmptySpot());
    }

    public void Render()
    {
        AnsiConsole.Cursor.SetPosition(0, 0);
        var canvas = new Canvas(_width, _height);
        var panel = new Panel(canvas);

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                canvas.SetPixel(x, y, _board[x, y] == 0 ? Color.Grey15 : Color.Aqua);
            }
        }

        foreach (var (x, y) in _apples)
        {
            canvas.SetPixel(x, y, Color.Red);
        }

        foreach (var (x, y) in _snake.Body)
        {
            canvas.SetPixel(x, y, Color.Green);
        }
        canvas.SetPixel(_snake.Head.x, _snake.Head.y, Color.LightGreen);

        AnsiConsole.WriteLine($"SCORE: {Score}");
        AnsiConsole.Write(panel);
    }

    public bool ReadInput()
    {
        var key = Console.ReadKey().Key;
        // user closed game
        if (key == ConsoleKey.Escape) return false;

        var direction = key switch
        {
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            _ => _snake.Direction
        };

        if (direction == Direction.Right && _snake.Direction == Direction.Left ||
            direction == Direction.Left && _snake.Direction == Direction.Right ||
            direction == Direction.Down && _snake.Direction == Direction.Up ||
            direction == Direction.Up && _snake.Direction == Direction.Down)
        {
            direction = _snake.Direction;
        }

        _snake.Direction = direction;

        // snake left board
        if (_snake.Head.x < 0 || _snake.Head.y < 0 || _snake.Head.x >= _width || _snake.Head.y >= _height)
        {
            return false;
        }

        // snake collided with itself
        if (_snake.Body.Contains(_snake.Head))
        {
            return false;
        }

        var appleGained = _apples.IndexOf(_snake.Head);
        if (appleGained >= 0)
        {
            Score++;
            _snake.AddSegment(_apples[appleGained]);
            _apples.RemoveAt(appleGained);
            _apples.Add(RandomEmptySpot());
        }

        return true;
    }

    private (int x, int y) RandomEmptySpot()
    {
        var availableSpots = new List<(int x, int y)>();

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_board[x, y] != 0) continue;
                if (_apples.Contains((x, y))) continue;
                if(_snake.Occupies((x, y))) continue;
                availableSpots.Add((x, y));
            }
        }

        return availableSpots.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
    }
}