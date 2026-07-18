using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Model
{
    /// <summary>
    /// 游戏设置
    /// </summary>
    public class SettingData
    {
        /// <summary>
        /// 主音量
        /// </summary>
        public float masterVolume = 1f;
        
        /// <summary>
        /// 音效音量
        /// </summary>
        public float sfxVolume = 1f;

        /// <summary>
        /// 窗口分辨率宽
        /// </summary>
        public int resolutionWidth = 1920;
        
        /// <summary>
        /// 窗口分辨率高
        /// </summary>
        public int resolutionHeight = 1080;
        
        /// <summary>
        /// 语言
        /// </summary>
        public LanguageType language = LanguageType.Chinese;
    }
}
