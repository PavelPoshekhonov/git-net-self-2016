////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Классы сущностей игровых объектов:
// Стена, Яблоко, Колобок, Танк, Пуля
////////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace TankWars
{
    // Направление движения
    public enum Direction { Left, Right, Top, Bottom }

    // Размеры игровых сущностей
    public class ObjectSize
    {
        // Размеры Стены, Яблока, Колобка, Танка
        public static readonly Size CommonSize = new Size(28, 28);
        // Размеры Пули
        public static readonly Size BulletH = new Size(10, 4);
        public static readonly Size BulletV = new Size(4, 10);
    }


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

            if ((Location.X + Size.Width  >= loc.X) && (Location.X <= loc.X + siz.Width))  xCross = true;
            if ((Location.Y + Size.Height >= loc.Y) && (Location.Y <= loc.Y + siz.Height)) yCross = true;

            if (xCross && yCross)
                return true;
            else
                return false;
        }
    }

    // Класс статического игрового объекта: Стена
    public class Wall : GameObject
    {
        // Конструктор
        public Wall(Point pos, Size siz) : base(pos, siz) { }
    }

    // Класс статического игрового объекта: Яблоко
    public class Apple : GameObject
    {
        public static int GiveScore = 1;                // Сколько дает очков колобку, если тот соберет яблоко

        // Конструктор
        public Apple(Point pos, Size siz) : base(pos, siz) { }
    }



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


    // Класс двигающегося игрового объекта: Колобок
    public class Kolobok : MovingObject
    {
        // События
        public event EventHandler LifesLeftChanged;         // Изменение оставшихся жизней
        protected virtual void OnLifesLeftChanged(EventArgs e)
        {
            LifesLeftChanged?.Invoke(this, e);
        }

        public event EventHandler ApplesCollectedChanged;   // Изменение количества собранных яблок
        protected virtual void OnApplesCollectedChanged(EventArgs e)
        {
            ApplesCollectedChanged?.Invoke(this, e);
        }

        public event EventHandler TanksKilledChanged;       // Изменение количества подбитых танков
        protected virtual void OnTanksKilledChanged(EventArgs e)
        {
            TanksKilledChanged?.Invoke(this, e);
        }

        // Поля
        int lifesLeft = 3;
        public int LifesLeft                                // Оставшиеся жизни
        { 
            get {return lifesLeft; }
            set
            {
                lifesLeft = value;
                OnLifesLeftChanged(EventArgs.Empty);
            } 
        }

        int applesCollected = 0;
        public int ApplesCollected                          // Собранные яблоки
        {
            get { return applesCollected; }
            set
            {
                applesCollected = value;
                OnApplesCollectedChanged(EventArgs.Empty);
            }
        }

        int tanksKilled = 0;
        public int TanksKilled                              // Подбитые танки
        {
            get { return tanksKilled; }
            set
            {
                tanksKilled = value;
                OnTanksKilledChanged(EventArgs.Empty);
            }
        }

        // Конструктор
        public Kolobok(Point pos, Size siz, Direction dir = Direction.Bottom) : base(pos, siz, dir) { }
    }

    // Класс двигающегося игрового объекта: Танк
    public class Tank : MovingObject
    {
        public static int GiveScore = 1;                // Сколько дает очков колобку, если тот подобьет танк

        // Конструктор
        public Tank(Point pos, Size siz, Direction dir = Direction.Bottom) : base(pos, siz, dir) { }
    }

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
            set {
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