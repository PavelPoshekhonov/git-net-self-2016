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
        protected Size size;
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
            int Blank = 7;
            Point pntWall = new Point();
            Size sizWall = new Size();

            // Карта уровня 01 не должна быть меньше чем 500 x 500
            size = new Size(Math.Max(size.Width, 500), Math.Max(size.Height, 500));

            xStep = Size.Width / 13;

            // Создаем вертикальные стены
            for (int i = 1; i <= 12; i += 2)
            {
                pntWall.X = Convert.ToInt32(i * xStep);
                sizWall.Width = ObjectSize.CommonSize.Width;
                if ((i == 5) || (i == 7))
                    sizWall.Height = Convert.ToInt32(Size.Height / 2 - ObjectSize.CommonSize.Height * 4) - Blank;
                else
                    sizWall.Height = Convert.ToInt32(Size.Height / 2 - ObjectSize.CommonSize.Height * 3) - Blank;

                pntWall.Y = ObjectSize.CommonSize.Height + Blank;
                WallList.Add(new Wall(pntWall, sizWall));

                if ((i == 5) || (i == 7))
                    pntWall.Y = Convert.ToInt32(Size.Height / 2 + ObjectSize.CommonSize.Height * 1);
                else
                    pntWall.Y = Convert.ToInt32(Size.Height / 2 + ObjectSize.CommonSize.Height * 2);
                WallList.Add(new Wall(pntWall, sizWall));
            }

            // Создаем горизонтальные стены
            for (int i = 0; i < 12; i += 2)
            {
                pntWall.X = Convert.ToInt32((i + 1) * xStep);
                pntWall.Y = Convert.ToInt32(Size.Height / 2 - ObjectSize.CommonSize.Height * 0.5);
                sizWall.Width = ObjectSize.CommonSize.Width;
                sizWall.Height = ObjectSize.CommonSize.Height;

                if ((i == 0) || (i == 2)) // Положение 
                {
                    pntWall.X = Convert.ToInt32(i * xStep);
                }
                if ((i == 10)) // Положение 
                {
                    pntWall.X = Size.Width - ObjectSize.CommonSize.Width;
                }

                if ((i == 4) || (i == 6) ) // Центральные - выше
                {
                    pntWall.Y = Convert.ToInt32(Size.Height / 2 - ObjectSize.CommonSize.Height * 1.5);
                }

                if ((i == 2) || (i == 8)) // Широкие
                {
                    sizWall.Width = ObjectSize.CommonSize.Width * 2;
                }

                WallList.Add(new Wall(pntWall, sizWall));
            }
        }
    }
}
