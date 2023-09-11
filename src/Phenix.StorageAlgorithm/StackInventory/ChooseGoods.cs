using System;
using System.Collections.Generic;

namespace Phenix.StorageAlgorithm.StackInventory
{
    /// <summary>
    /// 挑货
    /// </summary>
    public static class ChooseGoods
    {
        #region 属性

        private const int ArrayMaxCount = 10000;

        #endregion

        #region 方法

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="source">整排全部货物清单</param>
        /// <param name="matchCondition">匹配条件</param>
        /// <param name="owner">货主</param>
        /// <param name="transferTarget">过户对象(出库时为null)</param>
        /// <param name="minWeight">最小重量</param>
        /// <param name="maxWeight">最大重量</param>
        /// <param name="aimForMaxWight">"足发"(false)要求尽量靠近区间的最小值、"不超发"(true)要求尽量靠近区间的最大值</param>
        /// <exception cref="ArgumentNullException">source不允许为空</exception>
        /// <exception cref="ArgumentOutOfRangeException">minWight不允许小于0或大于maxWight</exception>
        /// <returns>挑出货的集合(挑不出时为null)</returns>
        public static IList<IGoods> Execute(IList<IGoods> source,
            Func<IGoods, bool> matchCondition, string owner, string transferTarget,
            int minWeight, int maxWeight, bool aimForMaxWight)
        {
            if (source == null)
                return null;
            if (source.Count == 0)
                return new List<IGoods>();

            if (minWeight < 0 || minWeight > maxWeight)
                throw new ArgumentOutOfRangeException(nameof(minWeight));

            IList<GroupGoods> groupGoodsList = GroupGoods.Fetch(source, matchCondition, owner, transferTarget); //获取Row(小到大)-Value(大到小)-GroupGoods序列

            int zeroCount = Int32.MaxValue;
            int minGoodsWeight = Int32.MaxValue;
            int toolWeight = 0;
            foreach (GroupGoods item in groupGoodsList)
            {
                if (zeroCount > 0)
                {
                    int z = 0;
                    int w = item.Weight;
                    while (w > 0 && w % 10 == 0)
                    {
                        w = w / 10;
                        z = z + 1;
                    }

                    if (zeroCount > z)
                        zeroCount = z;
                }

                if (minGoodsWeight > item.Weight)
                    minGoodsWeight = item.Weight;
                toolWeight = toolWeight + item.Weight;
            }

            if (toolWeight < minWeight)
                return null;

            if (minWeight < minGoodsWeight)
                minWeight = minGoodsWeight;

            int precision = (int)Math.Pow(10, zeroCount);
            int maxWeightP = maxWeight / precision;
            int minWeightP = minWeight / precision;
            int minGoodsWeightP = minGoodsWeight / precision;
            int valueMatrixCount = maxWeightP % ArrayMaxCount > 0 ? maxWeightP / ArrayMaxCount + 1 : maxWeightP / ArrayMaxCount;
            List<double[]> valueMatrixL = new List<double[]>(valueMatrixCount); //价值矩阵L侧
            for (int i = 1; i <= valueMatrixCount; i++)
                valueMatrixL.Add(i == valueMatrixCount ? new double[maxWeightP % ArrayMaxCount + 1] : new double[ArrayMaxCount]);
            List<double[]> valueMatrixR = new List<double[]>(valueMatrixCount); //价值矩阵R侧
            for (int i = 1; i <= valueMatrixCount; i++)
                valueMatrixR.Add(i == valueMatrixCount ? new double[maxWeightP % ArrayMaxCount + 1] : new double[ArrayMaxCount]);
            Dictionary<int, SortedSet<int>> putinWeightDict = new Dictionary<int, SortedSet<int>>(groupGoodsList.Count);
            for (int i = 0; i < groupGoodsList.Count; i++)
            {
                GroupGoods groupGoods = groupGoodsList[i];
                SortedSet<int> putinWeightList = new SortedSet<int>();
                int goodsWeightP = groupGoods.Weight / precision;
                for (int w = minGoodsWeightP; w <= maxWeightP; w++)
                {
                    int w1 = w / ArrayMaxCount;
                    int w2 = w % ArrayMaxCount;
                    int marginWeightP = w - goodsWeightP; //承重w的背包放入GroupGoods后的承重余量是marginWeight
                    if (marginWeightP >= 0) //承重w的背包放得下GroupGoods
                    {
                        double value = valueMatrixL[marginWeightP / ArrayMaxCount][marginWeightP % ArrayMaxCount] + groupGoods.Value; //与前轮（第0...i-1件）规划得到的承重MarginWeight的背包合拼
                        if (value >= valueMatrixL[w1][w2]) //放入GroupGoods的Value相等或更高
                        {
                            valueMatrixR[w1][w2] = value;
                            putinWeightList.Add(w);
                            continue;
                        }
                    }

                    valueMatrixR[w1][w2] = valueMatrixL[w1][w2];
                }

                (valueMatrixL, valueMatrixR) = (valueMatrixR, valueMatrixL);
                if (putinWeightList.Count > 0)
                    putinWeightDict.Add(i, putinWeightList);
            }

            List<IGoods> result = new List<IGoods>();
            if (aimForMaxWight) //"不超发"(true)要求尽量靠近区间的最大值
            {
                for (int ii = groupGoodsList.Count - 1; ii >= 0; ii--)
                    if (putinWeightDict.TryGetValue(ii, out SortedSet<int> pii))
                    {
                        for (int w = maxWeightP; w >= minWeightP; w--)
                            if (pii.Contains(w))
                            {
                                result.Clear();
                                GroupGoods groupGoods = groupGoodsList[ii];
                                result.AddRange(groupGoods.GoodsList);
                                int putinRow = groupGoods.Row;
                                int canPackWeightP = w - groupGoods.Weight / precision;
                                for (int i = ii - 1; i >= 0; i--)
                                    if (putinWeightDict.TryGetValue(i, out SortedSet<int> pi) && pi.Contains(canPackWeightP))
                                    {
                                        groupGoods = groupGoodsList[i];
                                        if (putinRow != groupGoods.Row) //不考虑已放入过GroupGoods的Row
                                        {
                                            result.AddRange(groupGoods.GoodsList);
                                            putinRow = groupGoods.Row;
                                        }

                                        canPackWeightP = canPackWeightP - groupGoods.Weight / precision;
                                        if (canPackWeightP < minGoodsWeightP)
                                            break;
                                    }

                                if (w - canPackWeightP >= minWeightP)
                                    return result;
                            }
                    }
            }
            else //"足发"(false)要求尽量靠近区间的最小值
            {
                for (int ii = groupGoodsList.Count - 1; ii >= 0; ii--)
                    if (putinWeightDict.TryGetValue(ii, out SortedSet<int> pii))
                    {
                        for (int w = minWeightP; w <= maxWeightP; w++)
                            if (pii.Contains(w))
                            {
                                result.Clear();
                                GroupGoods groupGoods = groupGoodsList[ii];
                                result.AddRange(groupGoods.GoodsList);
                                int putinRow = groupGoods.Row;
                                int canPackWeightP = w - groupGoods.Weight / precision;
                                for (int i = ii - 1; i >= 0; i--)
                                    if (putinWeightDict.TryGetValue(i, out SortedSet<int> pi) && pi.Contains(canPackWeightP))
                                    {
                                        groupGoods = groupGoodsList[i];
                                        if (putinRow != groupGoods.Row) //不考虑已放入过GroupGoods的Row
                                        {
                                            result.AddRange(groupGoods.GoodsList);
                                            putinRow = groupGoods.Row;
                                        }

                                        canPackWeightP = canPackWeightP - groupGoods.Weight / precision;
                                        if (canPackWeightP < minGoodsWeightP)
                                            break;
                                    }

                                if (w - canPackWeightP >= minWeightP)
                                    return result;
                            }
                    }
            }

            return null;
        }

        #endregion
    }
}