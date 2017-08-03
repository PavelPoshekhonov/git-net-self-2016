////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения двигающегося игрового объекта: Танк
////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using TankWars.Properties;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Класс отображения двигающегося игрового объекта: Пуля
    public class BulletView : MovingObjectView
    {
        // Конструктор
        public BulletView(Control canvas) : base(canvas) { }

        // Установить обработчик события "Изменение активности"
        public override void SetEventHandlers(GameObject obj)
        {
            base.SetEventHandlers(obj);

            if ((obj is Bullet) == false) return;

            (obj as Bullet).ActiveChanged += ObjectActiveChanged;
            ObjectActiveChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение активности"
        public override void UnSetEventHandlers(GameObject obj)
        {
            base.UnSetEventHandlers(obj);

            if ((obj is Bullet) == false) return;

            (obj as Bullet).ActiveChanged -= ObjectActiveChanged;
        }

        // Обработчик события "Изменение активности"
        public void ObjectActiveChanged(object sender, EventArgs e)
        {
            if ((sender is Bullet) == false) return;
            objectBox.Visible = (sender as Bullet).Active;
        }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj)
        {
            if ((obj is MovingObject) == false) return;

            objectBox.Size = obj.Size;

            switch ((obj as MovingObject).Direction)
            {
                case Direction.Left:
                    {
                        objectBox.Image = Resources.Bullet_L;
                        break;
                    }
                case Direction.Right:
                    {
                        objectBox.Image = Resources.Bullet_R;
                        break;
                    }
                case Direction.Top:
                    {
                        objectBox.Image = Resources.Bullet_T;
                        break;
                    }
                case Direction.Bottom:
                default:
                    {
                        objectBox.Image = Resources.Bullet_B;
                        break;
                    }
            }
        }
    }
}
