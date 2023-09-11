using System;
using System.Collections.Generic;
using System.Linq;

namespace Phenix.StorageAlgorithm.StackInventory
{
    /// <summary>
    /// 分组货物
    /// </summary>
    internal class GroupGoods
    {
        private GroupGoods(int row, double value, int weight, IList<IGoods> goodsList)
        {
            _row = row;
            _value = value;
            _weight = weight;
            _goodsList = goodsList;
        }

        #region 工厂

        internal static IList<GroupGoods> Fetch(IList<IGoods> source, Func<IGoods, bool> matchCondition, string owner, string transferTarget = null)
        {
            //整理成Row(小到大)-Layer(内到外)-Goods有序结构
            SortedDictionary<int, SortedDictionary<int, List<IGoods>>> rowGoodsDict = new SortedDictionary<int, SortedDictionary<int, List<IGoods>>>();
            foreach (IGoods item in source)
            {
                if (!rowGoodsDict.TryGetValue(item.Row, out SortedDictionary<int, List<IGoods>> layerGoodsDict))
                {
                    layerGoodsDict = new SortedDictionary<int, List<IGoods>>();
                    rowGoodsDict.Add(item.Row, layerGoodsDict);
                }

                if (!layerGoodsDict.TryGetValue(item.Layer, out List<IGoods> layerGoodsList))
                {
                    layerGoodsList = new List<IGoods>();
                    layerGoodsDict.Add(item.Layer, layerGoodsList);
                }

                layerGoodsList.Add(item);
            }

            //取堆放货物最多排上的货物数量
            int rowGoodsMaxCount = 0;
            foreach (KeyValuePair<int, SortedDictionary<int, List<IGoods>>> kvp1 in rowGoodsDict)
            {
                int count = 0;
                foreach (KeyValuePair<int, List<IGoods>> kvp2 in kvp1.Value)
                foreach (IGoods item in kvp2.Value)
                    count = count + 1;
                if (count > rowGoodsMaxCount)
                    rowGoodsMaxCount = count;
            }

            const double normalizeValue = 1.0; //归一化值
            double unitValue = normalizeValue / rowGoodsMaxCount; //价值单位

            //整理成Row(小到大)-Value(大到小)-GroupGoods序列
            List<GroupGoods> result = new List<GroupGoods>();
            foreach (KeyValuePair<int, SortedDictionary<int, List<IGoods>>> kvp1 in rowGoodsDict)
            {
                double value = normalizeValue;
                double transferValue = 0;
                Stack<IGoods> rowStack = new Stack<IGoods>();
                foreach (KeyValuePair<int, List<IGoods>> kvp2 in kvp1.Value)
                foreach (IGoods item in kvp2.Value)
                {
                    value = value - unitValue; //价值初始化为: 空位数*单位价值
                    if (transferTarget != null)
                        if (item.Owner == transferTarget) //货物货主是过户对象
                            transferValue = transferValue + unitValue;
                        else if (item.Owner == owner) //货物货主是过户货主
                            transferValue = transferValue - unitValue;
                    rowStack.Push(item); //Row上按Layer从内到外压入栈
                }

                //Row上按Layer从外到内分组货物
                int weight = 0;
                List<IGoods> goodsList = new List<IGoods>();
                List<GroupGoods> groupGoodsList = new List<GroupGoods>();
                while (rowStack.TryPop(out IGoods goods))
                    if (goods.Owner == owner && (matchCondition == null || matchCondition(goods))) //符合候选条件
                    {
                        value = value + unitValue;
                        weight = weight + goods.Weight;
                        goodsList.Add(goods);

                        if (transferTarget != null && goods.Owner == owner) //货物货主是过户货主
                            transferValue = transferValue + unitValue;

                        groupGoodsList.Add(new GroupGoods(kvp1.Key, value + transferValue, weight, goodsList.ToArray()));
                    }
                    else if (transferTarget == null || goods.Owner != transferTarget) //出库或货物货主不是过户对象
                        value = value - unitValue;

                //Row上按Value从大到小获取GroupGoods
                result.AddRange(groupGoodsList.OrderByDescending(p => p.Value));
            }

            return result.AsReadOnly();
        }

        #endregion

        #region 属性

        private readonly int _row;

        /// <summary>
        /// 排
        /// </summary>
        public int Row
        {
            get { return _row; }
        }

        private readonly double _value;

        /// <summary>
        /// 价值
        /// </summary>
        public double Value
        {
            get { return _value; }
        }

        private readonly int _weight;

        /// <summary>
        /// 重量
        /// </summary>
        public int Weight
        {
            get { return _weight; }
        }

        private readonly IList<IGoods> _goodsList;

        /// <summary>
        /// 货物清单
        /// </summary>
        public IList<IGoods> GoodsList
        {
            get { return _goodsList; }
        }

        #endregion
    }
}