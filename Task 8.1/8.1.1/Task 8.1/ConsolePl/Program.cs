using System;
using BLL;

namespace ConsolePl
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Menu();

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    switch (input)
                    {
                        case 1:
                            ShowUsers();
                            break;
                        case 2:
                            ShowAwards();
                            break;
                        case 3:
                            AddUserDialogue();
                            break;
                        case 4:
                            AddAwardDialogue();
                            break;
                        case 5:
                            DeleteUserDialogue();
                            break;
                        case 6:
                            DeleteAwardDialogue();
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
                else
                    Console.WriteLine("Ошибка ввода!");

            } while (true) ;
        }

        static void Menu()
        {
            Console.WriteLine("Выберите действие:");

            Console.WriteLine("1. Просмотреть список пользователей.");

            Console.WriteLine("2. Просмотреть список наград.");

            Console.WriteLine("3. Добавить пользователя.");

            Console.WriteLine("4. Добавить награду.");

            Console.WriteLine("5. Удалить пользователя.");

            Console.WriteLine("6. Удалить награду.");

            Console.WriteLine("7 Выход.");
        }

        static void ShowUsers()
        {
            foreach (var user in BuisnessLogic.GetListOfUsers())
            {
                Console.WriteLine(user.ToString());
            }
        }

        static void ShowAwards()
        {
            foreach (var award in BuisnessLogic.GetListOfAwards())
            {
                Console.WriteLine(award.ToString());
            }
        }

        static void AddUserDialogue()
        {

        }

        static void AddAwardDialogue()
        {

        }

        static void DeleteUserDialogue()
        {

        }

        static void DeleteAwardDialogue()
        {

        }

    }
}
