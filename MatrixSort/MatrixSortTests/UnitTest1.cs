using NUnit.Framework;
using MatrixSort;

namespace MatrixSortTests
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 }, { 1, 1, 1 } });
            Matrix m2 = new Matrix(new double[,] { { 13, 2, 2 }, { 3, 4, 5 }, { 1, 2, 3 }, { 1, 1, 1 } });

            ConcreteStrategyA strat = new ConcreteStrategyA();
            m1.SetStrategy(strat);
            m1.SetOrder(false);
            m1.Sort();

            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                    Assert.AreEqual(m1[i, j], m2[i, j]);
        }

        [Test]
        public void Test2()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 }, { 1, 1, 1 } });
            Matrix m3 = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 } });

            ConcreteStrategyA strat = new ConcreteStrategyA();
            m1.SetStrategy(strat);
            m1.SetOrder(true);
            m1.Sort();

            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                    Assert.AreEqual(m1[i, j], m3[i, j]);
        }

        [Test]
        public void Test3()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 }, { 1, 1, 1 } });
            Matrix m4 = new Matrix(new double[,] { { 13, 2, 2 }, { 3, 4, 5 }, { 1, 2, 3 }, { 1, 1, 1 } });

            ConcreteStrategyB strat = new ConcreteStrategyB();
            m1.SetStrategy(strat);
            m1.SetOrder(false);
            m1.Sort();

            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                    Assert.AreEqual(m1[i, j], m4[i, j]);
        }

        [Test]
        public void Test4()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 }, { 1, 1, 1 } });
            Matrix m5 = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 } });

            ConcreteStrategyB strat = new ConcreteStrategyB();
            m1.SetStrategy(strat);
            m1.SetOrder(true);
            m1.Sort();

            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                    Assert.AreEqual(m1[i, j], m5[i, j]);
        }

        [Test]
        public void Test5()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 }, { 1, 1, 1 } });
            Matrix m6 = new Matrix(new double[,] { { 3, 4, 5 }, { 13, 2, 2 }, { 1, 2, 3 }, { 1, 1, 1 } });

            ConcreteStrategyC strat = new ConcreteStrategyC();
            m1.SetStrategy(strat);
            m1.SetOrder(false);
            m1.Sort();


            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                    Assert.AreEqual(m1[i, j], m6[i, j]);
        }

        [Test]
        public void Test6()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 13, 2, 2 }, { 1, 1, 1 } });
            Matrix m7 = new Matrix(new double[,] { { 1, 2, 3 }, { 1, 1, 1 }, { 13, 2, 2 }, { 3, 4, 5 } });

            ConcreteStrategyC strat = new ConcreteStrategyC();
            m1.SetStrategy(strat);
            m1.SetOrder(true);
            m1.Sort();

            m1.Show();
            m7.Show();


            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                    Assert.AreEqual(m1[i, j], m7[i, j]);
        }
    }
}