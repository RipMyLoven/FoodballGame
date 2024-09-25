using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodballGame
{
    public class Team
    {
        // Список игроков в команде
        public List<Player> Players { get; } = new List<Player>();

        // Имя команды
        public string Name { get; private set; }

        // Ссылка на игру, в которой участвует команда
        public Game Game { get; set; }

        // Конструктор, который инициализирует команду с её названием
        public Team(string name)
        {
            Name = name;
        }

        // Устанавливает случайные позиции для игроков на поле
        public void StartGame(int width, int height)
        {
            Random rnd = new Random(); // Создаем объект для генерации случайных чисел
            foreach (var player in Players)
            {
                // Для каждого игрока устанавливаются случайные координаты в пределах поля
                player.SetPosition(
                    rnd.NextDouble() * width,  // Случайная позиция по оси x
                    rnd.NextDouble() * height  // Случайная позиция по оси y
                );
            }
        }

        // Добавляет игрока в команду
        public void AddPlayer(Player player)
        {
            // Если игрок уже состоит в команде, то ничего не делать
            if (player.Team != null) return;

            // Добавляем игрока в список игроков команды
            Players.Add(player);

            // Устанавливаем команду для игрока
            player.Team = this;
        }

        // Возвращает текущую позицию мяча (для команды)
        public (double, double) GetBallPosition()
        {
            // Получаем позицию мяча через объект Game
            return Game.GetBallPositionForTeam(this);
        }

        // Устанавливает скорость мяча для команды
        public void SetBallSpeed(double vx, double vy)
        {
            // Устанавливаем скорость мяча через объект Game
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        // Возвращает игрока, который находится ближе всех к мячу
        public Player GetClosestPlayerToBall()
        {
            // Начально предположим, что первый игрок в списке — самый близкий
            Player closestPlayer = Players[0];

            // Задаем максимальное возможное расстояние
            double bestDistance = Double.MaxValue;

            // Проходим по каждому игроку команды
            foreach (var player in Players)
            {
                // Получаем расстояние игрока до мяча
                var distance = player.GetDistanceToBall();

                // Если расстояние меньше текущего лучшего, обновляем ближайшего игрока
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            // Возвращаем ближайшего игрока к мячу
            return closestPlayer;
        }

        // Движение команды
        public void Move()
        {
            // Игрок, ближайший к мячу, двигается к мячу
            GetClosestPlayerToBall().MoveTowardsBall();

            // Каждый игрок в команде совершает свое движение
            Players.ForEach(player => player.Move());
        }
    }
}
