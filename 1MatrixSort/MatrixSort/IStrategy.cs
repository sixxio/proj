using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSort
{
    public interface IStrategy
    {
        public static void Swap(ref double[,] arr, int index)
        {
            double tempValue;

            for (int i = 0; i < arr.GetLength(1); i++)
            {
                tempValue = arr[index, i];
                arr[index, i] = arr[index + 1, i];
                arr[index + 1, i] = tempValue;
            }
        }

        public object DoAlgorithm(double[,] mtr, bool desc);

        public static object SortingAlgorithm(double[,] mtr, Order ord, bool desc)
        {

            var len = mtr.GetLength(0);

            for (var i = 1; i < len; i++)
                for (var j = 0; j < len - i; j++)
                    if ((ord(mtr, j) > ord(mtr, j + 1) && desc) || ord(mtr, j) < ord(mtr, j + 1) && !desc)
                        Swap(ref mtr, j);

            return mtr;
        }
    }
}
