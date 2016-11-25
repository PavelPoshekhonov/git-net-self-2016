////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Базовый класс двигающегося игрового объекта
////////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace TankWars.MVC.Model
{
    // Базовый класс двигающегося игрового объекта
    public class MovingObject : GameObject
    {
        // События
        public event EventHandler DirectionChanged;     // Изменение направления
        protected virtual void OnDirectionChanged(EventArgs e)
        {
            DirectionChanged?.Invoke(this, e);
        }

        // Поля
        Direction direction;
        public Direction Direction                      // Направление
        {
            get { return direction; }
            set
            {
                direction = value;

                // Вызов события отрисовки
                OnDirectionChanged(EventArgs.Empty);
            }
        }

        // Конструктор
        public MovingObject(Point pos, Size siz, Direction dir = Direction.Bottom) : base(pos, siz)
        {
            Direction = dir;
        }
    }
}
