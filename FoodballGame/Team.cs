using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FoodballGame
{
    public class Team
    {
        public List<Player> Players { get; } = new List<Player>();
        public string Name { get; private set; }
        public Game Game { get; set; }

        public Team(string name)
        {
            Name = name;
        }

        public void StartGame(int width, int height)
        {
            Random rnd = new Random();
            foreach (var player in Players)
            {
                player.SetPosition(rnd.Next(1, width - 1), rnd.Next(1, height - 1));
                player.Team = this; // Устанавливаем команду игрока - Mängija meeskonna seadistamine
            }
        }

        public void AddPlayer(Player player)
        {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }

        public (double, double) GetBallPosition()
        {
            return Game.GetBallPositionForTeam(this);
        }

        public void SetBallSpeed(double vx, double vy)
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue;
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            return closestPlayer;
        }

        public void Move()
        {
            GetClosestPlayerToBall().MoveTowardsBall();
            Players.ForEach(player => player.Move());
        }
        public static void DrawField(int width, int height, List<Player> homePlayers, List<Player> awayPlayers, Ball ball)
        {
            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(0, y + 1);
                Console.Write("!");
                for (int x = 0; x < width; x++)
                {
                    if (Player.IsPlayerAtPosition(x, y, homePlayers))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("E");
                    }
                    else if (Player.IsPlayerAtPosition(x, y, awayPlayers))
                    {
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("T");
                    }
                    else if (Player.IsBallAtPosition(x, y, ball))
                    {
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" O ");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("!");
            }
        }
    }
}
