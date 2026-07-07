
namespace Cooking.Data
{
    /// <summary>
    /// 季节数据——默认序列化为int（0-3）
    /// </summary>
    public enum Season
    {
        /// <summary>
        /// 春
        /// </summary>
        Spring = 0,
        /// <summary>
        /// 夏
        /// </summary>
        Summer = 1,
        /// <summary>
        /// 秋
        /// </summary>
        Autumn = 2,
        /// <summary>
        /// 冬
        /// </summary>
        Winter = 3,
    }

    /// <summary>
    /// 时间段——序列化默认为int（0-3）
    /// </summary>
    public enum DayTime
    {
        /// <summary>
        /// 清晨
        /// </summary>
        Dawn = 0,
        /// <summary>
        /// 白天
        /// </summary>
        Day = 1,
        /// <summary>
        /// 黄昏
        /// </summary>
        Dusk = 2,
        /// <summary>
        /// 夜晚
        /// </summary>
        Night = 3,
    }
}
