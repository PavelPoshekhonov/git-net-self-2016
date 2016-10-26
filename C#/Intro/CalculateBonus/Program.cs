using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateBonus
{
    class Program
    {
        // Возвращаемые значения
        enum ReturnCodes : int {
            AllOK = 0,      // Премия успешно начислена
            NoArg = -1,     // Не был передан обязательный (0й) аргумент
            WrongDep = -2,  // Указаный департамент не существует
            WrongRank = -3  // Указанная должность не существует
        }

        // Отделы
        enum Departments : int {
            RD = 0,         // Отдел разработок
            QA,             // Отдел тестирования
            Support,        // Отдел технической поддержки
            Sales,          // Отдел продаж
            Marketing       // Отдел маркетинга
        }

        // Должности
        enum Ranks : int {
            Employee = 0,   // Сотрудник
            Lead,           // Ведущий сотрудник
            Manager,        // Руководитель
            Director        // Директор
        }

        static void Main(string[] args)
        {
        }
    }
}
