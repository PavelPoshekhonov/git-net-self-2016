////////////////////////////////////////////////////////////
// MVC. Контроллер
// Управление процессом игры
////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TankWars.MVC.Model;
using TankWars.MVC.View;

namespace TankWars.MVC.Controller
{
    public class PackmanController : IDisposable
    {
        // Игровая карта
        GameMap gameMap;
        MapView mapView;

        // Игровые объекты
        Kolobok kolobokObject;
        List<Apple> appleList = new List<Apple>();
        List<Tank> tankList = new List<Tank>();
        List<Bullet> bulletList = new List<Bullet>();

        public Kolobok KolobokObject{ get {return kolobokObject; } }
        public List<Apple> AppleList { get {return appleList; } }
        public List<Tank> TankList { get { return tankList; } }
        public List<Bullet> BulletList { get { return bulletList; } }

        // Отображение игровых объектов
        KolobokView kolobokViewer;
        List<AppleView> appleViewer = new List<AppleView>();
        List<TankView> tankViewer = new List<TankView>();
        List<BulletView> bulletViewer = new List<BulletView>();

        // Таймеры, запускающие изменение положения объектов
        Timer kolobokTimer;
        List<Timer> tankTimer = new List<Timer>();
        List<Timer> bulletTimer = new List<Timer>();

        // Признак запущенной игры
        bool gameRunning = false;
        public bool GameRunning { get { return gameRunning; } }

        // Генератор случайных чисел
        Random rnd = new Random();

        // Игровая карта
        Control canvas;

        // Задержка таймера
        int moveDelay;


        // Конструктор. Создает объекты, но не запускает таймеры движения объектов
        public PackmanController(Control cnv, Label lbLf, Label lbAp, Label lbTn, int tankAmount, int appleAmount, int mDelay)
        {
            // Сохраняем игровую карту и скорость
            canvas = cnv;
            moveDelay = mDelay;

            // Создаем карту
            gameMap = new GameMap01(canvas.Size);
            // Создаем отображение карты
            mapView = new MapView();
            mapView.ShowMap(canvas, gameMap);

            for (int i = 0; i <= tankAmount; i++) // Пуль столько же, сколько и танков + 1 (от колобка)
            {
                // Создаем пули (неактивные)
                bulletList.Add(new Bullet(new Point(0, 0), ObjectSize.BulletH));
                if (i == 0)
                    bulletList[i].Name = "Пуля колобка";
                else
                    bulletList[i].Name = "Пуля танка " + i.ToString();
                // Создаем отображение пули
                bulletViewer.Add(new BulletView(canvas));
                // Назначаем обработчики событий "Изменение положения", "Изменение направления", "Изменение активности"
                bulletViewer[i].SetEventHandlers(bulletList[i]);
            }

            for (int i = 0; i < tankAmount; i++)
            {
                // Создаем танки
                tankList.Add(new Tank(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize, (Direction)rnd.Next(4)));
                tankList[i].Name = "Танк " + (i + 1).ToString();
                // Создаем отображение танка
                tankViewer.Add(new TankView(canvas));
                // Назначаем обработчики событий "Изменение положения", "Изменение направления"
                tankViewer[i].SetEventHandlers(tankList[i]);
            }

            // Создаем колобка
            kolobokObject = new Kolobok(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize, Direction.Top);
            kolobokObject.Name = "Колобок";
            // Создаем отображение колобка
            kolobokViewer = new KolobokView(canvas, lbLf, lbAp, lbTn);
            // Назначаем обработчики событий "Изменение положения", "Изменение направления", Обработчики событий игры
            kolobokViewer.SetEventHandlers(kolobokObject);
            // Контроль события колобка "Изменение оставшихся жизней"
            kolobokObject.LifesLeftChanged += KolobokLifesLeftChanged;

            for (int i = 0; i < appleAmount; i++)
            {
                // Создаем яблоки
                appleList.Add(new Apple(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize));
                appleList[i].Name = "Яблоко " + (i + 1).ToString();
                // Создаем отображение яблока
                appleViewer.Add(new AppleView(canvas));
                // Назначаем обработчик события "Изменение положения"
                appleViewer[i].SetEventHandlers(appleList[i]);
            }
            

            // Таймер колобка
            kolobokTimer = new Timer();         // Создаем таймер
            kolobokTimer.Interval = moveDelay;     // Интервал между срабатываниями 
            kolobokTimer.Tick += new EventHandler(KolobokController); // Подписываемся на события Tick
            
            // Таймеры танков
            for (int i = 0; i < tankAmount; i++)
            {
                tankTimer.Add(new Timer());     // Создаем таймер
                tankTimer[i].Interval = moveDelay; // Интервал между срабатываниями 
                tankTimer[i].Tick += new EventHandler(TankController); // Подписываемся на события Tick
            }

            // Таймеры пуль
            for (int i = 0; i <= tankAmount; i++)
            {
                bulletTimer.Add(new Timer());     // Создаем таймер
                bulletTimer[i].Interval = moveDelay; // Интервал между срабатываниями 
                bulletTimer[i].Tick += new EventHandler(BulletController); // Подписываемся на события Tick
            }
        }

