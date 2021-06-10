using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class User
    {
        public User(string userName)
        {

            if (!string.IsNullOrEmpty(userName) || !string.IsNullOrWhiteSpace(userName))
                UserName = userName;
            else
                throw new Exception("У клиента должно быть имя!");

            Console.WriteLine("Пользователь " + userName);

            Console.WriteLine();

            PizzaOrder = Order.MakeOrder();

            OrderId = Order.GetOrderId(PizzaOrder);
        }

        public string UserName { get; }

        public string PizzaOrder { get; set; }

        public int OrderId{ get;}

    }
}
