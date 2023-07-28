namespace StorageAlgorithm.StackStorageLocation
{
    /// <summary>
    /// 货物接口
    /// </summary>
    public interface IGoods
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        long Id { get; }

        /// <summary>
        /// 货主
        /// </summary>
        string Owner { get; }

        /// <summary>
        /// 重量
        /// </summary>
        int Weight { get; }

        /// <summary>
        /// 排
        /// </summary>
        int Row { get; }

        /// <summary>
        /// 层（小数在底层）
        /// </summary>
        int Layer { get; }

        #endregion
    }
}
