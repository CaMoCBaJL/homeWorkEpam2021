using System;
using BLInterfaces;

namespace ConsolePL
{
    class PresentationLayer
    {
        private IBLController _BLL;


        public PresentationLayer(IBLController bll)
        {
            _BLL = bll;
        }

        public void Start()
        {
            do
            {
                ShowMenu();

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    ChooseTheAction(input);
                }
                else
                    Console.WriteLine("Ошибка ввода!");

            } while (true);
        }

        void ShowMenu()
        {
            Console.WriteLine();

            Console.WriteLine("Выберите действие:");

            Console.WriteLine("1. Просмотреть список пользователей.");

            Console.WriteLine("2. Просмотреть список наград.");

            Console.WriteLine("3. Добавить пользователя.");

            Console.WriteLine("4. Добавить награду.");

            Console.WriteLine("5. Удалить пользователя.");

            Console.WriteLine("6. Удалить награду.");

            Console.WriteLine("7. Выход.");

            Console.WriteLine();
        }

        void ChooseTheAction(int input)
        {
            switch (input)
            {
                case 1:
                    new CommonConsoleLogic().ShowStrings(_BLL.UserLogic.GetEntities());
                    break;
                case 2:
                    new CommonConsoleLogic().ShowStrings(_BLL.AwardLogic.GetEntities());
                    break;
                case 3:
                    new UserPL(_BLL.UserLogic).AddUser();
                    break;
                case 4:
                    new AwardPL(_BLL.AwardLogic).AddAward();
                    break;
                case 5:
                    new UserPL(_BLL.UserLogic).DeleteUser();
                    break;
                case 6:
                    new AwardPL(_BLL.AwardLogic).DeleteAward();
                    break;
                case 7:
                    Console.WriteLine("Конец работы.");
                    Environment.Exit(123123);
                    break;
                default:
                    Console.WriteLine("Введенный пункт отсутствует в меню");
                    break;
            }
        }
    }
}
