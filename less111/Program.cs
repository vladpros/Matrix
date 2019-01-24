using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace less111
{
    class Program
    {

        private static int[,] A = new int[10, 6];
        private static int[,] B = new int[6, 10];
        private static int[,] C = new int[10, 10];
        private static object _locker = new object();


        static void Main(string[] args)
        {
            GenMatr();
            OutMatr(A);
            OutMatr(B);

            var pts1 = new ParameterizedThreadStart(ThreadMethod);
            var p = new Per();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                        p.i = i;
                        p.j = j;
                        Thread thread = new Thread(pts1);
                        Thread.Sleep(200);
                        thread.Start(p);
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine();
            OutMatr(C);
            Console.ReadKey();
        }

        private static void ThreadMethod(object obj)
        {
            var obj1 = (Per)obj;
            int k = obj1.i;
            int l = obj1.j;

            int sum = 0;
            Console.WriteLine($"Begin {k} + {l} thread");

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    sum += A[k, i] * B[j, l];
                }
            }
            C[k, l] = sum;
        }

        static void GenMatr()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    A[i, j] = i;
                    B[j, i] = j;
                }
            }
        }

        static void OutMatr(int[,] matr)
        {
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    Console.Write($"{matr[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}
