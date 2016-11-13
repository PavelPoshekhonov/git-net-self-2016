using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankWars
{
    // Направление движения
    public enum Direction { Left, Right, Top, Bottom }

    // Базовый класс для игровых объектов: Яблоко, Колобок, Танк, Пуля
    public class GameObject
    {
    }

    // Базовый класс для игровых объектов: Колобок, Танк, Пуля
    public class MovingObject : GameObject
    {
        // События
        public event EventHandler LocationChanged;
        protected virtual void OnLocationChanged(EventArgs e)
        {
            LocationChanged?.Invoke(this, e);
        }

        // Поля
        Size objectSize;
        public Size ObjectSize
        {
            get { return objectSize; }
        }

        Point location;
        public Point Location
        {
            get { return location; }
            set
            {
                OldLocation = location;
                location = value;
                LastMoving = DateTime.Now;

                // Вызов события отрисовки
                OnLocationChanged(EventArgs.Empty);
            }
        }
        public Point OldLocation { get; set; }          // Предыдущее положение объекта
        public DateTime LastMoving { get; set; }        // Время последнего перемещения
        public int MovementDelay { get; set; }          // Определяет как часто происходит перемещение объекта, мсек
        public Direction Course { get; set; }           // Направление

        // Конструктор
        public MovingObject(Rectangle pos, int mDelay, Direction cs = Direction.Bottom)
        {
            MovementDelay = mDelay;
            Course = cs;

            objectSize = pos.Size;
            location = pos.Location; // Попадет в OldPosition
            Location = location = pos.Location;
        }
    }


    // Колобок
    public class Kolobok : MovingObject
    {
        public int LifesLeft { get; set; }              // Оставшиеся жизни

        // Конструктор
        public Kolobok(Rectangle pos, int mDelay, Direction cs = Direction.Bottom) : base(pos, mDelay, cs)
        {
        }
    }

    // Танк
    public class Tank : MovingObject
    {
        // Конструктор
        public Tank(Rectangle pos, int mDelay, Direction cs = Direction.Bottom) : base(pos, mDelay, cs)
        {
        }
    }
}

