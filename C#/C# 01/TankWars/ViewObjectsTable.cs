////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения сущностей игровых объектов в таблице
////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace TankWars
{

    public class GameObjectViewTable
    {
        protected DataGridView grid;  // Грид
        protected int row;            // Строка, в которую надо отображать

        // Конструктор
        public GameObjectViewTable(DataGridView grd)
        {
            grid = grd;
        }

        // Установить обработчик события "Изменение положения"
        public virtual void SetEventHandlers(GameObject obj, int rw)
        {
            row = rw;

            // Записать имя в грид
            grid.Rows[row].Cells[0].Value = obj.Name;

            obj.LocationChanged += ObjectLocationChanged;
            ObjectLocationChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение положения"
        public virtual void UnSetEventHandlers(GameObject obj)
        {
            obj.LocationChanged -= ObjectLocationChanged;
        }

        // Обработчик события "Изменение положения"
        public void ObjectLocationChanged(object sender, EventArgs e)
        {
            if ((sender is GameObject) == false) return;
            grid.Rows[row].Cells[1].Value = (sender as GameObject).Location.ToString();
        }
    }

    public class BulletViewTable : GameObjectViewTable
    {
        // Конструктор
        public BulletViewTable(DataGridView grd) : base(grd) { }

        // Установить обработчик события "Изменение активности"
        public override void SetEventHandlers(GameObject obj, int rw)
        {
            base.SetEventHandlers(obj, rw);

            if ((obj is Bullet) == false) return;

            (obj as Bullet).ActiveChanged += BulletActiveChanged;
            BulletActiveChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение активности"
        public override void UnSetEventHandlers(GameObject obj)
        {
            base.UnSetEventHandlers(obj);

            if ((obj is Bullet) == false) return;

            (obj as Bullet).ActiveChanged -= BulletActiveChanged;
        }

        // Обработчик события "Изменение активности"
        public void BulletActiveChanged(object sender, EventArgs e)
        {
            if ((sender is Bullet) == false) return;

            if ((sender as Bullet).Active == false)
                grid.Rows[row].Cells[1].Value = "Пуля не активна";
        }
    }
}