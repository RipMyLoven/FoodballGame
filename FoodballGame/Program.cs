using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodballGame
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Создание команд и стадиона
            Team esimeneTeam = new Team("Esimene tiim");
            Team teineTeam = new Team("Teine tiim");
            Stadium stadium = new Stadium(70, 20);
            Game game = new Game(esimeneTeam, teineTeam, stadium);

            for (int i = 1; i <= 12; i++)
            {
                Player player = new Player($"Player {i}");
                if (i <= 6)
                {
                    esimeneTeam.AddPlayer(player);
                }
                else
                {
                    teineTeam.AddPlayer(player);
                }
            }

            game.Start();
            Console.WindowWidth = stadium.Width + 3;
            Console.WindowHeight = stadium.Height + 1;
            while (true)
            {
                game.Move();
                Team.DrawField(stadium.Width, stadium.Height, esimeneTeam, teineTeam, game.Ball);
                Thread.Sleep(100);
                Console.SetCursorPosition(0,0);
            }
        }
    }
}