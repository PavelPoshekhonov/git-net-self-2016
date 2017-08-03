////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Класс двигающегося игрового объекта: Колобок
////////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace TankWars.MVC.Model
{
    // Класс двигающегося игрового объекта: Колобок
    public class Kolobok : MovingObject
    {
        // События
        public event EventHandler LifesLeftChanged;         // Изменение оставшихся жизней
        protected virtual void OnLifesLeftChanged(EventArgs e)
        {
            LifesLeftChanged?.Invoke(this, e);
        }

        public event EventHandler ApplesCollectedChanged;   // Изменение количества собранных яблок
        protected virtual void OnApplesCollectedChanged(EventArgs e)
        {
            ApplesCollectedChanged?.Invoke(this, e);
        }

        public event EventHandler TanksKilledChanged;       // Изменение количества подбитых танков
        protected virtual void OnTanksKilledChanged(EventArgs e)
        {
            TanksKilledChanged?.Invoke(this, e);
        }

        // Поля
        int lifesLeft = 3;
        public int LifesLeft                                // Оставшиеся жизни
        {
            get { return lifesLeft; }
            set
            {
                lifesLeft = value;
                OnLifesLeftChanged(EventArgs.Empty);
            }
        }

        int applesCollected = 0;
        public int ApplesCollected                          // Собранные яблоки
        {
            get { return applesCollected; }
            set
            {
                applesCollected = value;
                OnApplesCollectedChanged(EventArgs.Empty);
            }
        }

        int tanksKilled = 0;
        public int TanksKilled                              // Подбитые танки
        {
            get { return tanksKilled; }
            set
            {
                tanksKilled = value;
                OnTanksKilledChanged(EventArgs.Empty);
            }
        }

        // Конструктор
        public Kolobok(Point pos, Size siz, Direction dir = Direction.Bottom) : base(pos, siz, dir) { }
    }
}
