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
    // Отобразить игровой объект
    public class GameObjectView
    {
        public GameObjectView()
        {
        }
    }

    public class KolobokView
    {
        Control Canvas { get; set; }       // Окно для рисования

        public KolobokView(Control canvas)
        {
            Canvas = canvas;
        }

        public void kolobok_LocationChanged(object sender, EventArgs e)
        {
            if ((sender is MovingObject) == false) return;

            Size objectSize = ((MovingObject)sender).ObjectSize;
            Point location = ((MovingObject)sender).Location;
            Point oldLocation = ((MovingObject)sender).OldLocation;

            PictureBox objectBox = new PictureBox();
            objectBox.Size = objectSize;
            objectBox.Location = location;

//            objectBox.


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

    public class TankView
    {
    }


}
