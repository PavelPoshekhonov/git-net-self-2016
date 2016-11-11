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
        Rectangle position;
        public Rectangle Position       // Текущее положение объекта
        {
            get { return position; }
            set
            {
                OldPosition = Position;
                position = value;
                LastMoving = DateTime.Now;
                // Инициировать перерисовку
            }
        }
    }

    // Базовый класс для игровых объектов: Колобок, Танк, Пуля
    public class MovingObject : GameObject
    {
        Rectangle position;
        public Rectangle Position       // Текущее положение объекта
        {
            get { return position; }
            set
            {
                OldPosition = Position;
                position = value;
                LastMoving = DateTime.Now;
                // Инициировать перерисовку
            }
        }
        public Rectangle OldPosition;       // Предыдущее положение объекта
        public DateTime LastMoving;         // Время последнего перемещения
        public int MovementDelay;           // Определяет как часто происходит перемещение объекта, мсек
        public Direction direction;

        public MovingObject()
        {
            // Выбрать свое место на игровом поле
            // Задать position
        }
    }

    // Колобок
    public class Kolobok : MovingObject
    {
    }

    // Танк
    public class Tank : MovingObject
    {
    }

}

