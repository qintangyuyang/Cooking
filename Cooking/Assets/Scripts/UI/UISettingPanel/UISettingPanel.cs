using System.Collections.Generic;
using System.IO;
using Cooking.Manager;
using Cooking.UI;
using LitJson;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.UI
{
    public class UISettingPanel : UIBase
    {
        enum BtnType
        {
            //通用
            General,

            //键位
            Key,

            //教程
            Tutorial,

            //其他
            Other
        }

        /// <summary>关闭界面按钮</summary>
        private Button _OutButton;

        /// <summary>通用设置按钮</summary>
        private Button _GeneralBtn;

        /// <summary>键位设置按钮</summary>
        private Button _KeyBtn;

        /// <summary>教程按钮</summary>
        private Button _TutorialBtn;

        /// <summary>其他按钮</summary>
        private Button _OtherBtn;

        /// <summary>通用设置界面列表</summary>
        private LoopListView2 _PagePanelGeneralList;

        private UIBinder _UIBinder;

        private BtnType CurrentBtnType = BtnType.General;

        private const string GeneralCfgName = "GeneralSettingCfg";

        public override void Init()
        {
            _UIBinder = this.transform.GetComponent<UIBinder>();
            if (_UIBinder != null)
            {
                //组件绑定
                _OutButton = _UIBinder.GetButton("OutButton");
                _GeneralBtn = _UIBinder.GetButton("GeneralBtn");
                _KeyBtn = _UIBinder.GetButton("KeyBtn");
                _TutorialBtn = _UIBinder.GetButton("TutorialBtn");
                _OtherBtn = _UIBinder.GetButton("OtherBtn");
                _PagePanelGeneralList = _UIBinder.GetGameObject("PagePanelGeneralList").GetComponent<LoopListView2>();

                //事件监听
                _OutButton.onClick.AddListener(OnClickOutButton);
                _GeneralBtn.onClick.AddListener(OnClickGeneralBtn);
                _KeyBtn.onClick.AddListener(OnClickKeyBtn);
                _TutorialBtn.onClick.AddListener(OnClickTutorialBtn);
                _OtherBtn.onClick.AddListener(OnClickOtherBtn);

                string path = Application.streamingAssetsPath + "/Config/GameSetting/" + GeneralCfgName + ".json";
                var jsonString=File.ReadAllText(path);
                var SettingData = JsonMapper.ToObject<Dictionary<string, Dictionary<string, int>>>(jsonString);
                _PagePanelGeneralList.InitListView(1, InitPagePanelGeneralList);
            }

            RefreshBtnType(BtnType.General);
        }

        private void RefreshBtnType(BtnType type)
        {

        }

        private LoopListViewItem2 InitPagePanelGeneralList(LoopListView2 list, int index)
        {
            var item = list.NewListViewItem("ItemText");
            return item;
        }

        private void OnClickOutButton()
        {
            UIManager.Instance.CloseUI<UISettingPanel>();
        }

        private void OnClickGeneralBtn()
        {
            Debug.LogError("点击通用设置按钮");
            if (CurrentBtnType != BtnType.General)
            {
                CurrentBtnType = BtnType.General;
                RefreshBtnType(CurrentBtnType);
            }
        }

        private void OnClickKeyBtn()
        {
            Debug.LogError("点击键位设置按钮");
            if (CurrentBtnType != BtnType.Key)
            {
                CurrentBtnType = BtnType.Key;
                RefreshBtnType(CurrentBtnType);
            }
        }

        private void OnClickTutorialBtn()
        {
            Debug.LogError("点击教程按钮");
            if (CurrentBtnType != BtnType.Tutorial)
            {
                CurrentBtnType = BtnType.Tutorial;
                RefreshBtnType(CurrentBtnType);
            }
        }

        private void OnClickOtherBtn()
        {
            Debug.LogError("点击其他按钮");
            if (CurrentBtnType != BtnType.Other)
            {
                CurrentBtnType = BtnType.Other;
                RefreshBtnType(CurrentBtnType);
            }
        }

        public override void OnClose()
        {
            base.OnClose();
            _OutButton.onClick.RemoveListener(OnClickOutButton);
            _GeneralBtn.onClick.RemoveListener(OnClickGeneralBtn);
            _KeyBtn.onClick.RemoveListener(OnClickKeyBtn);
            _TutorialBtn.onClick.RemoveListener(OnClickTutorialBtn);
            _OtherBtn.onClick.RemoveListener(OnClickOtherBtn);
        }
    }
}
