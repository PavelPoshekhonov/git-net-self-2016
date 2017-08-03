////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Класс двигающегося игрового объекта: Танк
////////////////////////////////////////////////////////////

using System.Drawing;

namespace TankWars.MVC.Model
{
    // Класс двигающегося игрового объекта: Танк
    public class Tank : MovingObject
    {
        public static int GiveScore = 1;                // Сколько дает очков колобку, если тот подобьет танк

        // Конструктор
        public Tank(Point pos, Size siz, Direction dir = Direction.Bottom) : base(pos, siz, dir) { }
    }
}
