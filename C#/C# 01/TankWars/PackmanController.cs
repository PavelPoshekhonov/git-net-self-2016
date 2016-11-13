using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankWars
{
    public class PackmanController
    {
        // Объекты
        public MovingObject Kolobok;
        public MovingObject[] Tank;

        // Отображение объектов
        public KolobokView KolobokViewer;

        // Конструктор
        public PackmanController(Control canvas, int tankAmount, int mDelay)
        {
            KolobokViewer = new KolobokView(canvas);

            // Создаем колобка и назначаем обработчик события
            Kolobok = new MovingObject(new Rectangle(100, 100, 40, 40), mDelay);
            Kolobok.LocationChanged += KolobokViewer.kolobok_LocationChanged;

        }

        // Деструктор
        public void Dispose()
        {
            Kolobok.LocationChanged -= KolobokViewer.kolobok_LocationChanged;
        }

        public void Play()
        {
            for (int i = 0; i < 9; i++)
            {
                Thread.Sleep(50);
                Kolobok.Location = new Point(Kolobok.Location.X, Kolobok.Location.Y + 5);
                
            }
        }
    }
}
