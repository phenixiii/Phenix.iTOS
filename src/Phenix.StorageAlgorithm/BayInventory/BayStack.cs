using System;
using System.Collections.Generic;

namespace Phenix.StorageAlgorithm.BayInventory
{
    /// <summary>
    /// 栈
    /// </summary>
    internal class BayStack
    {
        private BayStack(BayStackGroup group)
        {
            _group = group;
        }

        #region 工厂

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="limitRow">排长极限</param>
        /// <param name="limitTier">层高极限</param>
        /// <param name="initialBay">初始贝图</param>
        /// <param name="deliveryOrders">发箱顺序(箱号-序号)清单</param>
        /// <returns>栈</returns>
        public static IDictionary<int, BayStack> Fetch(int limitRow, int limitTier,
            IList<IInitialLocation> initialBay, params IDictionary<string, int>[] deliveryOrders)
        {
            Dictionary<int, BayStack> result = null;
            int minEntropy = Int32.MaxValue;
            foreach (IDictionary<string, int> deliveryOrder in deliveryOrders)
            {
                Dictionary<int, BayStack> bayStackDict = new Dictionary<int, BayStack>(limitRow);
                int entropy = 0;
                for (int i = 1; i <= limitRow; i++)
                {
                    SortedDictionary<int, BayContainer> containerDict = new SortedDictionary<int, BayContainer>();
                    foreach (IInitialLocation initialLocation in initialBay)
                        if (initialLocation.Row == i)
                        {
                            if (initialLocation.Tier < 1)
                                throw new ArgumentOutOfRangeException(nameof(initialLocation.Tier), initialLocation.Tier, "tier < 1");
                            if (initialLocation.Tier > limitTier)
                                throw new ArgumentOutOfRangeException(nameof(initialLocation.Tier), initialLocation.Tier, "tier > " + limitTier);

                            BayContainer container = new BayContainer(initialLocation, deliveryOrder.TryGetValue(initialLocation.ContainerNo, out int deliveryOrdinal) ? deliveryOrdinal : Int32.MaxValue);
                            if (containerDict.TryGetValue(initialLocation.Tier, out BayContainer value))
                            {
                                if (value.InitialLocation.ContainerNo != container.InitialLocation.ContainerNo)
                                    throw new InvalidOperationException(initialLocation.Tier + "层已有" + value.InitialLocation.ContainerNo + "不能再添加" + container.InitialLocation.ContainerNo + "!");
                            }
                            else
                                containerDict.Add(initialLocation.Tier, container);
                        }

                    BayStackGroup bayStackGroup = BayStackGroup.Fetch(containerDict);
                    bayStackDict.Add(i, new BayStack(bayStackGroup));
                    entropy = entropy + bayStackGroup.Entropy;
                }

                if (minEntropy > entropy)
                {
                    result = bayStackDict;
                    minEntropy = entropy;

                    if (minEntropy == 0)
                        break;
                }
            }

            return result != null ? result.AsReadOnly() : null;
        }

        #endregion

        #region 属性

        private BayStackGroup _group;

        /// <summary>
        /// 栈组
        /// </summary>
        public BayStackGroup Group
        {
            get { return _group; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 装载集箱
        /// </summary>
        /// <param name="container">集箱</param>
        public void Load(BayContainer container)
        {
            _group = _group.Load(container);
        }

        /// <summary>
        /// 卸载集箱
        /// </summary>
        /// <returns>集箱</returns>
        public BayContainer Unload()
        {
            _group = _group.Unload(out BayContainer result);
            return result;
        }

        #endregion
    }
}