using System;
using System.Threading;

namespace FoodballGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Stadium stadion = new Stadium(40, 20);

            Team TeamA = new ("Team A");
            Team TeamB = new ("Team B");

            for (int i = 0; i < 11; i++)
            {
                TeamA.AddPlayer(new Player($"TeamA{i + 1}"));
                TeamB.AddPlayer(new Player($"TeamB{i + 1}"));
            }

            Game game = new Game(TeamA, TeamB, stadion);
            game.Start();

            while (true)
            {
                game.Move();
                PrintGameState(game);
                Thread.Sleep(100);

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
            foreach (var player in game.TeamA.Players)
            {
                int playerX = (int)player.X;
                int playerY = (int)player.Y;
                if (playerX >= 0 && playerX < width && playerY >= 0 && playerY < height)
                {
                    field[playerY, playerX] = 'A';
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var player in game.TeamB.Players)
            {
                int playerX = (int)player.X;
                int playerY = (int)player.Y;
                if (playerX >= 0 && playerX < width && playerY >= 0 && playerY < height)
                {
                    field[playerY, playerX] = 'B';
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
            Console.WriteLine($"Score: {game.TeamA.Name} {game.TeamA.Score} - {game.TeamB.Score} {game.TeamB.Name}\n");

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (field[y, x] == 'A')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (field[y, x] == 'B')
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
