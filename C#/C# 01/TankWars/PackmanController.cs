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
        // Игровая карта
        GameMap gameMap;

        // Игровые объекты
        Kolobok kolobokObject;
        List<Apple> appleList = new List<Apple>();
        List<Tank> tankList = new List<Tank>();
        List<Bullet> bulletList = new List<Bullet>();

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

        // Размер игровой карты
        Size mapSize;


        // Конструктор. Создает объекты, но не запускает таймеры движения объектов
        public PackmanController(Control canvas, Label lbLf, Label lbAp, Label lbTn, int tankAmount, int appleAmount, int mDelay)
        {
            // Сохраняем размер игровой карты
            mapSize = canvas.Size;

            // Создаем карту
            gameMap = new GameMap01(mapSize);
            // !!! Создаем отображение карты

            for (int i = 0; i < tankAmount; i++)
            {
                // Создаем танк
                tankList.Add(new Tank(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize, (Direction)rnd.Next(4)));
                // Создаем отображение танка
                tankViewer.Add(new TankView(canvas));
                // Назначаем обработчики событий "Изменение положения", "Изменение направления"
                tankViewer[i].SetLocationChangedHandler(tankList[i]);
                tankViewer[i].SetDirectionChangedHandler(tankList[i]);
            }

            // Создаем колобка
            kolobokObject = new Kolobok(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize, Direction.Top);
            kolobokObject.LifesLeftChanged += KolobokLifesLeftChanged;
            // Создаем отображение колобка
            kolobokViewer = new KolobokView(canvas, lbLf, lbAp, lbTn);
            // Назначаем обработчики событий "Изменение положения", "Изменение направления"
            kolobokViewer.SetLocationChangedHandler(kolobokObject);
            kolobokViewer.SetDirectionChangedHandler(kolobokObject);
            kolobokViewer.SetGameEventsHandler(kolobokObject);


            for (int i = 0; i < appleAmount; i++)
            {
                // Создаем яблоко
                appleList.Add(new Apple(GetRandomLocation(ObjectSize.CommonSize), ObjectSize.CommonSize));
                // Создаем отображение яблока
                appleViewer.Add(new AppleView(canvas));
                // Назначаем обработчик события "Изменение положения"
                appleViewer[i].SetLocationChangedHandler(appleList[i]);
            }
            

            // Таймер колобка
            kolobokTimer = new Timer();         // Создаем таймер
            kolobokTimer.Interval = mDelay;     // Интервал между срабатываниями 
            kolobokTimer.Tick += new EventHandler(KolobokController); // Подписываемся на события Tick
            
            // Таймеры танков
            for (int i = 0; i < tankAmount; i++)
            {
                tankTimer.Add(new Timer());     // Создаем таймер
                tankTimer[i].Tag = i;           // Сохраняем номер танка, с которым работает таймер
                tankTimer[i].Interval = mDelay; // Интервал между срабатываниями 
                tankTimer[i].Tick += new EventHandler(TankController); // Подписываемся на события Tick
            }
        }


        // Подготовить новую игру (для уже созданных объектов)
        public void InitNewGame()
        {
            if (kolobokObject == null) return;

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

            gameRunning = true;
        }

        // Приостановить игру
        public void Pause()
        {
            // Останавливаем таймер колобка
            kolobokTimer.Stop();

            // Останавливаем таймеры танков
            foreach (Timer TTimer in tankTimer)
            {
                TTimer.Stop();
            }

            gameRunning = false;
        }


        // Обработчик события колобка "Изменение оставшихся жизней"
        public void KolobokLifesLeftChanged(object sender, EventArgs e)
        {
            if ((sender is Kolobok) == false) return;
            // Конец игры. Ставим на паузу (после которой можно запустить только новую игру)
            if ((sender as Kolobok).LifesLeft <= 0)
            {
                Pause();
                MessageBox.Show("Game over!");
            }
        }

        // Контроллер колобка
        void KolobokController(object sender, EventArgs e)
        {
            if (kolobokObject == null) return;

            // Предполагаемая новая позиция
            Point newLocation = GetNewLocation(kolobokObject.Direction, kolobokObject.Location);

            // Проверка на выход за пределы карты
            if ((CheckOutOfMap(newLocation, kolobokObject.Size) == true)) return;

            // Проверка пересечений
            List<GameObject> crossObjects = CheckAllCrossing(newLocation, kolobokObject.Size, kolobokObject);

            // Пересечений нет
            if (crossObjects.Count == 0)
            {
                kolobokObject.Location = newLocation;   // Переход на новую позицию
                return;
            }

            // Проверка пересечений
            foreach (GameObject go in crossObjects)
            {
                if (go is Wall) return; // Колобок уперся в стену. Ничего не делаем
                if (go is Apple)        // Колобок съел яблоко
                {
                    kolobokObject.ApplesCollected += Apple.GiveScore;
                    go.Location = GetRandomLocation(ObjectSize.CommonSize);
                    kolobokObject.Location = newLocation;   // Переход на новую позицию
                }
                if (go is Tank)         // Колобок нарвался на танк
                {
                    kolobokObject.Location = newLocation;   // Переход на новую позицию
                    kolobokObject.LifesLeft--;
                    if (kolobokObject.LifesLeft <= 0) return;
                }
                if (go is Bullet)       // Колобка подбила пуля 
                {
                    kolobokObject.Location = newLocation;   // Переход на новую позицию
                    kolobokObject.LifesLeft--;
                    if (kolobokObject.LifesLeft <= 0) return;
                    // !!! Уничтожить пулю
                }
            }
        }

        // Контроллер танка
        void TankController(object sender, EventArgs e)
        {
            int i = (int)(sender as Timer).Tag;
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

            // Пересечений нет
            if (crossObjects.Count == 0)
            {
                tankList[i].Location = newLocation;   // Переход на новую позицию
                return;
            }

            // Проверка пересечений
            foreach (GameObject go in crossObjects)
            {
                if (go is Wall)         // Танк уперся в стену. Меняем направление на случайное
                {
                    tankList[i].Direction = (Direction)rnd.Next(4);
                }
                if (go is Tank)         // Танк столкнулся другим с танком. Меняем направление на случайное
                {
                    tankList[i].Direction = (Direction)rnd.Next(4);
                }
                if (go is Kolobok)      // Танк задавил колобка
                {
                    tankList[i].Location = newLocation; // Переход на новую позицию
                    kolobokObject.LifesLeft--;
                    if (kolobokObject.LifesLeft <= 0) return;
                    kolobokObject.Location = GetRandomLocation(ObjectSize.CommonSize);
                }
                if (go is Bullet)       // Танк был подбит пулей (неважно чьей)
                {
                    kolobokObject.TanksKilled += Tank.GiveScore;    // Бонус колобку
                    tankList[i].Location = GetRandomLocation(ObjectSize.CommonSize);
                    // !!! Уничтожить пулю
                }
                if (go is Apple)
                {
                    tankList[i].Location = newLocation; // Переход на новую позицию
                }
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
            foreach (Bullet blt in bulletList)      // Пули
            {
                if (blt.CheckCrossing(loc, siz))
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
            if (kolobokObject == null) return;
            switch (e.KeyCode)
            {
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
                default:
                    break;
            }
        }
    }
}