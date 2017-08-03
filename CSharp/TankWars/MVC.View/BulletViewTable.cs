////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения пули в таблице
////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Класс отображения пули в таблице
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
