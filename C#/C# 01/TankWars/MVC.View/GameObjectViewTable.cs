////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения сущностей игровых объектов в таблице
////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Класс отображения сущностей игровых объектов в таблице
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
}