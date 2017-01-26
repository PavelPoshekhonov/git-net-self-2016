// Practice C# 01
// Консольное приложение для чтения, форматирования и отображения
// чисел, представляющих x,y координаты положения объекста.

using System;
using System.Collections.Generic;
using System.IO;

namespace FormatCoords
{
    /// <summary>Консольное приложение для чтения, форматирования и отображения чисел, представляющих x,y координаты положения объекста.</summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Массив списков координат. (Каждый файл загружается в свой список.)
            List<CoordSet> coordSet = new List<CoordSet>();

            // Выбор режима работы
            if (args.Length == 0)
            {
                // Ввод с консоли
                Console.WriteLine("FormatCoords. Запуск без аргументов. Чтение с консоли.");
                Console.WriteLine("Для завершения ввода введите пустую строку.");
                Console.WriteLine("");
                coordSet.Add(MakeConsoleInput());
            }
            else
            {
                // Загрузка из текстовых файлов
                Console.WriteLine("FormatCoords. Запуск с аргументами. Чтение из файлов.");
                Console.WriteLine("");
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
    }
}
