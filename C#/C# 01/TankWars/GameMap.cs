////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Класс карты игры
////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;

namespace TankWars
{
    // Базовый класс карт игры
    class GameMap
    {
        Size size;
        public Size Size                                // Размер карты
        {
            get { return size; }
        }

        public List<Wall> WallList = new List<Wall>();  // Массив стен

        // Конструктор
        public GameMap(Size siz)
        {
            size = siz;
        }
    }

    // Карта уровня 01
    class GameMap01 : GameMap
    {
        public GameMap01(Size siz) : base(siz)
        {
            double xStep;

            if (Size.Width < 13 * ObjectSize.CommonSize.Width) return;

            xStep = Size.Width / 13;

            // Создаем вертикальные стены
            for (int i = 1; i <= 6; i++)
            {
                WallList.Add(new Wall(new Point(Convert.ToInt32(i * xStep), ObjectSize.CommonSize.Height),
                             new Size(ObjectSize.CommonSize.Width, Convert.ToInt32(Size.Height / 2 - ObjectSize.CommonSize.Height * 1.5))));
            }
        }
    }
}
