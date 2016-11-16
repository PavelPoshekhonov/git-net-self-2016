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
        // Размеры картинок
        public static readonly int PicWidth = 28;
        public static readonly int PicHeight = 28;

        protected Control Canvas { get; set; }              // Окно для рисования
        protected PictureBox objectBox = new PictureBox();  // Область для отображения объекта

        // Конструктор
        public GameObjectView(Control canvas)
        {
            Canvas = canvas;
            Canvas.Controls.Add(objectBox); // Привязать картинку к игровой области
            objectBox.Size = new Size(PicWidth, PicHeight);
            objectBox.BackColor = Color.Transparent;
        }

        // Установить обработчик события "Изменение положения"
        public void SetLocationChangedHandler(GameObject obj)
        {
            obj.LocationChanged += object_LocationChanged;
            SelectImage(obj); // Выбрать картинку объекта
            MoveObject(obj);  // Переместить объект сразу при назначении обработчика
        }

        // Очистить обработчик события "Изменение положения"
        public void UnSetLocationChangedHandler(GameObject obj)
        {
            obj.LocationChanged -= object_LocationChanged;
        }

        // Обработчик события "Изменение положения"
        public void object_LocationChanged(object sender, EventArgs e)
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

    // Класс отображения статического игрового объекта: Яблоко
    public class AppleView : GameObjectView
    {
        // Конструктор
        public AppleView(Control canvas) : base(canvas) { }

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
            obj.DirectionChanged += object_DirectionChanged;
            SelectImage(obj); // Выбрать картинку объекта
        }

        // Очистить обработчик события "Изменение направления"
        public void UnSetDirectionChangedHandler(MovingObject obj)
        {
            obj.DirectionChanged -= object_DirectionChanged;
        }

        // Обработчик события "Изменение направления"
        public void object_DirectionChanged(object sender, EventArgs e)
        {
            if ((sender is MovingObject) == false) return;
            SelectImage((MovingObject)sender);
        }
    }

    // Класс отображения двигающегося игрового объекта: Колобок
    public class KolobokView : MovingObjectView
    {
        // Конструктор
        public KolobokView(Control canvas) : base(canvas) { }

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
        public TankView(Control canvas) : base(canvas) { }

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
        public BulletView(Control canvas) : base(canvas) { }

        // Выбрать картинку объекта (загрузить картинку в PictureBox)
        override public void SelectImage(GameObject obj) { }
    }

}