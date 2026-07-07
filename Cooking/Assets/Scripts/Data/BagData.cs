using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Data
{
    /// <summary>
    /// 背包数据
    /// </summary>
    public class BagData
    {
        /// <summary>
        /// 背包中食材列表
        /// </summary>
        public List<FoodData> foodDatas = new List<FoodData>();
    }

    /// <summary>
    /// 单一的食物类，比如牛肉类，土豆
    /// </summary>
    public class FoodData
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public string id;

        /// <summary>
        /// 持有数量
        /// </summary>
        public int num;
    }
    
}
