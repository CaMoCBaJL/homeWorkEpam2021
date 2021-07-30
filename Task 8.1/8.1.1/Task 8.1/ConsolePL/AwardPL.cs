using BLInterfaces;
using System;
using System.Collections.Generic;

namespace ConsolePL
{
    public class AwardPL
    {
        private ILogicLayer _BL;

        public AwardPL(ILogicLayer bl)
        {
            _BL = bl;
        }

        public void AddAward()
        {
            List<string> userInfo = new List<string>();

            userInfo.Add(_BL.GetEntities().Count.ToString());

            Console.WriteLine("Введите название награды");

            userInfo.Add(Console.ReadLine());

            Console.WriteLine($"{Environment.NewLine}Выберите пользователей," +
                $" представленных к награждению:{Environment.NewLine}");

            Console.WriteLine(_BL.AddEntity(userInfo, AddAwardedUsers(), string.Empty));

            Console.WriteLine();
        }

        List<int> AddAwardedUsers()
            => new CommonConsoleLogic().AddConnectedEntities(_BL);

        public void DeleteAward()
            => new CommonConsoleLogic().DeleteEntity(_BL);
    }
}