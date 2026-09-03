using System;
using System.Collections;
using System.Collections.Generic;
using Cooking.Controller;
using Cooking.Manager;
using Cooking.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
            PlayerDataManager.Instance.LoadAllPlayerData();
            LanguageManager.Instance.Initialize();
            SettingController.Instance.ApplyResolution();
            AudioManager.Instance.PlaBGM(Resources.Load<AudioClip>("AudioSource/BGM"));
            AudioManager.Instance.PlayAmbientSound(Resources.Load<AudioClip>("AudioSource/AmbientSound_Forest_Loop"));
            OpenUI().Forget();
        }

        public async UniTask OpenUI()
        {
            await UIManager.Instance.OpenUI<UIStartPanel>();
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
