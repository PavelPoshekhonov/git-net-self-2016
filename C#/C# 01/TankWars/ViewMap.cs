////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Класс отображения карты игры, состоящей из массива стен
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Linq;
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
                WallViewList.Last().SetEventHandlers(wll);
            }
        }

        // Отписаться от событий обновления
        public void CloseMap(GameMap gameMap)
        {
            for (int i = 0; i < gameMap.WallList.Count; i++)
            {
                WallViewList[i].UnSetEventHandlers(gameMap.WallList[i]);
            }
        }
    }
}
