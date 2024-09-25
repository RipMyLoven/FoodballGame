using System;

namespace FoodballGame
{
    public class Game
    {
        // Команды: домашняя и гостевая
        private Team homeTeam;
        private Team awayTeam;

        // Стадион, на котором проходит игра
        private Stadium stadium;

        // Мяч, используемый в игре
        private Ball ball;

        // Конструктор, принимающий домашнюю команду, гостевую команду и стадион
        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            this.stadium = stadium;

            // Устанавливаем ссылку на текущую игру для обеих команд
            homeTeam.SetGame(this);
            awayTeam.SetGame(this);
        }

        // Метод для начала игры
        public void Start()
        {
            // Устанавливаем мяч в центр стадиона
            ball = new Ball(stadium.GetWidth() / 2, stadium.GetHeight() / 2, this);

            // Инициализируем позиции игроков для домашней команды
            homeTeam.InitializePositions(stadium.GetWidth() / 2, stadium.GetHeight());

            // Инициализируем позиции игроков для гостевой команды
            awayTeam.InitializePositions(stadium.GetWidth() / 2, stadium.GetHeight());
        }

        // Возвращает домашнюю команду
        public Team GetHomeTeam()
        {
            return homeTeam;
        }

        // Возвращает гостевую команду
        public Team GetAwayTeam()
        {
            return awayTeam;
        }

        // Возвращает стадион
        public Stadium GetStadium()
        {
            return stadium;
        }

        // Возвращает мяч
        public Ball GetBall()
        {
            return ball;
        }

        // Получает позицию для гостевой команды, зеркально относительно стадиона
        public Position GetPositionForAwayTeam(double x, double y)
        {
            return new Position(stadium.GetWidth() - x, stadium.GetHeight() - y);
        }

        // Возвращает позицию мяча для команды
        public Position GetBallPositionForTeam(Team team)
        {
            if (team == homeTeam)
            {
                return new Position(ball.GetX(), ball.GetY());
            }
            // Для гостевой команды позиции зеркально отображены
            return GetPositionForAwayTeam(ball.GetX(), ball.GetY());
        }

        // Устанавливает скорость мяча в зависимости от команды
        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == awayTeam)
            {
                // Для гостевой команды скорость мяча зеркальна
                ball.SetSpeed(-vx, -vy);
            }
            else
            {
                // Для домашней команды скорость мяча обычная
                ball.SetSpeed(vx, vy);
            }
        }

        // Метод для обновления состояния игры (каждый "тик" времени)
        public void Tick()
        {
            // Передвигаем игроков домашней и гостевой команд, а также мяч
            homeTeam.Move();
            awayTeam.Move();
            ball.Move();
        }
    }
}
