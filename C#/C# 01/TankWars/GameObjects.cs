////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Классы сущностей игровых объектов:
// Яблоко, Колобок, Танк, Пуля
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
        // Размеры Яблока, Колобка, Танка
        public static readonly Size CommonSize = new Size(28, 28);
        // Размеры Пули
        public static readonly Size BulletSize = new Size(28, 10);
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
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public int LifesLeft { get; set; } = 3;         // Оставшиеся жизни
        public int ApplesCollected { get; set; } = 0;   // Собранные яблоки
        public int TanksKilled { get; set; } = 0;       // Подбитые танки

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
    public class Bullet : MovingObject
    {
        // Конструктор
        public Bullet(Point pos, Size siz, Direction dir = Direction.Bottom) : base(pos, siz, dir) { }
    }
}