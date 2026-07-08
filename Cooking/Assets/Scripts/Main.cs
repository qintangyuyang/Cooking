using System;
using System.Collections;
using System.Collections.Generic;
using Cooking.Manager;
using Cooking.UI;
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
            LanguageManager.Instance.Initialize();
            PlayerDataManager.Instance.LoadAllPlayerData();
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
