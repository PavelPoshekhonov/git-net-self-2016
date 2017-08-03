using System;

namespace FormatCoords
{
    /// <summary>Представляет пару координат x и y в двухмерном пространстве (на плоскости).
    /// Кроме того, хранит индексы десятичных разделителей чисел x и y.</summary>
    public struct Point
    {
        int separatorPosX;
        /// <summary>Индекс десятичного разделителя в X.</summary>
        public int SeparatorPosX { get { return separatorPosX; } }      // Индекс десятичного разделителя в X

        int separatorPosY;
        /// <summary>Индекс десятичного разделителя в Y.</summary>
        public int SeparatorPosY { get { return separatorPosY; } }      // Индекс десятичного разделителя в Y

        decimal x;
        /// <summary>Возвращает или задает X-координату структуры.</summary>
        public decimal X
        {
            get { return x; }
            set
            {
                x = value;
                char decimalSeparator = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                separatorPosX = (x.ToString().IndexOf(decimalSeparator) == -1) ? x.ToString().Length : x.ToString().IndexOf(decimalSeparator);
            }
        }

        decimal y;
        /// <summary>Возвращает или задает Y-координату структуры.</summary>
        public decimal Y
        {
            get { return y; }
            set
            {
                y = value;
                char decimalSeparator = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                separatorPosY = (y.ToString().IndexOf(decimalSeparator) == -1) ? y.ToString().Length : y.ToString().IndexOf(decimalSeparator);
            }
        }
    }
}
