using System;
using System.Collections.Generic;

namespace FormatCoords
{
    /// <summary>Предназначен для хранения и форматированного вывода списка координат <see cref="Point"/>.</summary>
    public class CoordSet
    {
        /// <summary>Имя источника списка координат (Консоль / Файл).</summary>
        public string Sourse { get; }               // Имя источника списка координат (Консоль / Файл)
        List<Point> coords = new List<Point>();     // Список координат

        int maxXinteger = 0;                        // Максимальная целая часть X
        int maxXfract = 0;                          // Максимальная дробная часть X
        int maxYinteger = 0;                        // Максимальная целая часть Y

        /// <summary>Инициализирует новый экземпляр класса CoordSet.</summary>
        /// <param name="sourse">Имя источника списка координат (Консоль / Файл).</param>
        public CoordSet(string sourse)
        {
            Sourse = sourse;
        }

        /// <summary>Добавляет координату в список координат.</summary>
        /// <param name="coordStr">Строка, содержащая пару координат x и y.</param>
        public void Add(string coordStr)
        {
            string[] strXY;
            char decimalSeparator;
            Point newPoint = new Point();

            // Разделить x от y
            strXY = coordStr.Split(',');
            if (strXY.Length != 2)
            {
                return;
            }

            decimalSeparator = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            strXY[0] = strXY[0].Replace('.', decimalSeparator).Replace(',', decimalSeparator).Trim();
            strXY[1] = strXY[1].Replace('.', decimalSeparator).Replace(',', decimalSeparator).Trim();

            try
            {
                // Преобразовать из string в decimal
                newPoint.X = decimal.Parse(strXY[0]);
                newPoint.Y = decimal.Parse(strXY[1]);

                // Добавляем точку x,y в список
                coords.Add(newPoint);
            }
            catch (FormatException)
            {
                return;
            }
        }

        // Поиск самых длинных целых и дробных частей
        void FindLondestIntAndFracParts()
        {
            foreach (Point crd in coords)
            {
                maxXinteger = Math.Max(maxXinteger, crd.SeparatorPosX);
                maxXfract = Math.Max(maxXfract, crd.X.ToString().Length - crd.SeparatorPosX - 1);
                maxYinteger = Math.Max(maxYinteger, crd.SeparatorPosY);
            }
            return;
        }

        /// <summary>Производит форматированный вывод списка координат.</summary>
        public void MakeOutput()
        {
            string strX, strY;

            FindLondestIntAndFracParts();

            foreach (Point crd in coords)
            {
                strX = ("").PadRight(maxXinteger - crd.SeparatorPosX) + crd.X.ToString() +
                       ("").PadRight(maxXfract - (crd.X.ToString().Length - crd.SeparatorPosX - 1));

                strY = ("").PadRight(maxYinteger - crd.SeparatorPosY) + crd.Y.ToString();

                Console.WriteLine("X: {0} Y: {1}", strX, strY);
            }
        }
    }
}
