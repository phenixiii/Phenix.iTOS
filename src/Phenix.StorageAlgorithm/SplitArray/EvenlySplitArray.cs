using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;

namespace Phenix.StorageAlgorithm.SplitArray
{
    /// <summary>
    /// 均衡拆分数组
    /// </summary>
    public static class EvenlySplitArray
    {
        #region 方法

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="source">待拆分的数值数组</param>
        /// <param name="volumeLimits">N个新数组标定的可容纳极限值</param>
        /// <param name="isOverLimit">结果超限需酌情采纳</param>
        /// <returns>均衡拆分到N个新数组的数值清单</returns>
        public static IList<IList<double>> Execute(double[] source, double[] volumeLimits, out bool isOverLimit)
        {
            isOverLimit = false;

            //边界处理
            if (source == null || source.Length == 0 || volumeLimits == null || volumeLimits.Length == 0)
                return null;

            //准备工作（含边界处理）
            IList<IList<double>> result = new List<IList<double>>(volumeLimits.Length);
            if (volumeLimits.Length == 1)
            {
                result[0] = new List<double>(source);
                return result;
            }

            if (source.Length < volumeLimits.Length)
                throw new InvalidOperationException("数组里仅" + source.Length + "项数值不够拆分成" + volumeLimits.Length + "份!");

            for (int i = 0; i < volumeLimits.Length; i++)
                result.Add(new List<double>());

            source.Sort((x, y) => -x.CompareTo(y)); //从大到小
            if (source[^1] < 0)
                throw new InvalidOperationException("数组里不允许出现小于0的数值!");

            double[] volumes = new double[volumeLimits.Length];
            Array.Copy(volumeLimits, volumes, volumeLimits.Length);

            //第一步：粗拆
            foreach (double value in source)
            {
                double maxVolumeLimit = Double.MinValue; //最大余量
                int maxVolumeLimitIndex = 0; //最大余量新数组的索引
                for (int i = 0; i < volumes.Length; i++) //寻找最大余量的新数组
                    if (maxVolumeLimit < volumes[i])
                    {
                        maxVolumeLimit = volumes[i];
                        maxVolumeLimitIndex = i;
                    }

                result[maxVolumeLimitIndex].Add(value); //拆分到最大余量的新数组
                volumes[maxVolumeLimitIndex] = maxVolumeLimit - value; //更新新数组的余量
            }

            //第二步：细调
            for (int iteration = 0; iteration < source.Length; iteration++) //最大迭代次数以数值数量为限
            {
                double minVolumeLimit = Double.MaxValue; //最小余量
                double maxVolumeLimit = Double.MinValue; //最大余量
                int minVolumeLimitIndex = 0; //最小余量新数组的索引
                int maxVolumeLimitIndex = 0; //最大余量新数组的索引
                for (int i = 0; i < volumes.Length; i++) //寻找两极余量的新数组
                {
                    if (minVolumeLimit > volumes[i])
                    {
                        minVolumeLimit = volumes[i];
                        minVolumeLimitIndex = i;
                    }

                    if (maxVolumeLimit < volumes[i])
                    {
                        maxVolumeLimit = volumes[i];
                        maxVolumeLimitIndex = i;
                    }
                }

                if (minVolumeLimitIndex == maxVolumeLimitIndex) //已达优化极限
                    break;

                List<double> minVolumeArray = (List<double>)result[minVolumeLimitIndex]; //最小余量新数组
                List<double> maxVolumeArray = (List<double>)result[maxVolumeLimitIndex]; //最大余量新数组
                maxVolumeArray.Sort((x, y) => x.CompareTo(y)); //从小到大
                int minVolumeArrayIndex = -1; //最小余量新数组索引
                int maxVolumeArrayIndex = -1; //最大余量新数组索引
                double middleDiff = (minVolumeLimit - maxVolumeLimit) / 2; //余量差额中间值
                double minDiff = Double.MaxValue; //最小差额
                bool find = false;
                for (int i1 = 0; i1 < minVolumeArray.Count; i1++) //遍历最小余量新数组
                for (int i2 = 0; i2 < maxVolumeArray.Count; i2++) //遍历最大余量新数组（效率还可优化）
                {
                    double diff = Math.Abs(middleDiff + minVolumeArray[i1] - maxVolumeArray[i2]);
                    if (minDiff > diff) //发现更小差额
                    {
                        minDiff = diff;
                        minVolumeArrayIndex = i1;
                        maxVolumeArrayIndex = i2;
                        find = true;
                    }
                    else if (find) //无遍历下去的意义
                        break;
                }

                if (minVolumeArrayIndex == -1) //已达优化极限
                    break;

                double minVolumeValue = minVolumeArray[minVolumeArrayIndex];
                double maxVolumeValue = maxVolumeArray[maxVolumeArrayIndex];
                minVolumeArray[minVolumeArrayIndex] = maxVolumeValue; //交换数值
                maxVolumeArray[maxVolumeArrayIndex] = minVolumeValue; //交换数值
                volumes[minVolumeLimitIndex] = volumes[minVolumeLimitIndex] + minVolumeValue - maxVolumeValue; //更新余量
                volumes[maxVolumeLimitIndex] = volumes[maxVolumeLimitIndex] + maxVolumeValue - minVolumeValue; //更新余量
            }

            isOverLimit = volumes.Min() < 0;
            return result;
        }

        #endregion
    }
}