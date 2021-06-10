using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    static class Order
    {
        public static string MakeOrder()
        {
            Console.WriteLine("Формаирование заказа начато:");

            Dictionary<string, int> order = new Dictionary<string, int>();

            while (true)
            {
                ShowMenu();

                if (int.TryParse(Console.ReadLine(), out int r))
                {
                    switch (r)
                    {
                        case 1:
                            AddToOrder(ref order, "1. Мексиканская");
                            break;

                        case 2:
                            AddToOrder(ref order, "2. 4 сыра");
                            break;

                        case 3:
                            AddToOrder(ref order, "3. Домашняя");
                            break;

                        case 4:
                            AddToOrder(ref order, "4. Пепперони");
                            break;

                        case 5:
                            Console.WriteLine(ShowOrder(order));
                                break;

                        case 6:
                            return ShowOrder(order);

                        default:
                            Console.WriteLine("Номер введенного варианта отсутствует в списке.");
                            break;


                    }
                }
                else
                    Console.WriteLine("Ввод неверен!");
            }

        }

        private static string ShowOrder(Dictionary<string ,int> order)
        {
            StringBuilder result = new StringBuilder();


            if (order.Count != 0)
            {
                foreach (var item in order)
                {
                    result.Append($"{item.Key},{item.Value + Environment.NewLine}");
                }
                return result.ToString();
            }

            return "Пока заказ пуст.";

        }

        private static void AddToOrder(ref Dictionary<string, int> order, string key)
        {
            if (order.ContainsKey(key))
            {
                int currentAmount = order.GetValueOrDefault(key);

                order.Remove(key);

                order.Add(key, currentAmount + 1);
            }
            else
            {
                order.Add(key, 1);
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пиццу:");
            Console.WriteLine("1. Мексиканская");
            Console.WriteLine("2. 4 сыра");
            Console.WriteLine("3. Домашняя");
            Console.WriteLine("4. Пепперони");
            Console.WriteLine("5. Вывод заказа");
            Console.WriteLine("6. Выход.");
            Console.WriteLine();
        }

        public static int GetOrderId(string order)
        {

            if (order != "Пока заказ пуст.")
            {
                int r = 0;

                foreach (var item in order.Split(Environment.NewLine))
                    if(item != string.Empty)
                    r += (int.Parse(item.Substring(0, 1)) * int.Parse(item.Split(',')[1]));

                return r;
            }
            else
            {
                return int.MinValue;
            }
        }

    }
}
