using Spectre.Console;

namespace PFAB.Snake.Runner;

internal class Game
{
    private readonly int _width;
    private readonly int _height;
    private readonly int[,] _board;

    public Game(int width, int height)
    {
        _width = width;
        _height = height;
        _board = new int[width, height];
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

        AnsiConsole.Write(panel);
    }
}