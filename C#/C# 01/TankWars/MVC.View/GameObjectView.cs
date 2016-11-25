////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Базовый класс отображения статического игрового объекта
////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Базовый класс отображения статического игрового объекта
    public abstract class GameObjectView
    {
        protected PictureBox objectBox = new PictureBox();  // Область для отображения объекта

        // Конструктор
        public GameObjectView(Control canvas)
        {
            canvas.Controls.Add(objectBox); // Привязать картинку к игровой области
            objectBox.Size = new Size(2, 2);
            objectBox.BackColor = Color.Transparent;
        }

        // Установить обработчик события "Изменение положения"
        public virtual void SetEventHandlers(GameObject obj)
        {
            obj.LocationChanged += ObjectLocationChanged;
            SelectImage(obj); // Выбрать картинку объекта
            ObjectLocationChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение положения"
        public virtual void UnSetEventHandlers(GameObject obj)
        {
            obj.LocationChanged -= ObjectLocationChanged;
        }

        // Обработчик события "Изменение положения"
        public void ObjectLocationChanged(object sender, EventArgs e)
        {
            if ((sender is GameObject) == false) return;
            MoveObject((GameObject)sender); // Переместить объект
        }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        public abstract void SelectImage(GameObject obj);

        // Переместить объект
        public void MoveObject(GameObject obj)
        {
            objectBox.Location = obj.Location;
        }
    }
}