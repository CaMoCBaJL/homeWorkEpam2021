using System;
using System.Text;
using System.Reflection;
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
                    ChooseTheAction(input);
                }
                else
                    Console.WriteLine("Ошибка ввода!");

            } while (true) ;
        }

        static void Menu()
        {
            Console.WriteLine();

            Console.WriteLine("Выберите действие:");

            Console.WriteLine("1. Просмотреть список пользователей.");

            Console.WriteLine("2. Просмотреть список наград.");

            Console.WriteLine("3. Добавить пользователя.");

            Console.WriteLine("4. Добавить награду.");

            Console.WriteLine("5. Удалить пользователя.");

            Console.WriteLine("6. Удалить награду.");

            Console.WriteLine("7 Выход.");
        }

        static void ChooseTheAction(int input)
        {
            switch (input)
            {
                case 1:
                    ShowEntities(EntityType.User);
                    break;
                case 2:
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

        static void ShowEntities(EntityType entityType)
        {
            foreach (var entity in BuisnessLogic.GetListOfEntities(entityType))
            {
                Console.WriteLine(entity);

                Console.WriteLine();
            }
        }

        static void AddEntityDialogue(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.User:
                    StringBuilder userInfo = new StringBuilder();
                    
                    Console.WriteLine("Введите имя пользователя:");

                    userInfo.Append(Console.ReadLine() + Environment.NewLine);

                    userInfo.Append(BuisnessLogic.CorrectInputTheParameter("Дата рождения(дд.мм.гг)", BuisnessLogic.birthDateRegexPattern) + Environment.NewLine);

                    userInfo.Append(BuisnessLogic.CorrectInputTheParameter("Возраст", BuisnessLogic.ageRegexPattern));

                    Console.WriteLine(BuisnessLogic.AddEntity(entityType, userInfo.ToString()));

                    break;
                case EntityType.Award:
                    Console.WriteLine("Введите название награды:");

                    Console.WriteLine(BuisnessLogic.AddEntity(entityType, Console.ReadLine()));
                    break;
                case EntityType.None:
                default:
                    return;
            }
        }

        static void DeleteEntityDialogue(EntityType entityType)
        {
            Console.WriteLine($"Выберите {entityType.ToString()} для удаления:");

            if (BuisnessLogic.GetListOfEntities(entityType).Count > 0)
            {
                do
                {
                    ShowEntities(entityType);

                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        Console.WriteLine(BuisnessLogic.RemoveEntity(entityType, result));

                        return;
                    }
                    else
                        Console.WriteLine("Выбранный вариант отсутствует в списке!");

                } while (true);
            }
            else
                Console.WriteLine("В базе отсутствуют " + entityType.ToString());
        }
    }
}
