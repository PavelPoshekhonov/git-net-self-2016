////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения карты игры, состоящей из массива стен
////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankWars
{
    class ViewMap
    {
        public List<WallView> WallViewList = new List<WallView>();  // Массив стен

        // Отобразить карту
        public void ShowMap(Control canvas, GameMap gameMap)
        {
            WallViewList.Clear();

            foreach (Wall wll in gameMap.WallList)
            {
                WallViewList.Add(new WallView(canvas));
                WallViewList.Last().SetLocationChangedHandler(wll);
            }
        }
    }
}
