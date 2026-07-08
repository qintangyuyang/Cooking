using System.Collections;
using System.Collections.Generic;
using Cooking.Model;
using UnityEngine;

namespace Cooking.Manager
{
    /// <summary>
    /// 玩家数据管理器
    /// </summary>
    public class PlayerDataManager
    {
        private static PlayerDataManager _instance;

        public static PlayerDataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerDataManager();
                }
                return _instance;
            }
        }
        private PlayerDataManager(){}

        /// <summary>
        /// 当局用于数据交互的玩家数据
        /// </summary>
        private PlayerInfoData _playerInfoData;

        /// <summary>
        /// 用于数据交互的背包数据
        /// </summary>
        private BagData _bagData;

        /// <summary>
        /// 用于交互的设置数据
        /// </summary>
        private SettingData _settingData;

        /// <summary>
        /// 将玩家的数据全部载入到内存
        /// </summary>
        public void LoadAllPlayerData()
        {
            //首先加载玩家数据
            _playerInfoData = JsonManager.Instance.LoadData<PlayerInfoData>("PlayerInfoData");
            //加载背包数据
            _bagData = JsonManager.Instance.LoadData<BagData>("BagData");
            //加载设置数据
            _settingData = JsonManager.Instance.LoadData<SettingData>("SettingData");
        }

        /// <summary>
        /// 保存玩家数据
        /// </summary>
        public void SaveAllPlayerData()
        {
            //首先加载玩家数据
            JsonManager.Instance.SaveData(_playerInfoData, "PlayerInfoData");
            //加载背包数据
            JsonManager.Instance.SaveData(_bagData, "BagData");
        }

        /// <summary>
        /// 设置数据的保存需要单独调用
        /// </summary>
        public void SaveSettingData()
        {
            JsonManager.Instance.SaveData(_settingData, "SettingData");
        }
        
        public PlayerInfoData GetPlayerInfoData()
        {
            return _playerInfoData;
        }

        public BagData GetBagData()
        {
            return _bagData;
        }

        public SettingData GetSettingData()
        {
            return _settingData;
        }
    }
}
