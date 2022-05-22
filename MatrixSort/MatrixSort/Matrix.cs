using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSort
{

    public delegate double Order(double[,] mtr, int index);

    public class Matrix
    {
        private double[,] data;
        bool desc = false;

        public Matrix(int rows, int columns)
        {
            if (rows > 0 && columns > 0)
                this.data = new double[rows, columns];
            else
                throw new Exception("Incorrect arguments");
        }

        public Matrix(double[,] userData)
        {
            this.data = userData;
        }

        public double this[int i, int j]
        {
            get => data[i, j];
            set => data[i, j] = value;
        }

        public int Rows => data.GetLength(0);

        public int Columns => data.GetLength(1);

        public int? Size => Rows == Columns ? Rows : null;

        public bool IsSquared => Rows == Columns;

        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < Rows; i++)
                    for (int j = 0; j < Columns; j++)
                        if (data[i, j] != 0)
                            return false;

                return true;
            }
        }

        public bool IsUnity
        {
            get
            {
                if (!IsSquared)
                    return false;

                for (var i = 0; i < Rows; i++)
                    for (var j = 0; j < Rows; j++)
                        if (i == j && Math.Abs(data[i, j] - 1) > 0.0000001 || i != j && data[i, j] != 0)
                            return false;

                return true;
            }
        }

        public bool IsSymmetric
        {
            get
            {
                if (!IsSquared)
                    return false;

                for (var i = 0; i < Rows; i++)
                    for (var j = 0; j <= i; j++)
                        if (Math.Abs(data[i, j] - data[j, i]) > 0.0000001)
                            return false;

                return true;
            }
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                throw new Exception("Unable to summarize matrix with different sizes");

            var res = new Matrix(m1.Rows, m1.Columns);

            for (var i = 0; i < m1.Rows; i++)
                for (var j = 0; j < m1.Columns; j++)
                    res[i, j] = m1[i, j] + m2[i, j];

            return res;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                throw new Exception("Unable to subtract matrix with different sizes");

            var res = new Matrix(m1.Rows, m1.Columns);

            for (var i = 0; i < m1.Rows; i++)
                for (var j = 0; j < m1.Columns; j++)
                    res[i, j] = m1[i, j] - m2[i, j];

            return res;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
                throw new Exception("Unable to multiply matrix with different sizes");

            var res = new Matrix(m1.Rows, m2.Columns);

            for (var i = 0; i < m1.Rows; i++)
                for (var j = 0; j < m2.Columns; j++)
                    for (var k = 0; k < m2.Rows; k++)
                        res[i, j] += m1[i, k] * m2[k, j];

            return res;
        }

        public static explicit operator Matrix(double[,] arr)
        {
            return new Matrix(arr);
        }

        public void Show() {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    Console.Write(data[i, j] + " ");

                Console.WriteLine();
            }
        }
        ////--------------------
        private IStrategy _strategy;


        public Matrix(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void SetOrder(bool desc)
        {
            this.desc = desc;
        }

        public void Sort()
        {

            if (_strategy != null)
                this.data = (double[,])this._strategy.DoAlgorithm(data, desc);

        }
    }
}
