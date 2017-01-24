// Practice C# 01
// Консольное приложение для чтения, форматирования и отображения
// чисел, представляющих x,y координаты положения объекста.

using System;
using System.Collections.Generic;
using System.IO;

namespace FormatCoords
{
    ///<summary>Представляет пару координат x и y в двухмерном пространстве (на плоскости).</summary>
    public struct Point
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }

    ///<summary>Предназначен для хранения и форматированного вывода массива координат <see cref="Point"/>.</summary>
    public class CoordSet
    {
        List<Point> coords = new List<Point>();

        ///<summary>Добавляет координату в массив координат.</summary>
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

            coords.Add(newPoint);
        }

        ///<summary>Производит форматированный вывод массива координат.</summary>
        public void MakeOutput()
        {
            foreach (Point crd in coords)
            {
                Console.WriteLine("X = " + crd.X.ToString() + " Y = " + crd.Y.ToString());
            }
        }
    }


    class Program
    {
        ///<summary>Обеспечивает ввод координат пользователем с консоли.</summary>
        static CoordSet MakeConsoleInput()
        {
            string strLine;
            CoordSet cs = new CoordSet();

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

        ///<summary>Обеспечивает ввод координат из текстовых файлов.</summary>
        static List<CoordSet> MakeFileInput(string[] args)
        {
            List<CoordSet> csl = new List<CoordSet>();

            for (int i = 0; i < args.Length; i++)
            {
                StreamReader sr = new StreamReader(args[i]);
                csl.Add(new CoordSet());

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
            List<CoordSet> coordList = new List<CoordSet>();

            // Выбор режима работы
            if (args.Length == 0)
            {
                // Ввод с консоли
                Console.WriteLine("FormatCoords. Запуск без аргументов. Чтение с консоли.");
                coordList.Add(MakeConsoleInput());
            }
            else
            {
                // Загрузка из текстовых файлов
                Console.WriteLine("FormatCoords. Запуск с аргументами. Чтение из файлов.");
                coordList = MakeFileInput(args);
            }

            // Форматированный вывод
            foreach (CoordSet cf in coordList)
            {
                Console.WriteLine("FormatCoords. Форматированный вывод.");
                cf.MakeOutput();
            }

            Console.WriteLine("");
            Console.WriteLine("FormatCoords. Нажмите Enter.");
            Console.ReadLine();
        }
    }
}
