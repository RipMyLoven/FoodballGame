using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodballGame
{
    public class Ball
    {
        // Позиция мяча на поле
        private int x, y;

        // Текущая скорость мяча по осям x и y
        private double dx = 0, dy = 0;

        // Коэффициент замедления скорости, имитирующий трение или сопротивление
        private double speedCoefficient = 0.9;

        // Ссылка на объект игры для взаимодействия с полем (стадионом)
        private Game game;

        // Конструктор класса Ball. Инициализирует начальные координаты мяча и связывает его с игрой.
        public Ball(int x, int y, Game game)
        {
            this.x = x; // Начальная позиция по оси x
            this.y = y; // Начальная позиция по оси y
            this.game = game; // Привязка мяча к объекту игры
        }

        // Метод для получения текущей позиции мяча по оси x
        public int getX()
        {
            return x;
        }

        // Метод для получения текущей позиции мяча по оси y
        public int getY()
        {
            return y;
        }

        // Метод для установки скорости мяча
        public void setSpeed(double dx, double dy)
        {
            this.dx = dx; // Устанавливаем скорость по оси x
            this.dy = dy; // Устанавливаем скорость по оси y
        }

        // Метод для проверки, находится ли мяч на поле
        public bool isIn()
        {
            // Проверяем, находится ли мяч внутри стадиона
            return game.getStadium().isIn(x, y);
        }

        // Метод, отвечающий за движение мяча
        public void move()
        {
            // Если мяч вышел за пределы стадиона, остановить его
            if (!isIn())
            {
                dx = 0; // Останавливаем движение по оси x
                dy = 0; // Останавливаем движение по оси y
            }

            // Обновляем позицию мяча на основе текущей скорости
            x += (int)dx; // Двигаем мяч по оси x
            y += (int)dy; // Двигаем мяч по оси y

            // Применяем замедление к скорости для имитации трения
            dx *= speedCoefficient; // Постепенное уменьшение скорости по оси x
            dy *= speedCoefficient; // Постепенное уменьшение скорости по оси y
        }
    }
}
