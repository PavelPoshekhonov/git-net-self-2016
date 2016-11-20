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
        DataGridView grid;  // Грид
        int row;            // Строка, в которую надо отображать

        // Конструктор
        public GameObjectViewTable(DataGridView grd)
        {
            grid = grd;
        }

        // Установить обработчик события "Изменение положения"
        public void SetLocationChangedHandler(GameObject obj, int rw)
        {
            row = rw;

            // !!! Записать имя в грид
            grid.Rows[row].Cells[0].Value = "";

            obj.LocationChanged += ObjectLocationChanged;
            ObjectLocationChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение положения"
        public void UnSetLocationChangedHandler(GameObject obj)
        {
            obj.LocationChanged -= ObjectLocationChanged;
        }

        // Обработчик события "Изменение положения"
        public void ObjectLocationChanged(object sender, EventArgs e)
        {
            if ((sender is GameObject) == false) return;
            // !!! Записать координаты в грид
        }
    }
}