        // Деструктор
        public void Dispose()
        {
            // Остановка таймеров
            Pause();
            kolobokTimer.Tick -= KolobokController; // Отписываемся от событий Tick
            foreach (Timer tmr in tankTimer)
            {
                tmr.Tick -= TankController;         // Отписываемся от событий Tick
            }
            foreach (Timer tmr in bulletTimer)
            {
                tmr.Tick -= BulletController;       // Отписываемся от событий Tick
            }

            // Отписка от событий
            for (int i = 0; i < appleList.Count; i++)
            {
                appleViewer[i].UnSetEventHandlers(appleList[i]);
            }
            for (int i = 0; i < tankList.Count; i++)
            {
                tankViewer[i].UnSetEventHandlers(tankList[i]);
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletViewer[i].UnSetEventHandlers(bulletList[i]);
            }

            kolobokViewer.UnSetEventHandlers(kolobokObject);
            kolobokObject.LifesLeftChanged -= KolobokLifesLeftChanged;

            mapView.CloseMap(gameMap);
        }

        // Подготовить новую игру (для уже созданных объектов)
        public void InitNewGame()
        {
            if (kolobokObject == null) return;

            // Деактивация пуль
            foreach (Bullet blt in bulletList)
            {
                blt.Active = false;
            }

            // Реинициализировать параметры колобка
            kolobokObject.LifesLeft = 3;
            kolobokObject.ApplesCollected = 0;
            kolobokObject.TanksKilled = 0;

            // Новое положение объектов
            kolobokObject.Location = GetRandomLocation(kolobokObject.Size);
            foreach (Apple app in appleList)
            {
                app.Location = GetRandomLocation(app.Size);
            }
            foreach (Tank tnk in tankList)
            {
                tnk.Location = GetRandomLocation(tnk.Size);
                tnk.Direction = (Direction)rnd.Next(4);
            }
        }

        // Запустить игру
        public void Run()
        {
            // Играет только живой колобок
            if (kolobokObject.LifesLeft <= 0) return;

            // Запускаем таймер колобка
            kolobokTimer.Start();

            // Запускаем таймеры танков
            foreach (Timer tmr in tankTimer)
            {
                tmr.Start();
            }

            // Запускаем таймеры пуль (только активных)
            for (int i = 0; i < bulletTimer.Count; i++)
            {
                if ((bulletList[i] != null) && (bulletTimer[i] != null) && (bulletList[i].Active == true))
                    bulletTimer[i].Start();
            }

            gameRunning = true;
        }

        // Поставить игру на паузу
        public void Pause()
        {
            // Останавливаем таймер колобка
            kolobokTimer.Stop();

            // Останавливаем таймеры пуль
            foreach (Timer tmr in bulletTimer)
            {
                tmr.Stop();
            }

            // Останавливаем таймеры танков
            foreach (Timer tmr in tankTimer)
            {
                tmr.Stop();
            }

            gameRunning = false;
        }


