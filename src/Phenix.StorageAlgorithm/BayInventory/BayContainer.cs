namespace Phenix.StorageAlgorithm.BayInventory
{
    /// <summary>
    /// 贝上集箱
    /// </summary>
    internal class BayContainer
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public BayContainer(IInitialLocation initialLocation, int deliveryOrdinal)
        {
            _initialLocation = initialLocation;
            _deliveryOrdinal = deliveryOrdinal;
        }

        #region 属性

        private readonly IInitialLocation _initialLocation;

        /// <summary>
        /// 初始位置
        /// </summary>
        public IInitialLocation InitialLocation
        {
            get { return _initialLocation; }
        }

        private readonly int _deliveryOrdinal;

        /// <summary>
        /// 发箱序号
        /// </summary>
        public int DeliveryOrdinal
        {
            get { return _deliveryOrdinal; }
        }

        #endregion
    }
}