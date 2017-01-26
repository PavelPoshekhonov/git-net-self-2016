// Practice C# 01
// Консольное приложение для чтения, форматирования и отображения
// чисел, представляющих x,y координаты положения объекста.

using System;
using System.Collections.Generic;
using System.IO;

namespace FormatCoords
{
    /// <summary>Представляет пару координат x и y в двухмерном пространстве (на плоскости).</summary>
    public struct Point
    {
        int separatorPosX;
        public int SeparatorPosX { get { return separatorPosX; } }

        int separatorPosY;
        public int SeparatorPosY { get { return separatorPosY; } }

        decimal x;
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

    /// <summary>Предназначен для хранения и форматированного вывода списка координат <see cref="Point"/>.</summary>
    public class CoordSet
    {
        public string Sourse { get; }               // Имя источника списка координат (Консоль / Файл)
        List<Point> coords = new List<Point>();     // Список координат

        int maxXinteger = 0;                        // Максимальная целая часть X
        int maxXfract = 0;                          // Максимальная дробная часть X
        int maxYinteger = 0;                        // Максимальная целая часть Y

        /// <summary>Конструктор.</summary>
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

            // Разделить x, y
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

                // Поиск самых длинных целых и дробных частей
                maxXinteger = Math.Max(maxXinteger, newPoint.SeparatorPosX);
                maxXfract   = Math.Max(maxXfract,   newPoint.X.ToString().Length - newPoint.SeparatorPosX - 1);
                maxYinteger = Math.Max(maxYinteger, newPoint.SeparatorPosY);
            }
            catch (FormatException)
            {
                return;
            }
        }

        /// <summary>Производит форматированный вывод списка координат.</summary>
        public void MakeOutput()
        {
            string strX, strY;

            foreach (Point crd in coords)
            {
                strX = ("").PadRight(maxXinteger - crd.SeparatorPosX) + crd.X.ToString() +
                       ("").PadRight(maxXfract - (crd.X.ToString().Length - crd.SeparatorPosX - 1));

                strY = ("").PadRight(maxYinteger - crd.SeparatorPosY) + crd.Y.ToString();

                Console.WriteLine("X: {0} Y: {1}" , strX, strY);
            }
        }
    }


    /// <summary>Консольное приложение для чтения, форматирования и отображения чисел, представляющих x,y координаты положения объекста.</summary>
    class Program
    {
        /// <summary>Обеспечивает ввод координат пользователем с консоли.</summary>
        /// <returns>Экземпляр <see cref="CoordSet"/>.</returns>
        static CoordSet MakeConsoleInput()
        {
            string strLine;
            CoordSet cs = new CoordSet("Ввод с консоли");

            do
            {
                strLine = Console.ReadLine();
                if (strLine != "")
                {
                    cs.Add(strLine);
                }
            }
            while (strLine != "");

            return cs;
        }

        /// <summary>Обеспечивает ввод координат из текстовых файлов.</summary>
        /// <returns>Массив списков координат <see cref="CoordSet"/>.</returns>
        static List<CoordSet> MakeFileInput(string[] args)
        {
            List<CoordSet> csl = new List<CoordSet>();

            for (int i = 0; i < args.Length; i++)
            {
                StreamReader sr = new StreamReader(args[i]);
                csl.Add(new CoordSet("Данные из файла " + args[i]));

                while (sr.EndOfStream == false)
                {
                    csl[i].Add(sr.ReadLine());
                }
            }

            return csl;
        }


        static void Main(string[] args)
        {
            // Массив списков координат. (Каждый файл загружается в свой список.)
            List<CoordSet> coordSet = new List<CoordSet>();

            // Выбор режима работы
            if (args.Length == 0)
            {
                // Ввод с консоли
                Console.WriteLine("FormatCoords. Запуск без аргументов. Чтение с консоли.");
                coordSet.Add(MakeConsoleInput());
            }
            else
            {
                // Загрузка из текстовых файлов
                Console.WriteLine("FormatCoords. Запуск с аргументами. Чтение из файлов.");
                coordSet = MakeFileInput(args);
            }

            // Форматированный вывод
            foreach (CoordSet cs in coordSet)
            {
                Console.WriteLine(cs.Sourse);
                cs.MakeOutput();
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("Нажмите Enter.");
            Console.ReadLine();
        }
    }
}
