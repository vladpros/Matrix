﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace less111
{
    class Program
    {

        private static int[,] a = new int[10, 6];
        private static int[,] b = new int[6, 10];
        private static int[,] c = new int[10, 10];
        private static object _locker = new object();


        static void Main(string[] args)
        {
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            Console.WriteLine("4");
            GenMatr();
            OutMatr(a);
            OutMatr(b);

            var pts1 = new ParameterizedThreadStart(ThreadMethod);
            var p = new Per();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    p.i = i;
                    p.j = j;
                    Thread thread = new Thread(pts1);
                    thread.Start(p);
                }
            }

            Console.WriteLine();
            OutMatr(c);
            Console.ReadKey();
        }

        private static void ThreadMethod(object obj)
        {
            obj = (Per)obj;
            int k = obj.i;
            int l = obj.j;

            int sum=0;
            Console.WriteLine($"Begin {k} + {l} thread");
            try
            {
                Monitor.Enter(_locker);
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        sum += a[k, i] * b[j, l];
                    }
                }
                c[k, l] = sum;
            }
            finally
            {
                Monitor.Exit(_locker);
            }



        }

        static void GenMatr()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    a[i, j] = i;
                    b[j, i] = j;
                }
            }
        }

        static void OutMatr(int[,] matr)
        {
            for (int i=0;i<matr.GetLength(0);i++)
            {
                for (int j = 0; j < matr.GetLength(1);j++)
                {
                    Console.Write($"{matr[i,j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

