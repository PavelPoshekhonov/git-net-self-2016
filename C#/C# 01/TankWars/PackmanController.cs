////////////////////////////////////////////////////////////
// MVC. Контроллер
// Управлением процессом игры
////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TankWars
{
    public class PackmanController : IDisposable
    {
        // Игровые объекты
        public Kolobok KolobokObject;
        public List<Apple> AppleList = new List<Apple>();
        public List<Tank> TankList = new List<Tank>();
        public List<Bullet> BulletList = new List<Bullet>();

        // Отображение игровых объектов
        public KolobokView KolobokViewer;
        public List<AppleView> AppleViewer = new List<AppleView>();
        public List<TankView> TankViewer = new List<TankView>();
        public List<BulletView> BulletViewer = new List<BulletView>();

        // Таймеры, запускающие изменение положения объектов
        public System.Threading.Timer KolobokTimer;
        public List<System.Threading.Timer> TankTimer = new List<System.Threading.Timer>();
        public List<System.Threading.Timer> BulletTimer = new List<System.Threading.Timer>();


        // Конструктор
        public PackmanController(Control canvas, int tankAmount, int appleAmount, int mDelay)
        {
            // Создаем колобка
            KolobokObject = new Kolobok(new Point(250, 400), new Size(28, 28), Direction.Top);
            // Создаем отображение колобка
            KolobokViewer = new KolobokView(canvas);
            // Назначаем обработчики событий "Изменение положения", "Изменение направления"
            KolobokViewer.SetLocationChangedHandler(KolobokObject);
            KolobokViewer.SetDirectionChangedHandler(KolobokObject);

            for (int i = 0; i < appleAmount; i++)
            {
                // Создаем яблоко
                AppleList.Add(new Apple(new Point(80 * (i + 1), 150), new Size(28, 28)));
                // Создаем отображение яблока
                AppleViewer.Add(new AppleView(canvas));
                // Назначаем обработчик события "Изменение положения"
                AppleViewer[i].SetLocationChangedHandler(AppleList[i]);
            }

            for (int i = 0; i < tankAmount; i++)
            {
                // Создаем танк
                TankList.Add(new Tank(new Point(50 * (i + 1), 50), new Size(28, 28), Direction.Bottom));
                // Создаем отображение танка
                TankViewer.Add(new TankView(canvas));
                // Назначаем обработчики событий "Изменение положения", "Изменение направления"
                TankViewer[i].SetLocationChangedHandler(TankList[i]);
                TankViewer[i].SetDirectionChangedHandler(TankList[i]);
            }
        }

        // Деструктор
        public void Dispose()
        {
            KolobokTimer.Dispose();
        }


        // Запустить игру
        public void Play()
        {
//            KolobokTimer = new System.Threading.Timer(KolobokController, null, 0, 250);
        }

        // Контроллер колобка
        void KolobokController(object obj)
        {
            KolobokObject.Location = new Point(KolobokObject.Location.X, KolobokObject.Location.Y + 5);
        }


        // Реакция на нажатие кнопки: Управление колобком
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (KolobokObject == null) return;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    {
                        KolobokObject.Direction = Direction.Left;
                        break;
                    }
                case Keys.Right:
                    {
                        KolobokObject.Direction = Direction.Right;
                        break;
                    }
                case Keys.Up:
                    {
                        KolobokObject.Direction = Direction.Top;
                        break;
                    }
                case Keys.Down:
                    {
                        KolobokObject.Direction = Direction.Bottom;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
