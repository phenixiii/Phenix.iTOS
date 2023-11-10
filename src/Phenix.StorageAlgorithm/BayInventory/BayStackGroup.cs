using System;
using System.Collections.Generic;

namespace Phenix.StorageAlgorithm.BayInventory
{
    /// <summary>
    /// 栈组
    /// </summary>
    internal class BayStackGroup
    {
        private BayStackGroup(BayStackGroup belowGroup)
        {
            _belowGroup = belowGroup;
        }

        #region 工厂

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="containerDict">层-箱</param>
        /// <returns>栈组</returns>
        public static BayStackGroup Fetch(IDictionary<int, BayContainer> containerDict)
        {
            BayStackGroup result = new BayStackGroup(null);
            for (int i = 1; i <= containerDict.Count - 1; i++)
            {
                if (!containerDict.TryGetValue(i, out BayContainer bayContainer))
                    throw new InvalidOperationException("第" + i + "层不应该悬空!");

                result = result.Load(bayContainer);
            }

            return result;
        }

        #endregion

        #region 属性

        private readonly BayStackGroup _belowGroup;

        /// <summary>
        /// 底下栈组
        /// </summary>
        public BayStackGroup BelowGroup
        {
            get { return _belowGroup; }
        }

        private bool? _descending;

        /// <summary>
        /// 从下至上降序排列
        /// </summary>
        public bool? Descending
        {
            get { return _descending; }
        }

        private readonly Stack<BayContainer> _containerList = new Stack<BayContainer>(); //从下至上

        /// <summary>
        /// 箱量
        /// </summary>
        public int ContainerCount
        {
            get { return _containerList.Count; }
        }

        private int? _groupCount;

        /// <summary>
        /// 栈组数量
        /// </summary>
        public int GroupCount
        {
            get
            {
                if (!_groupCount.HasValue)
                    _groupCount = _belowGroup != null ? _belowGroup.GroupCount + 1 : 1;
                return _groupCount.Value;
            }
        }

        private int? _entropy;

        /// <summary>
        /// 熵值
        /// </summary>
        public int Entropy
        {
            get
            {
                if (!_entropy.HasValue)
                {
                    int belowEntropy = _belowGroup != null ? _belowGroup.Entropy : 0;
                    _entropy = Descending.HasValue && !Descending.Value || belowEntropy > 0 ? ContainerCount * GroupCount + belowEntropy * GroupCount : 0;
                }

                return _entropy.Value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 装载集箱
        /// </summary>
        /// <param name="container">集箱</param>
        /// <returns>栈组</returns>
        public BayStackGroup Load(BayContainer container)
        {
            bool? descending = _containerList.Count > 0 ? _containerList.Peek().DeliveryOrdinal >= container.DeliveryOrdinal : null;
            if (descending.HasValue && _descending.HasValue && descending.Value != _descending.Value)
            {
                BayStackGroup result = new BayStackGroup(this);
                return result.Load(container);
            }

            _descending = descending;
            _containerList.Push(container);
            return this;
        }

        /// <summary>
        /// 卸载集箱
        /// </summary>
        /// <param name="container">集箱</param>
        /// <returns>层高</returns>
        public BayStackGroup Unload(out BayContainer container)
        {
            if (_containerList.Count > 0)
            {
                container = _containerList.Pop();
                return _containerList.Count > 0 || this.BelowGroup == null ? this : this.BelowGroup;
            }
            else if (this.BelowGroup != null)
                return this.BelowGroup.Unload(out container);
            else
            {
                container = null;
                return this;
            }
        }

        #endregion
    }
}