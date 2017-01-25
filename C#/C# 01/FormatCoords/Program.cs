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
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }

    /// <summary>Предназначен для хранения и форматированного вывода списка координат <see cref="Point"/>.</summary>
    public class CoordSet
    {
        public string Sourse { get; }
        List<Point> coords = new List<Point>();

        int maxXpre = 0;
        int maxXpost = 0;
        int maxYpre = 0;

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
            decimal x;
            decimal y;
            Point newPoint = new Point();
            string tempStr;

            strXY = coordStr.Split(',');
            if (strXY.Length != 2)
                return;

            // Разделитель десятичных разрядов
            char DecSep = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            tempStr = strXY[0].Replace('.', DecSep).Replace(',', DecSep);
            decimal.TryParse(tempStr, out x);

            tempStr = strXY[1].Replace('.', DecSep).Replace(',', DecSep);
            decimal.TryParse(tempStr, out y);

            newPoint.X = x;
            newPoint.Y = y;

            maxXpre  = Math.Max(maxXpre,  strXY[0].IndexOf(DecSep));
            maxXpost = Math.Max(maxXpost, strXY[0].Length - strXY[0].IndexOf(DecSep) - 1);
            maxYpre  = Math.Max(maxXpre,  strXY[1].IndexOf(DecSep));

            coords.Add(newPoint);
        }

        /// <summary>Производит форматированный вывод списка координат.</summary>
        public void MakeOutput()
        {
            foreach (Point crd in coords)
            {
                Console.WriteLine(string.Format("X: {0:#######.#######} Y: {1:#######.#######}", crd.X, crd.Y));
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
            }

            Console.WriteLine("");
            Console.WriteLine("Нажмите Enter.");
            Console.ReadLine();
        }
    }
}
