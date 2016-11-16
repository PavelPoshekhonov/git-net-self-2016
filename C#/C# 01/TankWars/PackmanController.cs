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
    public class PackmanController
    {
        // Игровые объекты
        Kolobok KolobokObject;
        List<Apple> AppleList = new List<Apple>();
        List<Tank> TankList = new List<Tank>();
        List<Bullet> BulletList = new List<Bullet>();

        // Отображение игровых объектов
        KolobokView KolobokViewer;
        List<AppleView> AppleViewer = new List<AppleView>();
        List<TankView> TankViewer = new List<TankView>();
        List<BulletView> BulletViewer = new List<BulletView>();

        // Таймеры, запускающие изменение положения объектов
        Timer KolobokTimer;
        List<Timer> TankTimer = new List<Timer>();
        List<Timer> BulletTimer = new List<Timer>();

        // Признак запущенной игры
        bool gameRunning = false;
        public bool GameRunning { get { return gameRunning; } }

        // Генератор случайных чисел
        private Random rnd = new Random();

        // Размер игровой карты
        Size mapSize;


        // Конструктор. Создает объекты, но не запускает таймеры движения объектов
        public PackmanController(Control canvas, int tankAmount, int appleAmount, int mDelay)
        {
            // Сохраняем размер игровой карты
            mapSize = canvas.Size;

            // Создаем колобка
            KolobokObject = new Kolobok(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize, Direction.Top);
            // Создаем отображение колобка
            KolobokViewer = new KolobokView(canvas);
            // Назначаем обработчики событий "Изменение положения", "Изменение направления"
            KolobokViewer.SetLocationChangedHandler(KolobokObject);
            KolobokViewer.SetDirectionChangedHandler(KolobokObject);
            
            for (int i = 0; i < appleAmount; i++)
            {
                // Создаем яблоко
                AppleList.Add(new Apple(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize));
                // Создаем отображение яблока
                AppleViewer.Add(new AppleView(canvas));
                // Назначаем обработчик события "Изменение положения"
                AppleViewer[i].SetLocationChangedHandler(AppleList[i]);
            }
            
            for (int i = 0; i < tankAmount; i++)
            {
                // Создаем танк
                TankList.Add(new Tank(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize, (Direction)rnd.Next(4)));
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


        // Подготовить новую игру (для уже созданных объектов)
        public void InitNewGame()
        {
            if (KolobokObject == null) return;

            // Реинициализировать параметры колобка
            KolobokObject.LifesLeft = 3;
            KolobokObject.ApplesCollected = 0;
            KolobokObject.TanksKilled = 0;

            // Новое положение объектов
            // !!! Пересоздать карту объекта
            KolobokObject.Location = GetRandomLocation(KolobokObject.Size);
            foreach (Apple app in AppleList)
            {
                app.Location = GetRandomLocation(app.Size);
            }
            foreach (Tank tnk in TankList)
            {
                tnk.Location = GetRandomLocation(tnk.Size);
                tnk.Direction = (Direction)rnd.Next(4);
            }
        }

        // Запустить игру
        public void Run()
        {
            // Играет только живой колобок
            if (KolobokObject.LifesLeft <= 0) return;

            // Запускаем таймер колобка
            KolobokTimer.Start();

            // Запускаем таймеры танков
            foreach (Timer tmr in TankTimer)
            {
                tmr.Start();
            }

            gameRunning = true;
        }

        // Приостановить игру
        public void Pause()
        {
            // Останавливаем таймер колобка
            KolobokTimer.Stop();

            // Останавливаем таймеры танков
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

            // Проверка на выход за пределы карты
            if ((CheckOutOfMap(newLocation, KolobokObject.Size) == true)) return;

            // Проверка пересечений
            List<GameObject> crossObjects = CheckAllCrossing(newLocation, KolobokObject.Size, KolobokObject);
            // Проверка пересечения со стенами
            // Проверка пересечения с яблоками
            // Проверка пересечения с танками

            // Переход на новую позицию
            KolobokObject.Location = newLocation;
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


        // Выдать случайную допустимую позицию на карте для объекта размером siz
        // Применяется при начальной расстановке, съедании яблока, смерти колобка
        Point GetRandomLocation(Size siz)
        {
            Point loc;
            do // Повторяем пока есть пересечения с другими объектами или выход за пределы карты
            {
                loc = new Point(rnd.Next(mapSize.Width - siz.Width), rnd.Next(mapSize.Height - siz.Height));
            } 
            while ((CheckAllCrossing(loc, siz).Count != 0) || (CheckOutOfMap(loc, siz) == true));
            return loc;
        }

        // Возвращает список объектов, с которыми пересекается позиция (loc, siz)
        List<GameObject> CheckAllCrossing(Point loc, Size siz, GameObject selfObj = null)
        {
            List<GameObject> result = new List<GameObject>();

            // Проверяем все объект на предмет пересечения с прямоугольником (loc, siz)
            if ((KolobokObject != null) && (KolobokObject.CheckCrossing(loc, siz)))
                result.Add(KolobokObject);

            foreach (Apple app in AppleList)
            {
                if (app.CheckCrossing(loc, siz))
                    result.Add(app);
            }
            foreach (Tank tnk in TankList)
            {
                if (tnk.CheckCrossing(loc, siz))
                    result.Add(tnk);
            }

            // Исключаем "пересечение c самим собой" при проверке новой позиции сдвинутой на 1 пиксел
            if (selfObj != null)
            {
                foreach (GameObject res in result)
                {
                    if (res.Equals(selfObj))
                    {
                        result.Remove(res);
                    }
                }
            }
            
            return result;
        }

        // Проверить, не выходит ли объект за область игрового поля
        private bool CheckOutOfMap(Point loc, Size siz)
        {
            // Если объект полностью внутри карты
            if ((loc.X >= 0) && (loc.X + siz.Width <= mapSize.Width) &&
                (loc.Y >= 0) && (loc.Y + siz.Height <= mapSize.Height))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Изменение позиции loc на 1 пиксель с учетом направления dir
        // Без учета возможного пересечения с другими объектами
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
