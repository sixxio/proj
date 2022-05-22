using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSort
{
    public class ConcreteStrategyC : IStrategy
    {
        public object DoAlgorithm(double[,] mtr, bool desc)
        {
            return IStrategy.SortingAlgorithm(mtr, FindMinElem, desc);
        }
        public double FindMinElem(double[,] arr, int index)
        {
            double min = arr[index, 0];

            for (int i = 0; i < arr.GetLength(1); i++)
                if (arr[index, i] < min)
                    min = arr[index, i];

            return min;
        }
    }
}
