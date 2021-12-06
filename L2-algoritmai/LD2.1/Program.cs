using System;
using System.CodeDom;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace LD2_0
{
    internal static class Program
    {
        private static int[] x;
        
        private static int[] y;

        private static int[,] cache; //temp
        
        private static int[,] dcache; //temp

        private static int count = 0;

        public static void Main(string[] args)
        {
            Random rand = new Random();
            int till = 5;
            int size = 20;
            x = GetArray(till, size);
            y = GetArray(till, size);

            Console.WriteLine("Metodas|Reiksme|    Laikas      |  Veiksmai | m : n");
            
            Stopwatch st = Stopwatch.StartNew();
            for (int i = 0; i < x.Length; i++)
            {
                int xRand = x[i];
                int yRand = y[i];
                F1(xRand, yRand);
            }
            Console.WriteLine($"  {st.Elapsed};    {count};");
            count = 0;
            st.Stop();
            st.Restart();
            Stopwatch at = Stopwatch.StartNew();
            for (int i = 0; i < x.Length; i++)
            {
                int xRand = x[i];
                int yRand = y[i];
                cache = InitializeCache(xRand + 1, yRand + 1, -1);
                dcache = InitializeCache(xRand + 1, yRand + 1, -1);
                F2(xRand, yRand);
            }
            at.Stop();
            Console.WriteLine($"  {st.Elapsed};    {count};");
            st.Restart();
            Console.WriteLine("------BAIGTA-------");
        }
        static int F1(int m, int n)
        {
            count++;
            if (n == 0) return m;
            
            if (m == 0 && n > 0) return n;
            
            else return Math.Min(1 + F1(m-1,n), Math.Min(1 + F1(m,n-1), D1(m, n) + F1(m-1,n-1)));
        }
        static int D1(int i, int j)
        {
            if (i == j) return 1;
            else return 0;
        }
        static int F2(int m, int n)
        {
            for (int mm = 0; mm <= m; mm++)
            for (int nn = 0; nn <= n; nn++)
            {
                count++;
                if (nn== 0) cache[mm, nn] = m;

                else if (mm == 0 && nn > 0) cache[mm, nn] = n;

                else cache[mm, nn] = Math.Min(1 + cache[mm - 1, nn], Math.Min(1 + cache[mm, nn - 1], D2(mm, nn) + cache[mm - 1, nn- 1]));
            }
            return cache[m, n];
        }
        static int D2(int i, int j)
        {
            for (int ii = 0; ii <= i; ii++)
            for (int jj = 0; jj <= j; jj++)
            {
                if (ii == jj) dcache[ii, jj] = 1;
                else dcache[ii, jj] = 0;
            }
            return dcache[i, j];
        }
        static int[,] InitializeCache(int rows, int columns, int fillValue)
        {
            var arr = new int[rows, columns];

            for (int j = 0; j < arr.GetLength(0); j++)
            for (int k = 0; k < arr.GetLength(1); k++)
                arr[j, k] = fillValue;

            return arr;
        }
        private static int[] GetArray(int length, int till)
        {
            var arr = new int[length];
            Random rand = new Random();
            for (int i = 0; i < length; i++)
                arr[i] = rand.Next(till);;
            return arr;
        }
    }
}