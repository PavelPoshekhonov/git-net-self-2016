////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Классы отображения сущностей игровых объектов:
// Яблоко, Колобок, Танк, Пуля
////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using TankWars.Properties;

namespace TankWars
{
    // Базовый класс отображения статического игрового объекта
    public abstract class GameObjectView
    {
        protected PictureBox objectBox = new PictureBox();  // Область для отображения объекта

        // Конструктор
        public GameObjectView(Control canvas)
        {
            canvas.Controls.Add(objectBox); // Привязать картинку к игровой области
            objectBox.Size = new Size(10, 10);
            objectBox.BackColor = Color.Transparent;
        }

        // Установить обработчик события "Изменение положения"
        public void SetLocationChangedHandler(GameObject obj)
        {
            obj.LocationChanged += ObjectLocationChanged;
            SelectImage(obj); // Выбрать картинку объекта
            ObjectLocationChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение положения"
        public void UnSetLocationChangedHandler(GameObject obj)
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

    // Класс отображения статического игрового объекта: Стена
    public class WallView : GameObjectView
    {
        // Конструктор
        public WallView(Control canvas) : base(canvas) { }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj)
        {
            objectBox.Image = Resources.Wall;
            // Повторяющаяся картинка
            objectBox.BackgroundImageLayout = ImageLayout.Tile;
        }
    }

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



    // Базовый класс отображения двигающегося игрового объекта
    public abstract class MovingObjectView : GameObjectView
    {
        // Конструктор
        public MovingObjectView(Control canvas) : base(canvas) { }

        // Установить обработчик события "Изменение направления"
        public void SetDirectionChangedHandler(MovingObject obj)
        {
            obj.DirectionChanged += ObjectDirectionChanged;
            ObjectDirectionChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение направления"
        public void UnSetDirectionChangedHandler(MovingObject obj)
        {
            obj.DirectionChanged -= ObjectDirectionChanged;
        }

        // Обработчик события "Изменение направления"
        public void ObjectDirectionChanged(object sender, EventArgs e)
        {
            if ((sender is MovingObject) == false) return;
            SelectImage((MovingObject)sender);
        }
    }

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
        public void SetGameEventsHandler(Kolobok obj)
        {
            obj.LifesLeftChanged += KolobokLifesLeftChanged;
            KolobokLifesLeftChanged(obj, EventArgs.Empty);          // Принудительное обновление

            obj.ApplesCollectedChanged += KolobokApplesCollectedChanged;
            KolobokApplesCollectedChanged(obj, EventArgs.Empty);    // Принудительное обновление

            obj.TanksKilledChanged += KolobokTanksKilledChanged;
            KolobokTanksKilledChanged(obj, EventArgs.Empty);        // Принудительное обновление
        }

        // Очистить обработчик событий игры
        public void UnSetGameEventsHandler(Kolobok obj)
        {
            obj.LifesLeftChanged -= KolobokLifesLeftChanged;
            obj.ApplesCollectedChanged -= KolobokApplesCollectedChanged;
            obj.TanksKilledChanged -= KolobokTanksKilledChanged;
        }

        // Обработчик события колобка "Изменение оставшихся жизней"
        public void KolobokLifesLeftChanged(object sender, EventArgs e)
        {
            if ((sender is Kolobok) == false) return;
            if (lbLifes == null) return;
            lbLifes.Text = (sender as Kolobok).LifesLeft.ToString();
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
                default:
                    {
                        objectBox.Image = Resources.Kolobok_B;
                        break;
                    }
            }
        }
    }

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
                default:
                    {
                        objectBox.Image = Resources.Tank_B;
                        break;
                    }
            }
        }
    }

    // Класс отображения двигающегося игрового объекта: Пуля
    public class BulletView : MovingObjectView
    {
        // Конструктор
        public BulletView(Control canvas) : base(canvas)
        {
            objectBox.Size = ObjectSize.BulletSize;
        }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj) { }
    }
}