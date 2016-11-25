////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Базовые типы и константы
////////////////////////////////////////////////////////////

using System.Drawing;

namespace TankWars.MVC.Model
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
}