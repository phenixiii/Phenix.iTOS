using System;
using System.Collections.Generic;

namespace Phenix.StorageAlgorithm.BayInventory
{
    /// <summary>
    /// 装船前预翻箱
    /// </summary>
    public static class PreShipmentBlockRelocation
    {
        #region 属性

        private const int ArrayMaxCount = 10000;

        #endregion

        #region 方法

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="limitRow">排长极限</param>
        /// <param name="limitTier">层高极限</param>
        /// <param name="initialBay">初始贝图</param>
        /// <param name="deliveryOrders">发箱顺序(箱号-序号)清单</param>
        /// <exception cref="ArgumentOutOfRangeException">maxRow和maxTier不允许小于等于1</exception>
        /// <returns>翻箱操作序列</returns>
        public static IList<MoveOperation> Execute(int limitRow, int limitTier,
            IList<IInitialLocation> initialBay, params IDictionary<string, int>[] deliveryOrders)
        {
            if (initialBay == null)
                return null;
            if (initialBay.Count <= 1)
                return new List<MoveOperation>();

            if (deliveryOrders == null)
                return null;
            if (deliveryOrders.Length == 0)
                return new List<MoveOperation>();

            if (limitRow <= 1)
                throw new ArgumentOutOfRangeException(nameof(limitRow), limitRow, "limitRow <= 1");
            if (limitTier <= 1)
                throw new ArgumentOutOfRangeException(nameof(limitTier), limitTier, "limitTier <= 1");

            IDictionary<int, BayStack> stackDict = BayStack.Fetch(limitRow, limitTier, initialBay, deliveryOrders);


            return null;
        }

        #endregion
    }
}