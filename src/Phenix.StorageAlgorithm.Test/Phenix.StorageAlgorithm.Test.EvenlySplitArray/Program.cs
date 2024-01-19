using System;
using System.Collections.Generic;
using System.Linq;
using Phenix.StorageAlgorithm.SplitArray;

namespace Phenix.StorageAlgorithm.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** 演示 Phenix.StorageAlgorithm.SplitArray.EvenlySplitArray 功能 ****");
            Console.WriteLine();

            //准备
            Random random = new Random((int)DateTime.Now.Ticks);
            List<double> source = new List<double>(random.Next(80, 100));
            for (int i = 0; i < source.Capacity; i++)
                source.Add(random.Next(500000, 700000) / 10000.0);
            Console.Write("待拆分的数值数组：");
            foreach (double d in source)
                Console.Write("{0}, ", d);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("总量：{0}", source.Sum());
            Console.WriteLine();

            List<double> volumeLimits = new List<double>(random.Next(2, 5));
            int volumeLimit = (int)Math.Floor((source.Sum() + source.Max()) / volumeLimits.Capacity);
            for (int i = 0; i < volumeLimits.Capacity; i++)
                volumeLimits.Add(random.Next(volumeLimit, volumeLimit + 10));
            Console.Write("新数组可容纳极限值：");
            foreach (double d in volumeLimits)
                Console.Write("{0}, ", d);
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("请按任意键继续：");
            Console.ReadKey();

            //验证
            try
            {
                IList<IList<double>> newArrays = EvenlySplitArray.Execute(source.ToArray(), volumeLimits.ToArray(), out bool isOverLimit);
                for (int i = 0; i < newArrays.Count; i++)
                {
                    ((List<double>)newArrays[i]).Sort((x, y) => x.CompareTo(y));
                    double arrayTotal = newArrays[i].Aggregate<double, double>(0, (current, d) => current + d);
                    Console.WriteLine("第{0}组，容量{1}，拆入{2}，余量{3}", i, volumeLimits[i], arrayTotal, volumeLimits[i] - arrayTotal);
                    Console.Write("含量：");
                    foreach (double d in newArrays[i])
                        Console.Write("{0}, ", d);
                    Console.WriteLine();
                    Console.WriteLine();
                }

                if (isOverLimit)
                    Console.WriteLine("结果超限需酌情采纳!");
            }
            catch (Exception e)
            {
                Console.WriteLine("无解: {0}", e.Message);
            }

            Console.WriteLine();
            Console.Write("请按任意键结束：");
            Console.ReadKey();
        }
    }
}