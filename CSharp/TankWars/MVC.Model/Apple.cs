////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Класс статического игрового объекта: Яблоко
////////////////////////////////////////////////////////////

using System.Drawing;

namespace TankWars.MVC.Model
{
    // Класс статического игрового объекта: Яблоко
    public class Apple : GameObject
    {
        public static int GiveScore = 1;                // Сколько дает очков колобку, если тот соберет яблоко

        // Конструктор
        public Apple(Point pos, Size siz) : base(pos, siz) { }
    }

}
