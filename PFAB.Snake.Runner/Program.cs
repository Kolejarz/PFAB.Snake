using PFAB.Snake.Runner;

var game = new Game(8, 8);

do
{
    game.Render();
} while(game.ReadInput());

Console.WriteLine($"GAME FINISHED! Your score: {game.Score}");