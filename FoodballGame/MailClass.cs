using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodballGame
{
    public class Program
    {
        static void ChangeConsoleColor(ConsoleColor foregroundColor)
        {
            Console.ForegroundColor = foregroundColor;

        }
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // loomine team ja stadium
            Team esimeneTeam = new Team("Esimene Team");
            Team teineTeam = new Team("Teine Team");
            Stadium stadium = new Stadium(70, 20);
            Game game = new Game(esimeneTeam, teineTeam, stadium);

            // loomine mängija
            for (int i = 1; i <= 10; i++)
            {
                Player player = new Player($"Player {i}");
                if (i <= 5)
                {
                    esimeneTeam.AddPlayer(player);
                }
                else
                {
                    teineTeam.AddPlayer(player);
                }
            }

            game.Start();
            Console.WindowWidth = stadium.Width + 2;
            Console.WindowHeight = stadium.Height + 1;

            while (true)
            {
                game.Move();
                Team.DrawField(stadium.Width, stadium.Height, esimeneTeam.Players, teineTeam.Players, game.Ball);
                Thread.Sleep(100);
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}
