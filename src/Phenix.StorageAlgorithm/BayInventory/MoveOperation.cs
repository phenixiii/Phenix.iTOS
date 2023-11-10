namespace Phenix.StorageAlgorithm.BayInventory
{
    /// <summary>
    /// 移动操作
    /// </summary>
    public class MoveOperation
    {
        internal MoveOperation(int originRow, int destRow, IInitialLocation initialLocation, int deliveryOrdinal)
        {
            _originRow = originRow;
            _destRow = destRow;
            _initialLocation = initialLocation;
            _deliveryOrdinal = deliveryOrdinal;
        }

        #region 属性

        private readonly int _originRow;

        /// <summary>
        /// 起点排
        /// </summary>
        public int OriginRow
        {
            get { return _originRow; }
        }

        private readonly int _destRow;

        /// <summary>
        /// 讫点排
        /// </summary>
        public int DestRow
        {
            get { return _destRow; }
        }

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