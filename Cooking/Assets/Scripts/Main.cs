using System;
using System.Collections;
using System.Collections.Generic;
using Cooking.Manager;
using Cooking.UI;
using UnityEngine;
using Cooking.Controller;

namespace Cooking
{
    public class Main : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            LanguageManager.Instance.Initialize();
            PlayerDataManager.Instance.LoadAllPlayerData();
            SettingController.Instance.ApplyResolution();
            AudioManager.Instance.PlaBGM(Resources.Load<AudioClip>("AudioSource/BGM"));
            AudioManager.Instance.PlayAmbientSound(Resources.Load<AudioClip>("AudioSource/AmbientSound_Forest_Loop"));
            UIManager.Instance.OpenUI<UIStartPanel>();
        }

        private void OnApplicationQuit()
        {
            PlayerDataManager.Instance.SaveAllPlayerData();
            PlayerDataManager.Instance.SaveSettingData();
        }

        void Update()
        {

        }
    }
}
