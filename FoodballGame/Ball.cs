using System;

namespace FoodballGame
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }

        private double _vx, _vy;

        private Game _game;

        public Ball(double x, double y, Game game)
        {
            _game = game;
            X = x;
            Y = y;
        }

        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void SetSpeed(double vx, double vy)
        {
            _vx = vx;
            _vy = vy;
        }

        public void Move()
        {
            double newX = X + _vx;
            double newY = Y + _vy;

            if (newX < 0 || newX >= _game.Stadium.Width)
            {
                _vx = -_vx;
                newX = X + _vx;
            }

            if (newX < 0)
            {
                newX = 1;
            }
            else if (newX >= _game.Stadium.Width)
            {
                newX = _game.Stadium.Width - 1;
            }

            if (newY < 0 || newY >= _game.Stadium.Height)
            {
                _vy = -_vy;
                newY = Y + _vy;
            }

            if (newY < 0)
            {
                newY = 1;
            }
            else if (newY >= _game.Stadium.Height)
            {
                newY = _game.Stadium.Height - 1;
            }

            X = newX;
            Y = newY;
        }
    }
}