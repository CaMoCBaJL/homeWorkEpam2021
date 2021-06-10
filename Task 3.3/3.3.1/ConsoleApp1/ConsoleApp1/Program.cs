using System;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3, 4, 5 };

            Console.WriteLine($"Сумма элементов в массиве: {arr.ApplyFuncToArr(Funcs.sum)}");

            Console.WriteLine($"Среднее значение в массиве: {arr.ApplyFuncToArr(Funcs.averageElem)}");

            Console.WriteLine($"Самый распространенный элемент: {arr.ApplyFuncToArr(Funcs.mostRecentElem)}");
        }
        

    }

}
