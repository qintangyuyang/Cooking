using Cooking.Manager;
using Cooking.Model;
using UnityEngine;

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
        private SettingController(){}

        private SettingData Data => PlayerDataManager.Instance.GetSettingData();

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

        /// <summary>设置音乐音量</summary>
        public void SetMasterVolume(float value)
        {
            value = Mathf.Clamp01(value);
            Data.masterVolume = value;
        }
        
        /// <summary>设置环境音</summary>
        public void SetAmbientSound(float value)
        {
            value = Mathf.Clamp01(value);
            Data.AmbientSound = value;
        }
        
        /// <summary>设置音效音量</summary>
        public void SetSfxVolume(float value)
        {
            value = Mathf.Clamp01(value);
            Data.sfxVolume = value;
        }
        
        /// <summary>保存设置数据数据</summary>
        public void SaveSetting()
        {
            PlayerDataManager.Instance.SaveSettingData();
        }

    }
}
