////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Класс двигающегося игрового объекта: Пуля
////////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace TankWars.MVC.Model
{
    // Класс двигающегося игрового объекта: Пуля
    public class Bullet : MovingObject, IDisposable
    {
        // События
        public event EventHandler ActiveChanged;        // Запуск / Остановка пули
        protected virtual void OnActiveChanged(EventArgs e)
        {
            ActiveChanged?.Invoke(this, e);
        }

        // Поля
        bool active = false;
        public bool Active                              // Признак запущенной пули
        {
            get { return active; }
            set
            {
                active = value;

                // Вызов события
                OnActiveChanged(EventArgs.Empty);
            }
        }

        // Конструктор
        public Bullet(Point pos, Size siz, Direction dir = Direction.Bottom) : base(pos, siz, dir)
        {
            this.DirectionChanged += ThisDirectionChanged;

            ThisDirectionChanged(this, EventArgs.Empty); // Принудительное обновление
        }

        // Деструктор
        public void Dispose()
        {
            // Отмена обработчика
            this.DirectionChanged -= ThisDirectionChanged;
        }

        // Обработчик события "Изменение направления"
        public void ThisDirectionChanged(object sender, EventArgs e)
        {
            // Задаем размер объекта в зависимости от направления
            if ((Direction == Direction.Left) || (Direction == Direction.Right))
                Size = ObjectSize.BulletH;
            else
                Size = ObjectSize.BulletV;
        }

        // Рассчитать положение пули, в зависимости от положения выстрелевшего танка / колобка
        public static Point CalcLocation(MovingObject obj)
        {
            byte acc = 2;       // Отступ от выстрелевшего объекта

            switch (obj.Direction)
            {
                case Direction.Left:
                    return new Point(obj.Location.X - ObjectSize.BulletH.Width - acc,
                                     obj.Location.Y + obj.Size.Height / 2 - ObjectSize.BulletH.Height / 2);
                case Direction.Right:
                    return new Point(obj.Location.X + obj.Size.Width + acc,
                                     obj.Location.Y + obj.Size.Height / 2 - ObjectSize.BulletH.Height / 2);
                case Direction.Bottom:
                    return new Point(obj.Location.X + obj.Size.Width / 2 - ObjectSize.BulletV.Width / 2,
                                     obj.Location.Y + obj.Size.Height + acc);
                case Direction.Top:
                default:
                    return new Point(obj.Location.X + obj.Size.Width / 2 - ObjectSize.BulletV.Width / 2,
                                     obj.Location.Y - ObjectSize.BulletV.Height - acc);
            }

        }
    }
}
