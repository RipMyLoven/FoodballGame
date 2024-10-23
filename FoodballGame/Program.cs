namespace FoodballGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Stadium stadium = new Stadium(40, 20);

            Team homeTeam = new Team("Home");
            Team awayTeam = new Team("Away");

            for (int i = 0; i < 11; i++)
            {
                homeTeam.AddPlayer(new Player($"HomePlayer{i + 1}"));
                awayTeam.AddPlayer(new Player($"AwayPlayer{i + 1}"));
            }

            Game game = new Game(homeTeam, awayTeam, stadium);
            game.Start();

            while (true)
            {
                game.Move();
                PrintGameState(game);
                Thread.Sleep(1);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
        }

        static void PrintGameState(Game game)
        {
            Console.Clear();

            int width = game.Stadium.Width;
            int height = game.Stadium.Height;
            char[,] field = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    field[y, x] = ' ';
                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var player in game.HomeTeam.Players)
            {
                int playerX = (int)player.X;
                int playerY = (int)player.Y;
                if (playerX >= 0 && playerX < width && playerY >= 0 && playerY < height)
                {
                    field[playerY, playerX] = 'H';
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var player in game.AwayTeam.Players)
            {
                int playerX = (int)player.X;
                int playerY = (int)player.Y;
                if (playerX >= 0 && playerX < width && playerY >= 0 && playerY < height)
                {
                    field[playerY, playerX] = 'A';
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            int ballX = (int)game.Ball.X;
            int ballY = (int)game.Ball.Y;
            if (ballX >= 0 && ballX < width && ballY >= 0 && ballY < height)
            {
                field[ballY, ballX] = 'O';
            }

            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0; y < height; y++)
            {
                field[y, 0] = '|';
                field[y, width - 1] = '|';
            }

            for (int x = 0; x < width; x++)
            {
                field[0, x] = '-';
                field[height - 1, x] = '-';
            }

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = -2; i <= 2; i++)
            {
                if (height / 2 + i >= 0 && height / 2 + i < height)
                {
                    field[height / 2 + i, 0] = 'G';
                    field[height / 2 + i, width - 1] = 'G';
                }
            }

            Console.ResetColor();
            Console.WriteLine($"Score: {game.HomeTeam.Name} {game.HomeTeam.Score} - {game.AwayTeam.Score} {game.AwayTeam.Name}\n");

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (field[y, x] == 'H')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (field[y, x] == 'A')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (field[y, x] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (field[y, x] == 'G')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(field[y, x] + " ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
