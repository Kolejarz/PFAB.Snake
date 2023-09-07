using PFAB.Snake.Runner;

var game = new Game(21, 21);
await game.Run();

Console.WriteLine($"GAME FINISHED! Your score: {game.Score}");