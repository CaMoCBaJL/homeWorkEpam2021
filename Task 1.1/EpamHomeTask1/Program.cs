using System;


namespace EpamHomeTask1
{
    class Program
    {
        static void TwoDimArray()
        {
            Random r = new Random();
            int n = r.Next(2, 5);
            int[,] arr = new int[n, n];
            int sum = 0;
            Console.WriteLine("Массив: ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr[i, j] = r.Next(-100, 100);
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < n; i++) //Вместо перебора всех чисел в массиве проще стартовать с первого четного элемента n-ой строки и просматривать все четные, т.е. шаг = 2. 
                                        //Если строка нечетная n = {1,3,5,..}, то первый четный элемент в ней - [n, 1], иначе - [n, 0].            
            {
                for (int j = i % 2; j < n; j += 2)
                    sum += arr[i, j];
            }
            Console.WriteLine("Сумма четных элементов - " + sum);
        }
        static void NonNegativeSum()
        {
            Random r = new Random();
            int n = r.Next(5, 20);
            int[] arr = new int[n];
            int sum = 0;
            Console.WriteLine("Массив: ");
            for (int i = 0; i < n; i++)
            {
                arr[i] = r.Next(-100, 100);
                Console.Write(arr[i] + " ");
            }
            for (int i = 0; i < n; i++)
                if (arr[i] >= 0)
                    sum += arr[i];
            Console.WriteLine($"{Environment.NewLine}Сумма неотрицательных элементов массива - {sum}");
        }
        static void NoPositivie()
        {
            void Show3DArray(int[,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            Console.Write($" {arr[i, j, k]}");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("-------------------------------------");
                }
            }
            Random r = new Random();
            int n = r.Next(1, 5);
            int[,,] arr = new int[n, n, n];//решил сделать трёхмерный массив размерности n x n x n.
            for (int i = 0; i < arr.GetLength(0); i++)//Использую GetLength, чтобы при изменении размеров массива программа все равно работала.
                for (int j = 0; j < arr.GetLength(1); j++)
                    for (int k = 0; k < arr.GetLength(2); k++)
                    {
                        arr[i, j, k] = r.Next(-1000, 1000);
                    }
            Console.WriteLine("Исходный массив:");
            Show3DArray(arr);
            for (int i = 0; i < arr.GetLength(0); i++)//Использую GetLength, чтобы при изменении размеров массива программа все равно работала.
                for (int j = 0; j < arr.GetLength(1); j++)
                    for (int k = 0; k < arr.GetLength(2); k++)
                    {
                        if (arr[i, j, k] > 0)
                            arr[i, j, k] = 0;
                    }
            Console.WriteLine("Измененный массив:");
            Show3DArray(arr);
        }
        static void Quicksort(ref int[] array, int start, int end)
        {
            int Partition(ref int[] array, int start, int end) // start - индекс первого элемента; end - индекс последнего элемента.
            {
                int temp;
                int marker = start;
                for (int i = start; i < end; i++)
                {
                    if (array[i] < array[end])
                    {
                        temp = array[marker];
                        array[marker] = array[i];
                        array[i] = temp;
                        marker += 1;
                    }
                }
                temp = array[marker];
                array[marker] = array[end];
                array[end] = temp;
                return marker;
            }

            if (start >= end)
            {
                return;
            }
            int pivot = Partition(ref array, start, end);
            Quicksort(ref array, start, pivot - 1);
            Quicksort(ref array, pivot + 1, end);
        }
        static void ArrayProcessing()
        {
            var r = new Random();
            int n = r.Next(5, 20);
            int[] arr = new int[n];
            int minValue = int.MaxValue;
            int maxValue = 0;
            for (int i = 0; i < n; i++)
            {
                arr[i] = r.Next(-100, 100);
                if (arr[i] > maxValue)
                    maxValue = arr[i];
                if (arr[i] < minValue)
                    minValue = arr[i];
            }
            Console.WriteLine("Минимальный элемент: " + minValue);
            Console.WriteLine("Максимальный элемент: " + maxValue);
            Console.WriteLine(Environment.NewLine + "Массив до сортировки:");
            foreach (int elem in arr)
                Console.Write(elem + " ");
            Quicksort(ref arr, 0, arr.Length - 1);
            Console.WriteLine(Environment.NewLine + "Массив после сортировки:");
            foreach (int elem in arr)
                Console.Write(elem + " ");
        }
        static void FontAdjustment()
        {
            int i = 1;
            while (i % 11 != 0)
            {
                Console.WriteLine($"Параметры надписи: {(i == 1 ? "None" : "")} {(i % 3 == 0 ? "Bold" : "")} " +
                    $"{(i % 5 == 0 ? "Italic" : "")} {(i % 7 == 0 ? "UnderLine" : "")}");
                Console.WriteLine("Введите:");
                Console.WriteLine("\t\t 1: bold");
                Console.WriteLine("\t\t 2: italic");
                Console.WriteLine("\t\t 3: underline");
                Console.WriteLine("\t\t 4: Выход");
                try
                {
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            if (i % 3 != 0)
                                i *= 3;
                            else
                                i /= 3;
                            break;
                        case 2:
                            if (i % 5 != 0)
                                i *= 5;
                            else
                                i /= 5;
                            break;
                        case 3:
                            if (i % 7 != 0)
                                i *= 7;
                            else
                                i /= 7;
                            break;
                        case 4:
                            if (i % 11 != 0)
                                i *= 11;
                            else
                                i /= 11;
                            break;
                        default:
                            Console.WriteLine(Environment.NewLine + "--------------------------------"
                        + "Ввод неверен!" + "--------------------------------" + Environment.NewLine);
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(Environment.NewLine + "--------------------------------"
                        + "Ввод неверен!" + "--------------------------------" + Environment.NewLine);
                }
            }
        }
        /// <summary>
        /// Вместо перебора тысячи чисел, проще просуммировать все кратные трём, начиная с 3, затем все кратные пяти, начиная с 5, за исключением кратных 3.
        /// </summary>
        /// <returns></returns>
        static int SumOfNumbers()
        {
            int res = 0;
            for (int i = 3; i < 1000; i += 3)
                res += i;
            for (int i = 5; i < 1000; i += 5)
                res += i % 3 == 0 ? 0 : i;
            return res;
        }
        static void X_MAS_TREE()
        {
            int n;
            Console.WriteLine("Введите высоту треугольника (число выводимых строк) :");
            try
            {
                n = int.Parse(Console.ReadLine());
                if (n > 0)
                {
                    AnotherTriangle(1, 2 * n - 1);
                    for (int i = 3; i <= 2 * n - 1; i += 2)
                        AnotherTriangle(i, 2 * n - 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        /// <summary>
        /// Метод для построения треугольника из "*".
        /// <para> <paramref name="n" /> - высота треугольника. </para>
        /// <para> <paramref name="len" /> - сдвиг, используется для отрисовки ёлочки. </para>
        /// </summary>
        /// <param name="n"></param>
        /// <param name="len"></param>
        static void AnotherTriangle(int n, int len)
        {
            for (int i = 1; i <= n; i += 2)
            {
                for (int j = 0; j < (len - n) / 2 + n / 2 - i / 2; j++)
                    Console.Write(" ");
                for (int j = 0; j < i; j++)
                    Console.Write("*");
                for (int j = n / 2 + i / 2; j <= 2 * n + len; j++)
                    Console.Write(" ");
                Console.WriteLine();
            }
        }

        static void AnotherTriangle()
        {
            int n;
            Console.WriteLine("Введите высоту треугольника (число выводимых строк) :");
            try
            {
                n = int.Parse(Console.ReadLine());
                if (n > 1)
                    AnotherTriangle(2 * n - 1, 0);//Инкапсулирую логику, чтобы вызывать её отдельно для этого задания и следующего.
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        static void PrintTriangle()
        {
            int n;
            Console.WriteLine("Введите высоту треугольника (число выводимых строк) :");
            try
            {
                n = int.Parse(Console.ReadLine());
                if (n > 0)
                    for (int i = 0; i <= n; i++)
                    {
                        for (int j = 0; j < i; j++)
                            Console.Write("*");
                        Console.WriteLine();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        static void CalculateRectangleSquare()
        {
            int a;
            int b;
            Console.WriteLine("Введите ширину и высоту прямоугольника (положительные целые числа):");
            try
            {
                a = int.Parse(Console.ReadLine());
                b = int.Parse(Console.ReadLine());
                if (a < 1 || b < 1)
                    Console.WriteLine("Ошибка! Введенные числа не соответствуют формату!");
                else
                    Console.WriteLine($"Площадь прямогуольника - {a * b}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        static void Main(string[] args)
        {
            int res = 0;
            while (res != 11)
            {
                Console.WriteLine("Выберите метод для демонстрации:");
                Console.WriteLine("1 - Calculate Rectangle Square.");
                Console.WriteLine("2 - Print Triangle.");
                Console.WriteLine("3 - Another Triangle.");
                Console.WriteLine("4 - XMAS-TREE.");
                Console.WriteLine("5 - SumOfNumbers.");
                Console.WriteLine("6 - FontAdjustment.");
                Console.WriteLine("7 - Array Processing.");
                Console.WriteLine("8 - No Positive.");
                Console.WriteLine("9 - Non Negative Sum.");
                Console.WriteLine("10 - Two Dim Array.");
                Console.WriteLine("11 - Выход.");
                if (int.TryParse(Console.ReadLine(), out res))
                    switch (res)
                    {
                        case 1:
                            CalculateRectangleSquare();
                            break;
                        case 2:
                            PrintTriangle();
                            break;
                        case 3:
                            AnotherTriangle();
                            break;
                        case 4:
                            X_MAS_TREE();
                            break;
                        case 5:
                            Console.WriteLine($"Сумма всех чисел, кратных 3 и 5 в 1000 = {SumOfNumbers()}");
                            break;
                        case 6:
                            FontAdjustment();
                            break;
                        case 7:
                            ArrayProcessing();
                            break;
                        case 8:
                            NoPositivie();
                            break;
                        case 9:
                            NonNegativeSum();
                            break;
                        case 10:
                            TwoDimArray();
                            break;
                        case 11:
                            break;
                        default:
                            Console.WriteLine("Пункт с введенным номером отсутствует в меню." + Environment.NewLine);
                            break;
                    }
                else
                    Console.WriteLine("Ввод неверен!" + Environment.NewLine);
            }
        }

    }
}

