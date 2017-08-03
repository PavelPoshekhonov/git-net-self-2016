////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения статического игрового объекта: Стена
////////////////////////////////////////////////////////////

using System.Windows.Forms;
using TankWars.Properties;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Класс отображения статического игрового объекта: Стена
    public class WallView : GameObjectView
    {
        // Конструктор
        public WallView(Control canvas) : base(canvas) { }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj)
        {
            objectBox.Image = Resources.Wall;
            objectBox.BackgroundImage = Resources.Wall;
            objectBox.Size = obj.Size;
            // Повторяющаяся картинка
            objectBox.BackgroundImageLayout = ImageLayout.Tile;
        }
    }
}
