using System;
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
            LanguageType savedLang = LoadPreference();
            LoadLanguage(savedLang);
        }
        
        /// <summary>
        /// 加载多语言
        /// </summary>
        /// <param name="language"></param>
        private void LoadLanguage(LanguageType language)
        {

            if (!LanguageCodeMap.TryGetValue(language, out string languageCode))
            {
                Debug.LogError($"[LanguageManager] 不支持的语言类型: {language}");
                return;
            }
            
            //规定多语言的json文件都放在默认的StreamingAssets文件夹下了
            string path = Application.streamingAssetsPath + "/Config/Language/" + languageCode + ".json";

            try
            {
                if (!File.Exists(path))//路径不存在
                {
                    Debug.LogError($"[LanguageManager] 语言文件不存在: {path}");
                    if (language != defaultLanguage)
                    {
                        LoadLanguage(defaultLanguage);//加载默认语言
                    }
                    return;
                }
                string jsonString = File.ReadAllText(path);
                if (string.IsNullOrEmpty(jsonString))
                {
                    Debug.LogWarning($"[LanguageManager] 语言文件为空: {path}");
                    return;
                }

                var newDic = JsonMapper.ToObject<Dictionary<string, string>>(jsonString);
                if (newDic != null)
                {
                    languageDic = newDic;
                    currentLanguage = language;
                    EventManager.TriggerEvent(EventType.LanguageChanged);//触发多语言切换事件
                }

            }
            catch (Exception e)
            {
                Debug.LogError($"[LanguageManager] 加载语言失败: {path}\n{e}");
                if (language != defaultLanguage)
                {
                    LoadLanguage(defaultLanguage);//加载默认语言
                }
            }
        }

        public string GetText(string key, params object[] args)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("[LanguageManager] GetText key 为空");
                return key;
            }
            if (languageDic.TryGetValue(key, out string value))
            {
                return args.Length > 0 ? string.Format(value, args) : value;
            }

            return key;
        }

        public void SwitchLanguage(LanguageType language)
        {
            if(language==currentLanguage) return;
            LoadLanguage(language);

            if (currentLanguage == language)
            {
                //保存在偏好文件夹里
                SavePreference(language);
            }
        }

        //语言偏好保存本地实现持久化
        private void SavePreference(LanguageType language)
        {
            var data = new Dictionary<string, string>()
            {
                { "language", LanguageCodeMap[language] },
            };
            JsonManager.Instance.SaveData(data, PreferenceFileName);
        }

        private LanguageType LoadPreference()
        {
            try
            {
                var data = JsonManager.Instance.LoadData<Dictionary<string, string>>(PreferenceFileName);

                if (data != null && data.TryGetValue("language", out string langCode))
                {
                    foreach (var kvp in LanguageCodeMap)
                    {
                        if (kvp.Value == langCode)
                        {
                            return kvp.Key;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[LanguageManager] 偏好加载失败，使用默认语言: {e.Message}");
            }

            return defaultLanguage;
        } 
    }
}
