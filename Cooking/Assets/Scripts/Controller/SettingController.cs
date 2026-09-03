using Cooking.Manager;
using Cooking.Model;
using UnityEngine;
using EventType = Cooking.Manager.EventType;

namespace Cooking.Controller
{
    public class SettingController
    {
        private static SettingController _instance;

        public static SettingController Instance
        {
            get
            {
                _instance = _instance ?? new SettingController();
                return _instance;
            }
        }
        private SettingController() { }

        private SettingData Data => PlayerDataManager.Instance.GetSettingData();

        /// <summary>获取设置数据</summary>
        public SettingData GetSettingData()
        {
            return PlayerDataManager.Instance.GetSettingData();
        }

        /// <summary>重置设置数据</summary>
        public void ResetSetting(SettingData defaultSetting)
        {
            Data.masterVolume = defaultSetting.masterVolume;
            Data.AmbientSound = defaultSetting.AmbientSound;
            Data.sfxVolume = defaultSetting.sfxVolume;
            Data.resolutionWidth = defaultSetting.resolutionWidth;
            Data.resolutionHeight = defaultSetting.resolutionHeight;
            Data.language = defaultSetting.language;
            Data.TextSpeed = defaultSetting.TextSpeed;
            Data.IsOpenTip = defaultSetting.IsOpenTip;

            ApplyResolution();
            EventManager.TriggerEvent(EventType.SettingChanged);
        }

        //*********************************************获取设置数据*********************************************

        /// <summary>获取语言</summary>
        public LanguageType GetLanguage()
        {
            return Data.language;
        }

        /// <summary>获取文本速度</summary>
        public float GetTextSpeed()
        {
            return Data.TextSpeed;
        }

        /// <summary>获取是否开启提示的boolean值</summary>
        public bool GetIsOpenTip()
        {
            return Data.IsOpenTip;
        }

        /// <summary>获取窗口分辨率</summary>
        public string GetResolutionWidthHeight()
        {
            return Data.resolutionWidth + "x" + Data.resolutionHeight;
        }

        /// <summary>读取音乐音量</summary>
        public float GetMasterVolume()
        {
            return Data.masterVolume;
        }

        /// <summary>读取环境音量</summary>
        public float GetAmbientSound()
        {
            return Data.AmbientSound;
        }

        /// <summary>读取音效音量</summary>
        public float GetSfxVolume()
        {
            return Data.sfxVolume;
        }

        /// <summary>设置是否开启提示</summary>
        public void SetIsOpenTip(bool isOpen)
        {
            Data.IsOpenTip = isOpen;
            EventManager.TriggerEvent(EventType.SettingChanged);
        }



        //*********************************************设置数据*********************************************

        /// <summary>设置语言</summary>
        public void SetLanguage(LanguageType language)
        {
            Data.language = language;
            EventManager.TriggerEvent(EventType.SettingChanged);
        }

        /// <summary>设置窗口分辨率</summary>
        public void SetResolutionWidthHeight(string widthHeight)
        {
            var parts = widthHeight.Split('x');
            if (parts.Length == 2 && int.TryParse(parts[0], out int width) && int.TryParse(parts[1], out int height))
            {
                Data.resolutionWidth = width;
                Data.resolutionHeight = height;
                ApplyResolution();
            }
        }

        /// <summary>设置文本速度</summary>
        public void SetTextSpeed(float speed)
        {
            Data.TextSpeed = speed;
            EventManager.TriggerEvent(EventType.SettingChanged);
        }

        /// <summary>设置音乐音量</summary>
        public void SetMasterVolume(float value)
        {
            value = Mathf.Clamp01(value);
            Data.masterVolume = value;
            EventManager.TriggerEvent(EventType.SettingChanged);
        }

        /// <summary>设置环境音</summary>
        public void SetAmbientSound(float value)
        {
            value = Mathf.Clamp01(value);
            Data.AmbientSound = value;
            EventManager.TriggerEvent(EventType.SettingChanged);
        }

        /// <summary>设置音效音量</summary>
        public void SetSfxVolume(float value)
        {
            value = Mathf.Clamp01(value);
            Data.sfxVolume = value;
            EventManager.TriggerEvent(EventType.SettingChanged);
        }

        /// <summary>将存档中的分辨率设置应用到游戏中</summary>
        public void ApplyResolution()
        {
            int width = Data.resolutionWidth;
            int height = Data.resolutionHeight;
            if (width <= 0 || height <= 0)
                return;

            //保持玩家当前的全屏/窗口模式不变，只改分辨率
            Screen.SetResolution(width, height, Screen.fullScreenMode);
        }

        /// <summary>保存设置数据数据</summary>
        public void SaveSetting()
        {
            PlayerDataManager.Instance.SaveSettingData();
        }

    }
}
