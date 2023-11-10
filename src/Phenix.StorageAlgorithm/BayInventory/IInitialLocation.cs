namespace Phenix.StorageAlgorithm.BayInventory
{
    /// <summary>
    /// 初始位置
    /// </summary>
    public interface IInitialLocation
    {
        #region 属性

        /// <summary>
        /// 箱号
        /// </summary>
        string ContainerNo { get; }

        /// <summary>
        /// 排
        /// </summary>
        int Row { get; }

        /// <summary>
        /// 层
        /// </summary>
        int Tier { get; }

        #endregion
    }
}