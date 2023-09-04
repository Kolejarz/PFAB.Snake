using PFAB.Snake.Runner;

Console.WriteLine("Hello Snake!");
var game = new Game(16, 16);

do
{
    game.Render();
} while(game.ReadInput());