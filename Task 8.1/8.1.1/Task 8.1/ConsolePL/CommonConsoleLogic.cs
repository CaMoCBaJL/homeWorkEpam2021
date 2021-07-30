using System;
using System.Collections.Generic;
using BLInterfaces;
using DataValidation;

namespace ConsolePL
{
    public class CommonConsoleLogic
    {
        public void ShowStrings(List<string> stringsToShow)
        {
            int counter = 1;

            foreach (var entity in stringsToShow)
            {
                if (!Validator.DoesStringContainsCommonParts(entity))
                {
                    Console.WriteLine(counter + ". " + entity);

                    Console.WriteLine();

                    counter++;
                }

                else
                    Console.WriteLine(entity);
            }
        }

        public List<int> AddConnectedEntities(ILogicLayer bL)
        {
            List<int> result = new List<int>();

            do
            {
                var remainingAwards = bL.GetConnectedEntities(result);

                new CommonConsoleLogic().ShowStrings(remainingAwards);

                Console.WriteLine("0. Конец.");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                        break;
                    else if (input > 0 && input < remainingAwards.Count)
                        result.Add(input);
                    else
                        Console.WriteLine("Выбранный пункт отсутствует в списке.");
                }
                else
                    Console.WriteLine("Ввод неверен!");

            } while (true);

            return result;
        }

        public void DeleteEntity(ILogicLayer bL)
        {
            Console.WriteLine("Выберите пользователя для удаления:");

            if (bL.GetEntities().Count > 0)
            {
                do
                {
                    new CommonConsoleLogic().ShowStrings(bL.GetConnectedEntitiesNames());

                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        if (result > 0 && result < bL.GetEntities().Count)
                        {
                            Console.WriteLine(bL.RemoveEntity(result));

                            return;
                        }
                        else
                            Console.WriteLine("Выбранный вариант отсутствует в списке!");
                    }
                    else
                        Console.WriteLine("Выбранный вариант отсутствует в списке!");

                } while (true);
            }
            else
                Console.WriteLine("В базе отсутствуют выбранные сущности.");
        }
    }
}
