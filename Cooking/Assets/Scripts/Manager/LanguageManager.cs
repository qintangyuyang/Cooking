using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using Unity.Collections;
using UnityEngine;

namespace Cooking.Manager
{
    /// <summary>
    /// 语言类型的枚举
    /// </summary>
    public enum LanguageType
    {
        Chinese,    //zh-CN
        English,    //en-US
    }
    
    /// <summary>
    /// 多语言管理器
    /// </summary>
    public class LanguageManager
    {
        private static LanguageManager _instance;

        public static LanguageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LanguageManager();
                }
                return _instance;
            }
        }
        
        private LanguageManager(){}

        //默认语言-中文
        private const LanguageType defaultLanguage = LanguageType.Chinese;
        //当前正在使用的语言
        private LanguageType currentLanguage;

        public LanguageType CurrentLanguage => currentLanguage;
        
        //装某个语言的字典
        private Dictionary<string, string> languageDic = new Dictionary<string, string>();

        //语言代码映射表
        private static readonly Dictionary<LanguageType, string> LanguageCodeMap = new Dictionary<LanguageType, string>()
            {
                { LanguageType.Chinese, "zh-CN" },
                { LanguageType.English, "en-US" },
            };
        
        private const string PreferenceFileName = "LanguagePreference"; //偏好持久化文件名

        public void Initialize()
        {
            
        }
        
        /// <summary>
        /// 加载多语言
        /// </summary>
        /// <param name="language"></param>
        private void LoadLanguage(string language)
        {
            //规定多语言的json文件都放在默认的StreamingAssets文件夹下了
            string path = Application.streamingAssetsPath + "/Config/Language/" + language + ".json";
            
            string jsonStr = "";
            //根据路径加载json字符串
            jsonStr = File.ReadAllText(path);
            //将反序列化出来的数据装入字典
            languageDic = JsonMapper.ToObject<Dictionary<string, string>>(jsonStr);
            
            //触发一下多语言改变的事件
            EventManager.TriggerEvent(EventType.LanguageChanged);
        }

        public string GetText(string key, params object[] args)
        {
            if (languageDic.TryGetValue(key, out string value))
            {
                return args.Length > 0 ? string.Format(value, args) : value;
            }

            return key;
        }

        public void SwitchLanguage(string language)
        {
            //if(language==currentLanguage) return;
            LoadLanguage(language);
        }
    }
}
