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
            double firstvalue, secondvalue;
            string operatorV;
            bool endApp = false;

            while (!endApp)
            {
                Console.WriteLine("Введите первый операнд: ");
                firstvalue = Double.Parse(Console.ReadLine());

                Console.WriteLine("Введите второй операнд: ");
                secondvalue = Double.Parse(Console.ReadLine());

                Console.WriteLine("Выберите операцию: + , - , * , /");
                operatorV = Console.ReadLine();

                switch (operatorV)
                {
                    case "+":
                        Console.WriteLine(firstvalue + secondvalue);
                        break;

                    case "-":
                        Console.WriteLine(firstvalue - secondvalue);
                        break;

                    case "*":
                        Console.WriteLine(firstvalue * secondvalue);
                        break;

                    case "/":
                        if (secondvalue == 0)
                            Console.WriteLine("Нельзя делить на 0!");
                        else
                            Console.WriteLine(firstvalue / secondvalue);
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда, повторите попытку еще раз!");
                        break;
                }

                Console.Write("Введите 'bye' и Enter чтобы завершить работу, или нажмите любую другую клавишу и Enter чтобы продолжить: ");
                if (Console.ReadLine() == "bye") endApp = true;

                Console.WriteLine("\n");
            }
        }
    }
}
