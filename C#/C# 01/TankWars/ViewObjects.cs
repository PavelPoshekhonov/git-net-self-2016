////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Классы отображения сущностей игровых объектов:
// Яблоко, Колобок, Танк, Пуля
////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TankWars
{
    // Базовый класс отображения статического игрового объекта
    public class GameObjectView
    {
        protected Control Canvas { get; set; }              // Окно для рисования
        protected PictureBox objectBox = new PictureBox();  // Область для отображения объекта

        // Конструктор
        public GameObjectView(Control canvas)
        {
            Canvas = canvas;
        }

        // Установить обработчик события "Изменение положения"
        public void SetLocationChangedHandler(GameObject obj)
        {
            obj.LocationChanged += object_LocationChanged;
            ShowObject(obj); // Отобразить объект сразу при назначении обработчика
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
            ShowObject(sender);
        }

        public void ShowObject(object sender)
        { }
    }

    // Класс отображения статического игрового объекта: Яблоко
    public class AppleView : GameObjectView
    {
        // Конструктор
        public AppleView(Control canvas) : base(canvas) { }
    }



    // Базовый класс отображения двигающегося игрового объекта
    public class MovingObjectView : GameObjectView
    {
        // Конструктор
        public MovingObjectView(Control canvas) : base(canvas) { }

        public void object_DirectionChanged(object sender, EventArgs e)
        {
            if ((sender is GameObject) == false) return;
        }
    }

    // Класс отображения двигающегося игрового объекта: Колобок
    public class KolobokView : MovingObjectView
    {
        // Конструктор
        public KolobokView(Control canvas) : base(canvas) { }

        public void kolobok_LocationChanged(object sender, EventArgs e)
        {
            if ((sender is MovingObject) == false) return;

            objectBox.Size = ((MovingObject)sender).Size;
            objectBox.Location = ((MovingObject)sender).Location; 

            Canvas.Controls.Add(objectBox);

            Bitmap kolobok = new Bitmap(200, 100);
            Graphics kolobokGraphics = Graphics.FromImage(kolobok);
            int yellow = 0;
            int white = 11;
            while (white <= 100)
            {
                kolobokGraphics.FillRectangle(Brushes.Yellow, 20, yellow, 200, 10);
                kolobokGraphics.FillRectangle(Brushes.White, 20, white, 200, 10);
                yellow += 20;
                white += 20;
            }
            objectBox.Image = kolobok;
        }
    }

    // Класс отображения двигающегося игрового объекта: Танк
    public class TankView : MovingObjectView
    {
        // Конструктор
        public TankView(Control canvas) : base(canvas) { }
    }

    // Класс отображения двигающегося игрового объекта: Пуля
    public class BulletView : MovingObjectView
    {
        // Конструктор
        public BulletView(Control canvas) : base(canvas) { }
    }

}
