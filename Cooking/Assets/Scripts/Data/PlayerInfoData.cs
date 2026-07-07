using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Data
{
    /// <summary>
    /// 玩家数据
    /// </summary>
    public class PlayerInfoData
    {
        /// <summary>
        /// 初始金币500
        /// </summary>
        public int gold = 500;

        /// <summary>
        /// 当前季节
        /// </summary>
        public Season season = Season.Spring;

        /// <summary>
        /// 当前季节的第几天
        /// </summary>
        public int day = 1;

        /// <summary>
        /// 当前时间段
        /// </summary>
        public DayTime dayTime = DayTime.Dawn;

        /// <summary>
        /// 每日顾客接待，上限为4
        /// </summary>
        public int dailyLimit = 4;

        /// <summary>
        /// 当日已接待的数量
        /// </summary>
        public int dailyCurrentNum = 0;

        /// <summary>
        /// 是否手动提前结束当天营业
        /// </summary>
        public bool isDayEnd = false;

        /// <summary>
        /// 累计金币总收入
        /// </summary>
        public int totalGoldNum = 0;

        /// <summary>
        /// 累计完成订单数量
        /// </summary>
        public int totalCompleted = 0;
    }
}
