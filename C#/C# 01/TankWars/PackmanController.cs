////////////////////////////////////////////////////////////
// MVC. Контроллер
// Управление процессом игры
////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Timer KolobokTimer;
        public List<Timer> TankTimer = new List<Timer>();
        public List<Timer> BulletTimer = new List<Timer>();

        // Признак запущеенной игры
        bool gameRunning = false;
        public bool GameRunning { get { return gameRunning; } }

        private Random rnd = new Random();

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
                TankList.Add(new Tank(new Point(50 * (i + 1), 50), new Size(28, 28), (Direction)rnd.Next(4)));
                // Создаем отображение танка
                TankViewer.Add(new TankView(canvas));
                // Назначаем обработчики событий "Изменение положения", "Изменение направления"
                TankViewer[i].SetLocationChangedHandler(TankList[i]);
                TankViewer[i].SetDirectionChangedHandler(TankList[i]);
            }
            
            // Таймер колобка
            KolobokTimer = new Timer();         // Создаем таймер
            KolobokTimer.Interval = mDelay;     // Интервал между срабатываниями 
            KolobokTimer.Tick += new EventHandler(KolobokController); // Подписываемся на события Tick
            
            // Таймеры танков
            for (int i = 0; i < tankAmount; i++)
            {
                TankTimer.Add(new Timer());     // Создаем таймер
                TankTimer[i].Tag = i;           // Сохраняем номер танка, с которым работает таймер
                TankTimer[i].Interval = mDelay; // Интервал между срабатываниями 
                TankTimer[i].Tick += new EventHandler(TankController); // Подписываемся на события Tick
            }
        }

        // Деструктор
        public void Dispose()
        {
            KolobokTimer.Dispose();

            foreach (Timer TTimer in TankTimer)
            {
                TTimer.Dispose();
            }
        }


        // Запустить игру
        public void Play()
        {
            // Таймер колобка
            KolobokTimer.Start();

            // Таймеры танков
            foreach (Timer TTimer in TankTimer)
            {
                TTimer.Start();
            }

            gameRunning = true;
        }

        // Приостановить игру
        public void Pause()
        {
            // Таймер колобка
            KolobokTimer.Stop();

            // Таймеры танков
            foreach (Timer TTimer in TankTimer)
            {
                TTimer.Stop();
            }

            gameRunning = false;
        }


        // Контроллер колобка
        void KolobokController(object sender, EventArgs e)
        {
            if (KolobokObject == null) return;

            // Предполагаемая новая позиция
            Point newLocation = GetNewLocation(KolobokObject.Direction, KolobokObject.Location);

            // Проверка предполагаемой позиции
            if ((newLocation.X >= 0) && (newLocation.X + KolobokObject.Size.Width <= 500) &&
                (newLocation.Y >= 0) && (newLocation.Y + KolobokObject.Size.Height <= 500)) 
            {
                KolobokObject.Location = newLocation;
            }
        }

        // Контроллер танка
        void TankController(object sender, EventArgs e)
        {
            int i = (int)(sender as Timer).Tag;
            if (TankList[i] == null) return;

            Point newLocation = GetNewLocation(TankList[i].Direction, TankList[i].Location);

            // Проверка предполагаемой позиции
            if ((newLocation.X >= 0) && (newLocation.X + TankList[i].Size.Width <= 500) &&
                (newLocation.Y >= 0) && (newLocation.Y + TankList[i].Size.Height <= 500))
            {
                TankList[i].Location = newLocation;
            }
            else
            {
                TankList[i].Direction = (Direction)rnd.Next(4);
            }

        }

        Point GetNewLocation(Direction dir, Point loc)
        {
            switch (dir)
            {
                case Direction.Left:
                    {
                        return new Point(loc.X - 1, loc.Y);
                    }
                case Direction.Right:
                    {
                        return new Point(loc.X + 1, loc.Y);
                    }
                case Direction.Bottom:
                    {
                        return new Point(loc.X, loc.Y + 1);
                    }
                case Direction.Top:
                default:
                    {
                        return new Point(loc.X, loc.Y - 1);
                    }
            }
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
