////////////////////////////////////////////////////////////
// MVC. Контроллер
// Управлением процессом игры
////////////////////////////////////////////////////////////

using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TankWars
{
    public class PackmanController
    {
        // Игровые объекты
        public Kolobok Kolobok;
        public Apple[] Apple;
        public Tank[] Tank;
        public Bullet[] Bullet;

        // Отображение игровых объектов
        public KolobokView KolobokViewer;
        public AppleView[] AppleViewer;
        public TankView[] TankViewer;
        public BulletView[] BulletViewer;

        // Таймеры, запускающие изменение положения объектов
        public System.Threading.Timer KolobokTimer;
        public System.Threading.Timer[] TankTimer;
        public System.Threading.Timer[] BulletTimer;


        // Конструктор
        public PackmanController(Control canvas, int tankAmount, int appleAmount, int mDelay)
        {
            // Создаем колобка
            Kolobok = new Kolobok(new Point(250, 400), new Size(40, 40), Direction.Top);
            // Создаем отображение колобка
            KolobokViewer = new KolobokView(canvas);
            // Назначаем обработчики событий "Изменение положения", "Изменение направления"
            KolobokViewer.SetLocationChangedHandler(Kolobok);
/*
            for (int i = 0; i < tankAmount; i++)
            {
                // Создаем танк
                Tank[i] = new Tank(new Point(50 * (i + 1), 50), new Size(40, 40), Direction.Bottom);
                // Создаем отображение танка
                TankViewer[i] = new TankView(canvas);
                // Назначаем обработчики событий "Изменение положения", "Изменение направления"
                TankViewer[i].SetLocationChangedHandler(Tank[i]);
            }

            for (int i = 0; i < appleAmount; i++)
            {
                // Создаем яблоко
                Apple[i] = new Apple(new Point(80 * (i + 1), 150), new Size(40, 40));
                // Создаем отображение яблока
                AppleViewer[i] = new AppleView(canvas);
                // Назначаем обработчик события "Изменение положения"
                AppleViewer[i].SetLocationChangedHandler(Apple[i]);
            }
*/
        }

        // Деструктор
        public void Dispose()
        {
            KolobokViewer.UnSetLocationChangedHandler(Kolobok);
        }

        // Запустить игру
        public void Play()
        {
            KolobokTimer = new System.Threading.Timer(KolobokController, null, 0, 250);
        }

        // Контроллер колобка
        void KolobokController(object obj)
        {
            Kolobok.Location = new Point(Kolobok.Location.X, Kolobok.Location.Y + 5);
        }
    }
}
