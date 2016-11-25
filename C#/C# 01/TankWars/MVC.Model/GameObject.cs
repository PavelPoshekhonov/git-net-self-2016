////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Базовый класс статического игрового объекта
////////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace TankWars.MVC.Model
{
    // Базовый класс статического игрового объекта
    public class GameObject
    {
        // События
        public event EventHandler LocationChanged;      // Изменение положения
        protected virtual void OnLocationChanged(EventArgs e)
        {
            LocationChanged?.Invoke(this, e);
        }

        // Поля
        public string Name { get; set; } = "Game Object";   // Имя объектка
        Size size;
        public Size Size                                // Размер объекта
        {
            get { return size; }
            protected set { size = value; }
        }

        Point location;
        public Point Location                           // Положение объекта
        {
            get { return location; }
            set
            {
                location = value;

                // Вызов события отрисовки
                OnLocationChanged(EventArgs.Empty);
            }
        }

        // Конструктор
        public GameObject(Point pos, Size siz)
        {
            size = siz;
            Location = pos;
        }

        // Проверка на пересечение с объектом
        public bool CheckCrossing(Point loc, Size siz)
        {
            bool xCross = false;
            bool yCross = false;

            if ((Location.X + Size.Width >= loc.X) && (Location.X <= loc.X + siz.Width)) xCross = true;
            if ((Location.Y + Size.Height >= loc.Y) && (Location.Y <= loc.Y + siz.Height)) yCross = true;

            if (xCross && yCross)
                return true;
            else
                return false;
        }
    }
}
