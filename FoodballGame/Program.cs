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
               PrintGame.PrintGameState(game);
                Thread.Sleep(10);

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
    }
}
