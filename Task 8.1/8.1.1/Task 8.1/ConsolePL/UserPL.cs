using System;
using System.Collections.Generic;
using BLInterfaces;
using DataValidation;

namespace ConsolePL
{
    public class UserPL
    {
        private ILogicLayer _BL;

        public UserPL(ILogicLayer bl)
        {
            _BL = bl;
        }

        public void AddUser()
        {
            List<string> userInfo = new List<string>();

            userInfo.Add(_BL.GetEntities().Count.ToString());

            Console.WriteLine("Введите имя пользователя");

            userInfo.Add(Console.ReadLine());

            userInfo.Add(CorrectParameterInput("Дата рождения(дд.мм.гг)", StringConstants.birthDateRegexPattern) + Environment.NewLine);

            userInfo.Add(CorrectParameterInput("Возраст", StringConstants.ageRegexPattern));

            Console.WriteLine($"{Environment.NewLine}Наградите пользователя:{Environment.NewLine}");

            Console.WriteLine(_BL.AddEntity(userInfo, AddAwards(), string.Empty));

            Console.WriteLine();
        }

        string CorrectParameterInput(string paramName, string paramRegex)
        {
            do
            {
                string result = Console.ReadLine();

                if (Validator.ValidateParameter(result, paramRegex))
                    return result;
                else
                    Console.WriteLine($"Параметр введен неверно!({paramName})");

            } while (true);
        }

        List<int> AddAwards()
            => new CommonConsoleLogic().AddConnectedEntities(_BL);

        public void DeleteUser()
            => new CommonConsoleLogic().DeleteEntity(_BL);

    }
}
