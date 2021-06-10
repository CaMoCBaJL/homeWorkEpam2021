using System;
using System.Linq;

namespace Task_3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            CycledDynamicArray<int> arr = new CycledDynamicArray<int>(new int[] { 1, 2, 3, 4, 5, 6 });

            Console.WriteLine(new CycledDynamicArray<int>());

            arr.Add(7);

            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }

            //DynamicArray<int> arr = new DynamicArray<int>(new int[] { 1, 2, 3, 4, 5, 6 });

            //Console.WriteLine(arr.Capacity);

            //Console.WriteLine(arr.Length);

            //Console.WriteLine();

            //arr.Add(7);

            //Console.WriteLine(arr.Capacity);

            //Console.WriteLine(arr.Length);

            //Console.WriteLine();

            //arr.AddRange(Enumerable.Range(8, 24));

            //Console.WriteLine(arr.Capacity);

            //Console.WriteLine(arr.Length);

            //Console.WriteLine();

            //foreach (var item in arr)
            //{
            //    Console.Write($"{item} ");
            //}

            //Console.WriteLine();

            //arr.Insert(-2, arr.Length);

            //arr.Insert(-1, 0);

            //arr.Insert(-3, 15);

            //foreach (var item in arr)
            //{
            //    Console.Write($"{item} ");
            //}

            //Console.WriteLine();

            //Console.WriteLine();

            //arr.Remove(-2);

            //arr.Remove(-1);

            //arr.Remove(-3);

            //foreach (var item in arr)
            //{
            //    Console.Write($"{item} ");
            //}

            //Console.WriteLine();

            //Console.WriteLine();

            //for (int i = -1; i > -arr.Length; i--)
            //{
            //    Console.Write($"{arr[i]} ");
            //}


        }
    }
}
