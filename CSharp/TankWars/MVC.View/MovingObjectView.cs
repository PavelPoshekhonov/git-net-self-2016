////////////////////////////////////////////////////////////
// MVC. Презентационный уровень
// Базовый класс отображения двигающегося игрового объекта
////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using TankWars.MVC.Model;

namespace TankWars.MVC.View
{
    // Базовый класс отображения двигающегося игрового объекта
    public abstract class MovingObjectView : GameObjectView
    {
        // Конструктор
        public MovingObjectView(Control canvas) : base(canvas) { }

        // Установить обработчик события "Изменение направления"
        public override void SetEventHandlers(GameObject obj)
        {
            base.SetEventHandlers(obj);

            if ((obj is MovingObject) == false) return;

            (obj as MovingObject).DirectionChanged += ObjectDirectionChanged;
            ObjectDirectionChanged(obj, EventArgs.Empty); // Принудительное обновление
        }

        // Очистить обработчик события "Изменение направления"
        public override void UnSetEventHandlers(GameObject obj)
        {
            base.UnSetEventHandlers(obj);

            if ((obj is MovingObject) == false) return;

            (obj as MovingObject).DirectionChanged -= ObjectDirectionChanged;
        }

        // Обработчик события "Изменение направления"
        public void ObjectDirectionChanged(object sender, EventArgs e)
        {
            if ((sender is MovingObject) == false) return;
            SelectImage((MovingObject)sender);
        }
    }
}
