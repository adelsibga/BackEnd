using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            double firstvalue = 0, secondvalue = 0;
            string operatorV;
            bool endApp = false;

            while (!endApp)
            {

                try
                {
                    Console.WriteLine("Введите первое число: ");
                    firstvalue = Double.Parse(Console.ReadLine());

                    Console.WriteLine("Введите второе число: ");
                    secondvalue = Double.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели не число");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("Выберите операцию: + , - , * , /");
                operatorV = Console.ReadLine();

                switch (operatorV)
                {
                    case "+":
                        if (((firstvalue + secondvalue) > double.MaxValue) || ((firstvalue + secondvalue) < double.MinValue))
                        {
                            Console.WriteLine("Переполнение!");
                        }
                        else
                            Console.WriteLine(firstvalue + secondvalue);
                        break;

                    case "-":
                        if ((firstvalue - secondvalue) > double.MaxValue || ((firstvalue - secondvalue) < double.MinValue))
                        {
                            Console.WriteLine("Переполнение!");
                        }
                        else
                            Console.WriteLine(firstvalue - secondvalue);
                        break;

                    case "*":
                        if ((firstvalue * secondvalue) > double.MaxValue || ((firstvalue * secondvalue) < double.MinValue))
                        {
                            Console.WriteLine("Переполнение!");
                        }
                        else
                            Console.WriteLine(firstvalue * secondvalue);
                        break;

                    case "/":
                        if (secondvalue == 0)
                            Console.WriteLine("Нельзя делить на 0!");
                        else if ((firstvalue / secondvalue) > double.MaxValue || ((firstvalue / secondvalue) < double.MinValue))
                        {
                            Console.WriteLine("Переполнение!");
                        }
                        else
                            Console.WriteLine(firstvalue / secondvalue);
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда, повторите попытку еще раз!");
                        break;
                }

                Console.Write("Введите 'bye' и Enter чтобы завершить работу, или нажмите любую другую клавишу и Enter чтобы продолжить: ");
                if (Console.ReadLine() == "bye") endApp = true;

                Console.WriteLine();
            }
        }
    }
}
