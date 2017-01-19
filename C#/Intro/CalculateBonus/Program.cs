using System;

namespace CalculateBonus
{
    ///<summary>Константы для вычисления налога с премии</summary>
    public static class TaxConstants
    {
        public static readonly decimal TaxBound = 10;
        public static readonly int TaxRateValue = 13;
    }

    ///<summary>Отделы</summary>
    public enum Departments : int
    {
        RD = 0,         // Отдел разработок
        QA = 1,         // Отдел тестирования
        Support = 2,    // Отдел технической поддержки
        Sales = 3,      // Отдел продаж
        Marketing = 4   // Отдел маркетинга
    }

    ///<summary>Должности</summary>
    public enum Ranks : int
    {
        Employee = 0,   // Сотрудник
        Lead = 1,       // Ведущий сотрудник
        Manager = 2,    // Руководитель
        Director = 3    // Директор
    }

    ///<summary>Класс Работник</summary>
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
        public decimal Bonus                            // Премия
        {
            get {return bonus; }
        }                  

        int taxRate;
        public int TaxRate                              // Ставка налога    
        {
            get { return taxRate; }
        }                                        

        bool taxTaken;
        public bool TaxTaken                            // Признак снятия налога
        {
            get { return taxTaken; }
        }

        ///<summary>Конструктор</summary>
        ///<param name="sn">Фамилия</param>
        ///<param name="rk">Должность</param>
        ///<param name="dp">Отдел</param>
        ///<param name="sl">Зарплата</param>
        ///<param name="sp">Salary Procent</param>
        ///<param name="kf">Koef</param>
        public Worker(string sn, Ranks rk, Departments dp, int sl, byte sp, double kf)
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

        ///<summary>Функция вычисления премии.</summary>
        void CalculateBonus()
        {
            bonus = (decimal)Salary * SalaryProcent / 100M  * (decimal)Koef;
            taxTaken = PayTax(ref bonus, out taxRate);
        }

        ///<summary>Функция списания налога с премии.</summary>
        ///<returns>Признак был ли списан налог.</returns>
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

        ///<summary>Функция форматированного вывода. Использует <seealso cref="System.Console.WriteLine()"/>.</summary>
        public void FormatWrite()
        {
            Console.WriteLine("Сотрудник: {0}", SurName);
            Console.WriteLine("Должность: {0}", Rank.ToString());
            Console.WriteLine("Отдел: {0}",     Department.ToString());
            Console.WriteLine("Премия: {0}",    Bonus.ToString());
            if (TaxTaken)
                Console.WriteLine("Включая налог: {0} %",     TaxRate.ToString());
            else
                Console.WriteLine("Не включая налог: {0} %",  TaxRate.ToString());
        }
    }


    class Program
    {
        ///<summary>Возвращаемые значения</summary>
        public enum ReturnCodes : int {
            AllOK = 0,      // Премия успешно начислена
            NoArg = -1,     // Не был передан обязательный (0й) аргумент
            WrongDep = -2,  // Указаный департамент не существует
            WrongRank = -3  // Указанная должность не существует
        }

        ///<summary>Точка входа в программу.</summary>
        ///<returns>Возвращает код <seealso cref="CalculateBonus.Program.ReturnCodes"/></returns>
        static int Main(string[] args)
        {
            string surName = "";
            Ranks rank = 0;
            Departments department = 0;
            int salary = 500;
            byte salaryProcent = 10;
            double koef = 0.90;

            // Разбор аргументов командной строки
            if (args.Length == 0)
                return (int)ReturnCodes.NoArg;

            if (args.Length >= 1)   // Код сотрудника и фамилия
            {
                try
                {
                    if (Enum.TryParse((args[0][0]).ToString(), out department) == false)
                        return (int)ReturnCodes.WrongDep;
                    else
                        if (Enum.IsDefined(typeof(Departments), department) == false)
                            return (int)ReturnCodes.WrongDep;

                    if (Enum.TryParse((args[0][1]).ToString(), out rank) == false)
                        return (int)ReturnCodes.WrongRank;
                    else
                        if (Enum.IsDefined(typeof(Ranks), rank) == false)
                            return (int)ReturnCodes.WrongRank;

                    surName = args[0].Substring(3);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("IndexOutOfRangeException: Неверный формат ввода аргумента кода и фамилии сотрудника!");
                    return (int)ReturnCodes.NoArg;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("ArgumentOutOfRangeException: Неверный формат ввода аргумента кода и фамилии сотрудника!");
                    return (int)ReturnCodes.NoArg;
                }

            }

            if (args.Length >= 2)   // Зарплата
                int.TryParse(args[1], out salary);

            if (args.Length >= 3)   // Процент от зарплаты, на выплату премии
                byte.TryParse(args[2], out salaryProcent);

            if (args.Length >= 4)   // Поправочный коэффициент
            {
                // Разделитель десятичных разрядов
                char DecSep = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                string tempStr = args[3].Replace('.', DecSep).Replace(',', DecSep);
                double.TryParse(tempStr, out koef);
            }

            // Создрание экземпляра класса и вывод результатов
            Worker Worker1 = new Worker(surName, rank, department, salary, salaryProcent, koef);
            Worker1.FormatWrite();

            return (int)ReturnCodes.AllOK;
        }
    }
}
