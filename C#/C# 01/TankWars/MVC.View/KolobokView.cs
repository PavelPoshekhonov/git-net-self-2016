////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения двигающегося игрового объекта: Колобок
////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using TankWars.Properties;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Класс отображения двигающегося игрового объекта: Колобок
    public class KolobokView : MovingObjectView
    {
        // Поля. Компоненты для отображения счета игры
        Label lbLifes;
        Label lbApples;
        Label lbTanks;

        // Конструктор
        public KolobokView(Control canvas) : base(canvas)
        {
            objectBox.Size = ObjectSize.CommonSize;
        }
        public KolobokView(Control canvas, Label lbLf, Label lbAp, Label lbTn) : this(canvas)
        {
            lbLifes = lbLf;
            lbApples = lbAp;
            lbTanks = lbTn;
        }

        // Установить обработчики событий игры
        public override void SetEventHandlers(GameObject obj)
        {
            base.SetEventHandlers(obj);

            if ((obj is Kolobok) == false) return;

            (obj as Kolobok).LifesLeftChanged += KolobokLifesLeftChanged;
            KolobokLifesLeftChanged(obj, EventArgs.Empty);          // Принудительное обновление

            (obj as Kolobok).ApplesCollectedChanged += KolobokApplesCollectedChanged;
            KolobokApplesCollectedChanged(obj, EventArgs.Empty);    // Принудительное обновление

            (obj as Kolobok).TanksKilledChanged += KolobokTanksKilledChanged;
            KolobokTanksKilledChanged(obj, EventArgs.Empty);        // Принудительное обновление
        }

        // Очистить обработчики событий игры
        public override void UnSetEventHandlers(GameObject obj)
        {
            base.UnSetEventHandlers(obj);

            if ((obj is Kolobok) == false) return;

            (obj as Kolobok).LifesLeftChanged -= KolobokLifesLeftChanged;
            (obj as Kolobok).ApplesCollectedChanged -= KolobokApplesCollectedChanged;
            (obj as Kolobok).TanksKilledChanged -= KolobokTanksKilledChanged;
        }


        // Обработчик события колобка "Изменение оставшихся жизней"
        public void KolobokLifesLeftChanged(object sender, EventArgs e)
        {
            if ((sender is Kolobok) == false) return;
            if (lbLifes == null) return;

            lbLifes.Text = (sender as Kolobok).LifesLeft.ToString();
            if ((sender as Kolobok).LifesLeft <= 0)
                lbLifes.ForeColor = Color.Red;
            else
                lbLifes.ForeColor = SystemColors.ControlText;
        }
        // Обработчик события колобка "Изменение количества собранных яблок"
        public void KolobokApplesCollectedChanged(object sender, EventArgs e)
        {
            if ((sender is Kolobok) == false) return;
            if (lbApples == null) return;
            lbApples.Text = (sender as Kolobok).ApplesCollected.ToString();
        }
        // Обработчик события колобка "Изменение количества подбитых танков"
        public void KolobokTanksKilledChanged(object sender, EventArgs e)
        {
            if ((sender is Kolobok) == false) return;
            if (lbTanks == null) return;
            lbTanks.Text = (sender as Kolobok).TanksKilled.ToString();
        }


        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj)
        {
            if ((obj is MovingObject) == false) return;

            switch ((obj as MovingObject).Direction)
            {
                case Direction.Left:
                    {
                        objectBox.Image = Resources.Kolobok_L;
                        break;
                    }
                case Direction.Right:
                    {
                        objectBox.Image = Resources.Kolobok_R;
                        break;
                    }
                case Direction.Top:
                    {
                        objectBox.Image = Resources.Kolobok_T;
                        break;
                    }
                case Direction.Bottom:
                default:
                    {
                        objectBox.Image = Resources.Kolobok_B;
                        break;
                    }
            }
        }
    }
}
