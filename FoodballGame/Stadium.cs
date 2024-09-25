using System;

namespace FoodballGame
{
    public class Stadium
    {
        // Ширина и высота стадиона
        private int width, height;

        // Конструктор, который принимает ширину и высоту стадиона
        public Stadium(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        // Метод для получения ширины стадиона
        public int GetWidth()
        {
            return width;
        }

        // Метод для получения высоты стадиона
        public int GetHeight()
        {
            return height;
        }

        // Метод для проверки, находится ли объект внутри стадиона
        public bool IsIn(double x, double y)
        {
            // Выводим текущие координаты для проверки
            Console.WriteLine("in: " + x + ", " + y);

            // Проверяем, находятся ли координаты внутри границ стадиона
            return x >= 0 && x < width && y >= 0 && y < height;
        }
    }
}
