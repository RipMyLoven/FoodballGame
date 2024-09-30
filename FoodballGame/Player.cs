using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodballGame
{
    public class Player
    {

        //väljud
        public string Name { get; } //mängija nimi
        public double X { get; private set; } //mängija x koordinaat
        public double Y { get; private set; } //mängija y koordinaat
        private double _vx, _vy;  //mängija ja palli kaugus
        public Team? Team { get; set; } = null; //meeskond, kus mängija mängib

        private const double MaxSpeed = 1; //maksimaalse mängija kiirus
        private const double MaxKickSpeed = 15; //max löögikiirus
        private const double BallKickDistance = 4; //löögikaugus

        private Random _random = new Random(); //juhuslik arv

        //konstruktorid
        public Player(string name) //sõltub sõnast ja sõne võrdleb Nimega
        {
            Name = name;
        }

        public Player(string name, double x, double y, Team team)
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        public void SetPosition(double x, double y) //määrata mängija koordinaadid
        {
            X = x;
            Y = y;
        }

        public (double, double) GetAbsolutePosition() //Saada absoluutne asukoht
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        public double GetDistanceToBall() //Saage pallile kaugus
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void MoveTowardsBall() //Liikumine pallile
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            _vx = dx / ratio;
            _vy = dy / ratio;
        }

        public void Move()
        {
            if (Team.GetClosestPlayerToBall() != this)
            {
                _vx = 0;
                _vy = 0;
            }

            if (GetDistanceToBall() < BallKickDistance)
            {
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(),
                    MaxKickSpeed * (_random.NextDouble() - 0.1)
                    );
            }

            var newX = X + _vx;
            var newY = Y + _vy;
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = _vy = 0;
            }
        }
        public static bool IsPlayerAtPosition(int x, int y, List<Player> players)
        {
            // проходим по списку игроков в команде.
            foreach (var player in players)
            {
                // Округляем координаты игрока до целых чисел.
                int playerX = (int)Math.Round(player.X);
                int playerY = (int)Math.Round(player.Y);

                // сравниваем координаты игрока с указанными x y
                if (playerX == x && playerY == y)
                {
                    return true; // возвращаем true если игрок находится в указанных координатах
                }
            }

            return false; // если ни один игрок не находится в указанных координатах возвращаем false
        }

        // проверяет, есть ли мяч в указанных координатах x y на игровом поле
        public static bool IsBallAtPosition(int x, int y, Ball ball)
        {
            // Округляем координаты мяча до целых чисел.
            int ballX = (int)Math.Round(ball.X);
            int ballY = (int)Math.Round(ball.Y);

            // сравниваем координаты мяча с указанными x y
            return ballX == x && ballY == y; // возвращаем true если мяч находится в указанных координатах иначе false
        }
    }
}
