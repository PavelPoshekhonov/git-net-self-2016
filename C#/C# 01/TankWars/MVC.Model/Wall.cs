﻿////////////////////////////////////////////////////////////
// MVC. Бизнес уровень
// Класс статического игрового объекта: Стена
////////////////////////////////////////////////////////////

using System.Drawing;

namespace TankWars.MVC.Model
{
    // Класс статического игрового объекта: Стена
    public class Wall : GameObject
    {
        // Конструктор
        public Wall(Point pos, Size siz) : base(pos, siz) { }
    }
}
