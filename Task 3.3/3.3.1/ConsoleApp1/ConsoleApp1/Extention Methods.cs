using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    static class ExtentionMethods
    {
        static public double ApplyFuncToArr(this int[] arr, Func<int[], double> func)
        {

            if (func != null)
                return func.Invoke(arr);

            return int.MinValue;

        }

    }
}
