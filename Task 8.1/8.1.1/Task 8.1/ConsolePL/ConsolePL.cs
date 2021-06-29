using System;
using System.Text;
using BLL;
using Entities;
using System.Collections.Generic;

namespace ConsolePL
{
    class PresentationLayer
    {
        public void Start()
        {
            do
            {
                Menu();

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    BuisnessLogic.CheckDataLocation();

                    ChooseTheAction(input);
                }
                else
                    Console.WriteLine("Ошибка ввода!");

            } while (true);
        }

        void Menu()
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

        void ShowEntities(EntityType entityType)
        {
            var bll = new BuisnessLogic();

            ShowSomeStrings(bll.GetListOfEntities(entityType, false));
        }

        void ShowSomeStrings(List<string> stringsToShow)
        {
            int counter = 1;

            foreach (var entity in stringsToShow)
            {
                if (!BuisnessLogic.DoesStringContainsCommonParts(entity))
                {
                    Console.WriteLine(counter + ". " + entity);

                    Console.WriteLine();

                    counter++;
                }

                else
                    Console.WriteLine(entity);
            }
        }

        void AddEntityDialogue(EntityType entityType)
        {
            var bll = new BuisnessLogic();

            switch (entityType)
            {
                case EntityType.User:
                    StringBuilder userInfo = new StringBuilder();

                    Console.WriteLine("Введите имя пользователя:");

                    userInfo.Append(Console.ReadLine() + Environment.NewLine);

                    userInfo.Append(CorrectInputTheParameter("Дата рождения(дд.мм.гг)", BuisnessLogic.birthDateRegexPattern) + Environment.NewLine);

                    userInfo.Append(CorrectInputTheParameter("Возраст", BuisnessLogic.ageRegexPattern));

                    Console.WriteLine($"{Environment.NewLine}Наградите пользователя:{Environment.NewLine}");

                    Console.WriteLine(bll.AddEntity(entityType, userInfo.ToString(), AddConnectedIds(entityType)));

                    Console.WriteLine();

                    break;
                case EntityType.Award:
                    Console.WriteLine("Введите название награды:");

                    string awardTitle;

                    do
                    {
                        awardTitle = Console.ReadLine();

                        if (string.IsNullOrEmpty(awardTitle.Trim()))
                            Console.WriteLine("Ошибка ввода!");
                        else
                            break;
                    } while (true);

                    Console.WriteLine($"{Environment.NewLine}Выберите пользователей для награждения созданной наградой:{Environment.NewLine}");

                    Console.WriteLine(bll.AddEntity(entityType, awardTitle, AddConnectedIds(entityType)));
                    break;
                case EntityType.None:
                default:
                    return;
            }
        }

        public string CorrectInputTheParameter(string entityField, string regularExpression)
        {
            string parameter;

            do
            {
                Console.WriteLine($"Введите параметр " + entityField);

                parameter = Console.ReadLine();

                if (BuisnessLogic.ValidateParameter(parameter, regularExpression))
                    return parameter;
                else
                    Console.WriteLine("Ввод неверен");


            } while (true);
        }


        List<int> AddConnectedIds(EntityType entityType)
        {
            var bll = new BuisnessLogic();

            List<int> result = new List<int>();

            EntityType connectedEntitiesType;

            if (entityType == EntityType.User)
                connectedEntitiesType = EntityType.Award;

            else if (entityType == EntityType.Award)
                connectedEntitiesType = EntityType.User;
            else
                return new List<int>();
            do
            {
                var remainingAwards = bll.GetListOfEntities(connectedEntitiesType, result);

                ShowSomeStrings(remainingAwards);

                Console.WriteLine("0. Конец.");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                        break;
                    else if (input > 0 && input < remainingAwards.Count)
                        result.Add(BuisnessLogic.GetEntityId(connectedEntitiesType, remainingAwards[input]));
                    else
                        Console.WriteLine("Выбранный пункт отсутствует в списке.");
                }
                else
                    Console.WriteLine("Ввод неверен!");

            } while (true);

            return result;
        }

        void DeleteEntityDialogue(EntityType entityType)
        {
            var bll = new BuisnessLogic();

            Console.WriteLine($"Выберите {entityType.ToString()} для удаления:");

            if (bll.GetListOfEntities(entityType, true).Count > 0)
            {
                do
                {
                    ShowEntities(entityType);

                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        Console.WriteLine(bll.RemoveEntity(entityType, result));

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
