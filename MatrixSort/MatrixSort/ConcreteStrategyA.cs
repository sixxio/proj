using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSort
{
    public class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(double[,] mtr, bool desc)
        {
            //Console.WriteLine(((double[,])IStrategy.SortingAlgorithm(mtr, FindSum, desc)).Length);
            return IStrategy.SortingAlgorithm(mtr, FindSum, desc);
        }

        public static double FindSum(double[,] arr, int index)
        {
            double sum = 0;

            for (int i = 0; i < arr.GetLength(1); i++)
                if (arr[index, i] > sum)
                    sum = arr[index, i];

            return sum;
        }

    }
}
