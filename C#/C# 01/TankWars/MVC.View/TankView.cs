////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения двигающегося игрового объекта: Танк
////////////////////////////////////////////////////////////

using System.Windows.Forms;
using TankWars.Properties;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Класс отображения двигающегося игрового объекта: Танк
    public class TankView : MovingObjectView
    {
        // Конструктор
        public TankView(Control canvas) : base(canvas)
        {
            objectBox.Size = ObjectSize.CommonSize;
        }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj)
        {
            if ((obj is MovingObject) == false) return;

            switch ((obj as MovingObject).Direction)
            {
                case Direction.Left:
                    {
                        objectBox.Image = Resources.Tank_L;
                        break;
                    }
                case Direction.Right:
                    {
                        objectBox.Image = Resources.Tank_R;
                        break;
                    }
                case Direction.Top:
                    {
                        objectBox.Image = Resources.Tank_T;
                        break;
                    }
                case Direction.Bottom:
                default:
                    {
                        objectBox.Image = Resources.Tank_B;
                        break;
                    }
            }
        }
    }
}
