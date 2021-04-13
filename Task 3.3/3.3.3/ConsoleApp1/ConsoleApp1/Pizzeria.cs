using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Pizzeria
    {
        public event Action<User, string> NotifyUser;

        public void MakeOrder (User usr)
        {
            if (NotifyUser != null)
            {
                if (usr.OrderId != int.MinValue)
                    NotifyUser.Invoke(usr, $"Заказ {usr.OrderId} начали готовить! ");
                else
                    NotifyUser.Invoke(usr, $"Эй, {usr.UserName} закажи уже чего-нибудь!");
            }
            else
                Console.WriteLine("Пиццерия закрыта!");
        }

        public void TakeOrder(User usr)
        {
            if (NotifyUser != null)
            {
                if (usr.OrderId != int.MinValue)
                    NotifyUser.Invoke(usr, $"Заказ {usr.OrderId} готов! ");
            }
            else
                Console.WriteLine("Пиццерия закрыта!");
        }



    }
}
