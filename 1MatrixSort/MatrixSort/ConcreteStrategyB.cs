using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSort
{
    public class ConcreteStrategyB : IStrategy
    {

        public object DoAlgorithm(double[,] mtr, bool desc)
        {
            return IStrategy.SortingAlgorithm(mtr, FindMaxElem, desc);
        }

        public double FindMaxElem(double[,] arr, int index)
        {
            double max = arr[index, 0];

            for (int i = 0; i < arr.GetLength(1); i++)
                if (arr[index, i] > max)
                    max = arr[index, i];
         
            return max;
        }
    }
}
