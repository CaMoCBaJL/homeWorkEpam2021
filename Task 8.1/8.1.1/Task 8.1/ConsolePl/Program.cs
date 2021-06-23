using System;
using BLL;
using Entities;

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
                    ActionChoose(input);
                }
                else
                    Console.WriteLine("Ошибка ввода!");

            } while (true) ;
        }

        static void ActionChoose(int input)
        {
            switch (input)
            {
                case 1:
                    Console.WriteLine("Список пользователей:" + Environment.NewLine);

                    ShowEntities(EntityType.User);
                    break;
                case 2:
                    Console.WriteLine("Список наград:" + Environment.NewLine);

                    ShowEntities(EntityType.Award);
                    break;
                case 3:
                    AddEntityDialogue(EntityType.User);
                    break;
                case 4:
                    AddEntityDialogue(EntityType.Award);
                    break;
                case 5:
                    DeleteEntityDialogue(EntityType.User);
                    break;
                case 6:
                    DeleteEntityDialogue(EntityType.Award);
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

        static void ShowEntities(EntityType entityType)
        {
            foreach (var entity in BuisnessLogic.GetListOfEntities(entityType))
                Console.WriteLine(entity);
        }

        static void AddEntityDialogue(EntityType entityType)
        {

        }

        static void DeleteEntityDialogue(EntityType entityType)
        {
            Console.WriteLine($"Выберите удаляемого {entityType.ToString()}:");

            do
            {
                ShowEntities(entityType);

                if(int.TryParse(Console.ReadLine(), out int result))
                {
                    Console.WriteLine(BuisnessLogic.RemoveEntity(entityType, result));

                    return;
                }
                else
                    Console.WriteLine("Выбранный вариант отсутствует в списке!");

            } while (true);
        }
    }
}
