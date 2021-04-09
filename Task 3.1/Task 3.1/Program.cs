using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
                if (int.TryParse(Console.ReadLine(), out int variant))
                {
                    switch (variant)
                    {
                        case 1:
                            WeakestLink(false);
                            break;
                        case 2:
                            WeakestLink(true);
                            break;
                        case 3:
                            TextAnalysis();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Введеного номера нет в списке вариантов!");
                            break;
                    }
                }
                else
                    Console.WriteLine("Ввод неверен!");
            }
        }

        static int EnterTheNumber()
        {
            int n;

            do
            {
                Console.WriteLine("Введите число:");
                if (int.TryParse(Console.ReadLine(), out n))
                    break;
                else
                    Console.WriteLine("Ввод неверен! ");
            } while (true);

            return n;

        }

        static void ShowMenu()
        {
            Console.WriteLine("Выберите задачу для исполнения:");
            Console.WriteLine("1. Weakest Link");
            Console.WriteLine("2. Weakest Link*");
            Console.WriteLine("3. Text Analysis.");
            Console.WriteLine("4. Выход.");
        }

        static void WeakestLink(bool isComplexed)
        {
            Console.WriteLine("N:");
            int n = EnterTheNumber();

            int k;
            if (!isComplexed)
                k = 2;
            else
            {
                Console.WriteLine("K:");
                k = EnterTheNumber();
                while(k < 2)
                {
                    Console.WriteLine("Ввод неверен!");
                    k = EnterTheNumber();
                }
            }

            Console.WriteLine($"Сгенерирован круг людей. Начинаем вычеркивать каждого {k}-го");

            List<int> players = new List<int>(Enumerable.Range(1, n));

            int i = 1;

            while (players.Count > k - 1)
                {
                Console.WriteLine("Выбыл - " + players[k - 1].ToString());
                players.RemoveAt(k - 1);
                players = new List<int>(players.Skip(k - 1).Take(players.Count - k + 1).Concat(players.Take(k - 1)));                
                Console.WriteLine($" Раунд {i}. Вычеркнут человек. Людей осталось: {players.Count}");
                i++;
            }

            Console.WriteLine(" Игра окончена. Невозможно вычеркнуть больше людей");

        }

        static void ShowTextMenu()
        {
            Console.WriteLine("Текст проанализирован. Выберите действие:");
            Console.WriteLine("1. Вывод самых распространенных слов.");
            Console.WriteLine("2. Вывод уникальных слов.");
            Console.WriteLine("3. Вывод исходного текста.");
            Console.WriteLine("4. Общая оценка текста");
            Console.WriteLine("5. Выход." + Environment.NewLine);
        }

        static void ShowTextInfo(int amountOfRepeatedWords, string mostCommonWord, Dictionary<string, int> analisysResult, int textLength)
        {
            Console.WriteLine($"Число неуникальных слов (встречающихся чаще 1 раза) - {amountOfRepeatedWords}");

            Console.WriteLine();

            Console.WriteLine("Самое распространенное слово - " + mostCommonWord);

            Console.WriteLine();

            Console.WriteLine($"Процент оригинальных слов в тексте {((analisysResult.Count - amountOfRepeatedWords) / (double)analisysResult.Count) * 100}");

            Console.WriteLine();
            
            Console.WriteLine("Всего слов в тексте (кроме знаков препинания) - " + textLength);

            Console.WriteLine();
        }

        static void TextAnalysis()//В моей реализации регистр слов в тексте не имеет значения.
        {
            Console.WriteLine("Введите текст, для окончания ввода ввдеите /stop/");

            StringBuilder text = new StringBuilder();
            string s = default;
            while (s != "/stop/")
            {
                s = Console.ReadLine();
                text.Append(s);
            }

            List<string> words = new List<string>(text.ToString().ToLower().Split(',', ' ', '.', '!', '\r', '\n'));
            words.RemoveAll(s => s == string.Empty);
            words.RemoveAll(s => s == "-");
             
            int textLength = words.Count;

            Dictionary<string, int> analisysResult = new Dictionary<string, int>();

            while (words.Count > 0)
            {
                string curWord = words[0];
                analisysResult.Add(words[0], words.Count(s => s == curWord));
                words.RemoveAll(s => s == curWord);
            }

            do
            {
                ShowTextMenu();
                if (int.TryParse(Console.ReadLine(), out int r))
                {
                    switch (r)
                    {
                        case 1:
                            foreach (var item in analisysResult.OrderByDescending(pair => pair.Value))
                            {
                                if (item.Value > 1)
                                    Console.WriteLine($"Слово {item.Key} встречается {item.Value} раз.");
                                else
                                    break;
                            }
                            break;
                        case 2:
                            foreach (var item in analisysResult.OrderBy(pair => pair.Value))
                            {
                                if (item.Value < 2)
                                    Console.WriteLine($"Слово {item.Key} встречается {item.Value} раз.");
                                else
                                    break;
                            }
                            break;
                        case 3:
                            Console.WriteLine(text.ToString() + Environment.NewLine);
                            break;
                        case 4:
                            int amountOfRepeatedWords = default;

                            string mostCommonWord = default;

                            foreach (var item in analisysResult.OrderByDescending(pair => pair.Value))
                            {
                                if (item.Value == analisysResult.Values.Max())
                                    mostCommonWord = item.Key;

                                if (item.Value > 1)
                                    amountOfRepeatedWords++;
                                else
                                    break;
                            }

                            ShowTextInfo(amountOfRepeatedWords, mostCommonWord, analisysResult, textLength);
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Введенный вариант отсутствует в меню выбора." + Environment.NewLine);
                            break;
                    }
                }
                else
                    Console.WriteLine("Ввод неверен!" + Environment.NewLine);
            } while (true);
        }


    }
}
