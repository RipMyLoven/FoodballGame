using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodballGame
{
    public class Player
    {
        public string Name { get; } // Имя игрока
        public double X { get; private set; } // Текущая позиция игрока по оси X
        public double Y { get; private set; } // Текущая позиция игрока по оси Y

        // Скорость игрока по осям X и Y
        private double _vx, _vy;

        // Команда, в которой состоит игрок (по умолчанию null)
        public Team? Team { get; set; } = null;

        // Максимальная скорость передвижения игрока
        private const double MaxSpeed = 5;

        // Максимальная сила удара по мячу
        private const double MaxKickSpeed = 25;

        // Дистанция до мяча, на которой игрок может ударить по мячу
        private const double BallKickDistance = 10;

        // Генератор случайных чисел для определения случайной силы удара
        private Random _random = new Random();

        // Конструктор, который принимает имя игрока
        public Player(string name)
        {
            Name = name;
        }

        // Конструктор, который принимает имя игрока, его координаты и команду
        public Player(string name, double x, double y, Team team)
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        // Метод для установки позиции игрока на поле
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Возвращает абсолютную позицию игрока относительно команды
        public (double, double) GetAbsolutePosition()
        {
            // Используем метод игры для получения позиции игрока относительно команды
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        // Возвращает расстояние от игрока до мяча
        public double GetDistanceToBall()
        {
            var ballPosition = Team!.GetBallPosition(); // Получаем позицию мяча
            var dx = ballPosition.Item1 - X; // Разница по оси X между игроком и мячом
            var dy = ballPosition.Item2 - Y; // Разница по оси Y между игроком и мячом
            return Math.Sqrt(dx * dx + dy * dy); // Возвращаем расстояние до мяча
        }

        // Метод для движения игрока в сторону мяча
        public void MoveTowardsBall()
        {
            var ballPosition = Team!.GetBallPosition(); // Получаем позицию мяча
            var dx = ballPosition.Item1 - X; // Разница по оси X между игроком и мячом
            var dy = ballPosition.Item2 - Y; // Разница по оси Y между игроком и мячом
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed; // Рассчитываем коэффициент скорости
            _vx = dx / ratio; // Устанавливаем скорость по оси X
            _vy = dy / ratio; // Устанавливаем скорость по оси Y
        }

        // Метод для передвижения игрока
        public void Move()
        {
            // Если игрок не является ближайшим к мячу, он не двигается
            if (Team.GetClosestPlayerToBall() != this)
            {
                _vx = 0;
                _vy = 0;
            }

            // Если игрок находится достаточно близко к мячу, он может нанести удар
            if (GetDistanceToBall() < BallKickDistance)
            {
                // Устанавливаем случайную скорость мяча в направлении удара
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(), // Случайная скорость удара по X
                    MaxKickSpeed * (_random.NextDouble() - 0.5) // Случайная скорость удара по Y
                );
            }

            // Рассчитываем новые координаты игрока
            var newX = X + _vx;
            var newY = Y + _vy;

            // Получаем абсолютную позицию на поле относительно команды
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);

            // Если новая позиция игрока находится на поле, обновляем координаты
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                // Если позиция за пределами поля, останавливаем игрока
                _vx = _vy = 0;
            }
        }
    }
}
