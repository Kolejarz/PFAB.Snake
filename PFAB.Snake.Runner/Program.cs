using PFAB.Snake.Runner;

Console.WriteLine("Hello Snake!");
var game = new Game(32, 24);

do
{
    game.Render();
} while(game.ReadInput());