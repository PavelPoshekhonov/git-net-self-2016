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
    }

    // Класс статического игрового объекта: Яблоко
    public class Apple : GameObject
    {
        public static int GiveScore = 1;                // Сколько дает очков колобку

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
        public Direction direction;
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
        public static int GiveScore = 1;                // Сколько дает очков колобку

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

