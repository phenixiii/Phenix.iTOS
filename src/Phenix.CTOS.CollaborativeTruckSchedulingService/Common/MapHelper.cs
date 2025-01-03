namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Common;

/// <summary>
/// 通过地图上的两个坐标计算距离
/// </summary>
public class MapHelper
{
    private const double EARTH_RADIUS = 6378137;

    /// <summary>
    /// 计算两点位置的距离
    /// 该公式为GOOGLE提供
    /// 误差小于0.2米
    /// </summary>
    /// <param name="lat1">第一点纬度</param>
    /// <param name="lng1">第一点经度</param>
    /// <param name="lat2">第二点纬度</param>
    /// <param name="lng2">第二点经度</param>
    /// <returns>返回两点的距离（米）</returns>
    public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
    {
        double radLat1 = Rad(lat1);
        double radLng1 = Rad(lng1);
        double radLat2 = Rad(lat2);
        double radLng2 = Rad(lng2);
        double a = radLat1 - radLat2;
        double b = radLng1 - radLng2;
        return 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
    }

    /// <summary>
    /// 经纬度转化成弧度
    /// </summary>
    /// <param name="value">经纬度</param>
    /// <returns>弧度</returns>
    private static double Rad(double value)
    {
        return value * Math.PI / 180d;
    }
}