        // Обработчик события колобка "Изменение оставшихся жизней"
        void KolobokLifesLeftChanged(object sender, EventArgs e)
        {
            if ((sender is Kolobok) == false) return;
            // Конец игры. Ставим на паузу (после которой можно будет запустить только новую игру)
            if ((sender as Kolobok).LifesLeft <= 0)
            {
                Pause();
                MessageBox.Show("Game over!", Application.ProductName,  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Контроллер колобка (выполняется по таймеру)
        void KolobokController(object sender, EventArgs e)
        {
            if (kolobokObject == null) return;

            // Предполагаемая новая позиция
            Point newLocation = GetNewLocation(kolobokObject.Direction, kolobokObject.Location);

            // Проверка на выход за пределы карты
            if ((CheckOutOfMap(newLocation, kolobokObject.Size) == true)) return;

            // Проверка пересечений
            List<GameObject> crossObjects = CheckAllCrossing(newLocation, kolobokObject.Size, kolobokObject);

            // Анализ пересечений - Препядствия
            foreach (GameObject go in crossObjects)
            {
                if (go is Wall)         // Колобок уперся в стену. Ничего не делаем
                {
                    return;
                };
                if (go is Tank)         // Колобок нарвался на танк
                {
                    kolobokObject.LifesLeft--;
                    if (kolobokObject.LifesLeft > 0)
                    {
                        kolobokObject.Location = GetRandomLocation(ObjectSize.CommonSize);
                    }
                    return;
                }
                if (go is Bullet)       // Колобка подбила пуля 
                {
                    kolobokObject.LifesLeft--;
                    if (kolobokObject.LifesLeft > 0)
                    {
                        kolobokObject.Location = GetRandomLocation(ObjectSize.CommonSize);
                    }
                    DeActivateBullet((Bullet)go); // Деактивировать пулю
                    return;
                    
                }
            }
            // Анализ пересечений - Не препядствия
            foreach (GameObject go in crossObjects)
            {
                if (go is Apple)        // Колобок съел яблоко
                {
                    kolobokObject.ApplesCollected += Apple.GiveScore;
                    go.Location = GetRandomLocation(ObjectSize.CommonSize);
                }
            }
            kolobokObject.Location = newLocation;   // Переход на новую позицию
        }

        // Контроллер танка (выполняется по таймеру)
        void TankController(object sender, EventArgs e)
        {
            int i = tankTimer.IndexOf((sender as Timer));
            if (tankList.Count < i) return;
            if (tankList[i] == null) return;

            // Предполагаемая новая позиция
            Point newLocation = GetNewLocation(tankList[i].Direction, tankList[i].Location);

            // Проверка на выход за пределы карты
            if ((CheckOutOfMap(newLocation, tankList[i].Size) == true))
            {
                tankList[i].Direction = (Direction)rnd.Next(4);
                return;
            }

            // Проверка пересечений
            List<GameObject> crossObjects = CheckAllCrossing(newLocation, tankList[i].Size, tankList[i]);

            // Анализ пересечений - Препядствия
            foreach (GameObject go in crossObjects)
            {
                if (go is Wall)         // Танк уперся в стену. Меняем направление на случайное
                {
                    tankList[i].Direction = (Direction)rnd.Next(4);
                    return;
                }
                if (go is Tank)         // Танк столкнулся другим с танком. Меняем направление на случайное
                {
                    tankList[i].Direction = (Direction)rnd.Next(4);
                    return;
                }
                if (go is Bullet)       // Танк был подбит пулей (неважно чьей)
                {
                    kolobokObject.TanksKilled += Tank.GiveScore;    // Бонус колобку
                    tankList[i].Location = GetRandomLocation(ObjectSize.CommonSize);
                    DeActivateBullet((Bullet)go); // Деактивировать пулю
                    return;
                }
            }
            // Анализ пересечений - Не препядствия
            foreach (GameObject go in crossObjects)
            {
                if (go is Kolobok)      // Танк задавил колобка
                {
                    kolobokObject.LifesLeft--;
                    if (kolobokObject.LifesLeft > 0)
                    {
                        kolobokObject.Location = GetRandomLocation(ObjectSize.CommonSize);
                    }
                }
            }
            tankList[i].Location = newLocation; // Переход на новую позицию

            // Стрельба по колобку
            if (ShouldTankShoot(tankList[i]) == true)
                ActivateBullet(tankList[i], i + 1); // 0-я пуля принадлежит колобку
        }

        // Контроллер пули (выполняется по таймеру)
        void BulletController(object sender, EventArgs e)
        {
            int i = bulletTimer.IndexOf((sender as Timer));
            if (bulletList.Count < i) return;
            if (bulletList[i] == null) return;

            // Предполагаемая новая позиция
            Point newLocation = GetNewLocation(bulletList[i].Direction, bulletList[i].Location, 2);

            // Проверка на выход за пределы карты
            if ((CheckOutOfMap(newLocation, bulletList[i].Size) == true))
            {
                DeActivateBullet(bulletList[i]); // Деактивировать пулю
                return;
            }

            // Проверка пересечений
            List<GameObject> crossObjects = CheckAllCrossing(newLocation, bulletList[i].Size, bulletList[i]);

            // Анализ пересечений - Препядствия
            foreach (GameObject go in crossObjects)
            {
                if (go is Wall)                         // Пуля уперлась в стену. Уничтожаем
                {
                    DeActivateBullet(bulletList[i]);    // Деактивировать пулю
                    return;
                }
                if (go is Bullet)                       // Пуля столкнулась с другой пулей. Уничтожить обе
                {
                    DeActivateBullet(bulletList[i]);    // Деактивировать пулю
                    DeActivateBullet((Bullet)go);       // Деактивировать пулю
                    return;
                }
                if (go is Tank)                         // Пуля подбила танк
                {
                    kolobokObject.TanksKilled += Tank.GiveScore;    // Бонус колобку
                    go.Location = GetRandomLocation(ObjectSize.CommonSize);
                    DeActivateBullet(bulletList[i]);    // Уничтожить пулю
                    return;
                }
                if (go is Kolobok)                      // Пуля подбила колобка
                {
                    kolobokObject.LifesLeft--;
                    DeActivateBullet(bulletList[i]);    // Деактивировать пулю
                    if (kolobokObject.LifesLeft > 0)
                    {
                        kolobokObject.Location = GetRandomLocation(ObjectSize.CommonSize);
                    }
                    return;
                }
            }
            bulletList[i].Location = newLocation; // Переход на новую позицию
        }


        // Выдать случайную допустимую позицию на карте для объекта размером siz
        // Применяется при начальной расстановке, съедании яблока, смерти колобка
        Point GetRandomLocation(Size siz)
        {
            Point loc;
            do // Повторяем пока есть пересечения с другими объектами или выход за пределы карты
            {
                loc = new Point(rnd.Next(canvas.Size.Width - siz.Width), rnd.Next(canvas.Size.Height - siz.Height));
            } 
            while ((CheckAllCrossing(loc, siz).Count != 0) || (CheckOutOfMap(loc, siz) == true));
            return loc;
        }

        // Возвращает список объектов, с которыми пересекается объект, заданный как (loc, siz)
        List<GameObject> CheckAllCrossing(Point loc, Size siz, GameObject selfObj = null)
        {
            List<GameObject> result = new List<GameObject>();

            // Проверяем все объект на предмет пересечения с прямоугольником (loc, siz)
            foreach (Wall wll in gameMap.WallList)  // Стены
            {
                if (wll.CheckCrossing(loc, siz))
                    result.Add(wll);
            }
            foreach (Apple app in appleList)        // Яблоки
            {
                if (app.CheckCrossing(loc, siz))
                    result.Add(app);
            }
            foreach (Tank tnk in tankList)          // Танки
            {
                if (tnk.CheckCrossing(loc, siz))
                    result.Add(tnk);
            }
            foreach (Bullet blt in bulletList)      // Пули (только активные)
            {
                if ((blt.Active == true) && (blt.CheckCrossing(loc, siz)))
                    result.Add(blt);
            }

            if ((kolobokObject != null) && (kolobokObject.CheckCrossing(loc, siz)))     // Колобок
                result.Add(kolobokObject);

            // Исключаем "пересечение c самим собой" при проверке новой позиции сдвинутой на 1 пиксел
            if (selfObj != null)
            {
                foreach (GameObject res in result)
                {
                    if (res.Equals(selfObj))
                    {
                        result.Remove(res);
                        break;
                    }
                }
            }
            
            return result;
        }

        // Проверить, не выходит ли объект за область игрового поля
        bool CheckOutOfMap(Point loc, Size siz)
        {
            // Если объект полностью внутри карты
            if ((loc.X >= 0) && (loc.X + siz.Width <= canvas.Size.Width) &&
                (loc.Y >= 0) && (loc.Y + siz.Height <= canvas.Size.Height))
                return false;
            else
                return true;
        }

        // Изменение позиции loc на step пикселей с учетом направления dir
        // Без учета возможного пересечения с другими объектами
        Point GetNewLocation(Direction dir, Point loc, byte step = 1)
        {
            switch (dir)
            {
                case Direction.Left:
                    return new Point(loc.X - step, loc.Y);
                case Direction.Right:
                    return new Point(loc.X + step, loc.Y);
                case Direction.Bottom:
                    return new Point(loc.X, loc.Y + step);
                case Direction.Top:
                default:
                    return new Point(loc.X, loc.Y - step);
            }
        }

        // Нужно ли танку стрелять
        bool ShouldTankShoot(MovingObject obj)
        {
            // Танк стреляет если, на прямой от него находится колобок (не важно на прямой видимости или нет!)
            Point loc = Bullet.CalcLocation(obj);
            Size siz;
            switch (obj.Direction)
            {
                case Direction.Left:
                    {
                        siz = new Size(loc.X + ObjectSize.BulletH.Width, ObjectSize.BulletH.Height);
                        loc.X = 0; // От начала карты
                        break;
                    }
                case Direction.Right:
                    {
                        siz = new Size(canvas.Width - loc.X, ObjectSize.BulletH.Height);
                        break;
                    }
                case Direction.Top:
                    {
                        siz = new Size(ObjectSize.BulletV.Width, loc.Y + ObjectSize.BulletV.Height);
                        loc.Y = 0; // От начала карты
                        break;
                    }
                case Direction.Bottom:
                default:
                    {
                        siz = new Size(ObjectSize.BulletV.Width, canvas.Height - loc.Y);
                        break;
                    }
            }

            if ((kolobokObject != null) && (kolobokObject.CheckCrossing(loc, siz))) // Пересечение с колобком
                return true;
            else
                return false;
        }


        // Выстрелить
        void ActivateBullet(MovingObject obj, int bulletIndex)
        {
            if (bulletList.Count < bulletIndex) return;
            if (bulletList[bulletIndex] == null) return;
            if (bulletList[bulletIndex].Active == true) return; // Пуля и так уже активна

            // Активируем пулю 
            // Рассчитываем положение пули, в зависимости от положения выстрелевшего танка / колобка
            bulletList[bulletIndex].Location = Bullet.CalcLocation(obj); 
            bulletList[bulletIndex].Direction = obj.Direction;
            bulletList[bulletIndex].Active = true;
            bulletTimer[bulletIndex].Start();
        }

        // Скрыть пулю
        void DeActivateBullet(Bullet blt)
        {
            int bulletIndex = bulletList.IndexOf(blt);

            if (bulletList.Count < bulletIndex) return;
            if (bulletList[bulletIndex] == null) return;

            bulletTimer[bulletIndex].Stop();
            bulletList[bulletIndex].Active = false;
        }


        // Реакция на нажатие кнопки: Управление колобком
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (kolobokObject == null) return;
            // Играет только живой колобок
            if (kolobokObject.LifesLeft <= 0) return;

            if (GameRunning == false) return;

            switch (e.KeyCode)
            {
                // Управление колобком
                case Keys.Left:
                    {
                        kolobokObject.Direction = Direction.Left;
                        break;
                    }
                case Keys.Right:
                    {
                        kolobokObject.Direction = Direction.Right;
                        break;
                    }
                case Keys.Up:
                    {
                        kolobokObject.Direction = Direction.Top;
                        break;
                    }
                case Keys.Down:
                    {
                        kolobokObject.Direction = Direction.Bottom;
                        break;
                    }
                // Стрельба
                case Keys.Space:
                    {
                        ActivateBullet(kolobokObject, 0);
                        break;
                    }
            }
        }
    }
}