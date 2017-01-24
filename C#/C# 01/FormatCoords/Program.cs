// Practice C# 01
// Консольное приложение для чтения, форматирования и отображения
// чисел, представляющих x,y координаты положения объекста.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatCoords
{
    ///<summary>Представляет пару координат x и y в двухмерном пространстве (на плоскости).</summary>
    public struct Point
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }

    ///<summary>Предназначен для хранения и форматированного вывода массива координат <see cref="Point"/>.</summary>
    public class CoordFormatter
    {
        List<Point> coords = new List<Point>();

        ///<summary>Добавляет координату в массив координат.</summary>
        public void Add(string coordStr)
        {
        }

        ///<summary>Производит форматированный вывод массива координат.</summary>
        public void MakeOutput()
        {
            foreach (Point crd in coords)
            {
                Console.WriteLine(crd.X.ToString(), crd.Y.ToString());
            }
        }
    }


    class Program
    {
        List<CoordFormatter> coordList = new List<CoordFormatter>();

        ///<summary>Обеспечивает ввод координат из текстовых файлов.</summary>
        static List<CoordFormatter> FormatFileInput(string[] args)
        { }

        ///<summary>Обеспечивает ввод координат пользователем с консоли.</summary>
        static CoordFormatter FormatConsoleInput()
        { }


        static void Main(string[] args)
        {
            // Выбор режима работы
            if (args.Length == 0)
            {
                coordList = FormatConsoleInput();
            }
            else
            {
                coordList = FormatFileInput(args);
            }

            foreach (CoordFormatter cf in coordList)
            {
                cf.MakeOutput();
            }

        }
    }
}
