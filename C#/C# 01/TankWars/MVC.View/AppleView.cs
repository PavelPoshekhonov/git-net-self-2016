////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения статического игрового объекта: Яблоко
////////////////////////////////////////////////////////////

using System.Windows.Forms;
using TankWars.Properties;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Класс отображения статического игрового объекта: Яблоко
    public class AppleView : GameObjectView
    {
        // Конструктор
        public AppleView(Control canvas) : base(canvas)
        {
            objectBox.Size = ObjectSize.CommonSize;
        }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj)
        {
            objectBox.Image = Resources.Apple;
        }
    }
}
