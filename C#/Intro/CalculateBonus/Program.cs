using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateBonus
{
    // Константы для вычисления налога с премии
    public static class TaxConstants
    {
        public static readonly decimal TaxBound = 10;
        public static readonly int TaxRateValue = 13;
    }

    // Отделы
    public enum Departments : int
    {
        RD = 0,         // Отдел разработок
        QA = 1,         // Отдел тестирования
        Support = 2,    // Отдел технической поддержки
        Sales = 3,      // Отдел продаж
        Marketing = 4   // Отдел маркетинга
    }

    // Должности
    public enum Ranks : int
    {
        Employee = 0,   // Сотрудник
        Lead = 1,       // Ведущий сотрудник
        Manager = 2,    // Руководитель
        Director =3     // Директор
    }

    // Класс Работник
    public class Worker
    {
        string surName;
        public string SurName                           // Фамилия
        {
            get { return surName.ToUpper(); }
            set { surName = value; }
        }
        public Ranks Rank { get; set; }                 // Должность
        public Departments Department { get; set; }     // Отдел

        int salary;
        public int Salary                               // Зарплата сотрудника
        {
            get { return salary; }
            set { salary = value;  CalculateBonus(); }
        }

        byte salaryProcent;
        public byte SalaryProcent                       // Процент от зарплаты, на выплату премии
        {
            get { return salaryProcent; }
            set { salaryProcent = value; CalculateBonus(); }
        }

        double koef;
        public double Koef                              // Поправочный коэффиециент
        {
            get { return koef; }
            set { koef = value; CalculateBonus(); }
        }

        decimal bonus;
        public decimal Bonus { get; }                   // Премия

        int taxRate;
        public int TaxRate { get; }                     // Ставка налога                       

        bool taxTaken;
        public bool TaxTaken { get; }                   // Признак снятия налога

        // Конструктор
        public Worker(string sn, Ranks rk, Departments dp, int sl = 500, byte sp = 10, double kf = 0.90)
        {
            // Фамилия, Должность, Отдел
            SurName = sn;
            Rank = rk;
            Department = dp;

            // Зарплата и премия
            salary = sl;
            salaryProcent = sp;
            koef = kf;
            CalculateBonus();
        }

        // Функция вычисления премии
        void CalculateBonus()
        {
            bonus = (decimal)Salary * SalaryProcent / 100M  * (decimal)Koef;
            taxTaken = PayTax(ref bonus, out taxRate);
        }

        // Функция списания налога с премии
        bool PayTax(ref decimal bn, out int tr)
        {
            tr = TaxConstants.TaxRateValue;
            if (bn > TaxConstants.TaxBound)
            {
                bn = bn * (100M - tr) / 100M;
                return true;
            }
            else
                return false;
        }
        
        // Функция форматированного вывода
        public void FormatWrite()
        {
            Console.WriteLine("Сотрудник: {0}", SurName);
            Console.WriteLine("Должность: {0}", Rank.ToString());
            Console.WriteLine("Отдел: {0}",     Department.ToString());
            Console.WriteLine("Премия: {0}",    Bonus.ToString());
            if (TaxTaken)
                Console.WriteLine("Включая налог: {0}",     TaxRate.ToString());
            else
                Console.WriteLine("Не включая налог: {0}",  TaxRate.ToString());
        }
    }


    class Program
    {
        // Возвращаемые значения
        public enum ReturnCodes : int {
            AllOK = 0,      // Премия успешно начислена
            NoArg = -1,     // Не был передан обязательный (0й) аргумент
            WrongDep = -2,  // Указаный департамент не существует
            WrongRank = -3  // Указанная должность не существует
        }

        static int Main(string[] args)
        {
            return (int)ReturnCodes.AllOK;
        }
    }
}
