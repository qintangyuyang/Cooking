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

        /// <summary>装通用设置配置表里的预制名字</summary>
        private List<string> GeneralSettingDataList = new List<string>();

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
                GeneralSettingDataList = JsonMapper.ToObject<List<string>>(jsonString);
                
                _PagePanelGeneralList.InitListView(GeneralSettingDataList.Count, InitPagePanelGeneralList);
            }

            RefreshBtnType(BtnType.General);
        }

        private void RefreshBtnType(BtnType type)
        {

        }

        private LoopListViewItem2 InitPagePanelGeneralList(LoopListView2 list, int index)
        {
            if (index < 0 || index >= GeneralSettingDataList.Count)
            {
                return null;
            }

            var itemName = GeneralSettingDataList[index];
            var item = list.NewListViewItem(itemName);
            UIBinder _itemUIBinder;
            _itemUIBinder = item.transform.GetComponent<UIBinder>();
            switch (index)
            {
                case 0:
                    var Text0 = _itemUIBinder.GetText("Text");
                    Text0.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_6");//音量
                    break;
                case 1:
                    var DecText1 = _itemUIBinder.GetText("DecText");
                    DecText1.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_7");//音乐
                    Slider slider1 = _itemUIBinder.GetGameObject("Slider").GetComponent<Slider>();
                    slider1.onValueChanged.AddListener((value) =>
                    {
                        print("音乐音量改变："+value);
                    });
                    break;
                case 2:
                    var DecText2 = _itemUIBinder.GetText("DecText");
                    DecText2.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_8");//环境音
                    Slider slider2 = _itemUIBinder.GetGameObject("Slider").GetComponent<Slider>();
                    slider2.onValueChanged.AddListener((value) =>
                    {
                        print("环境音量改变："+value);
                    });
                    break;
                case 3:
                    var DecText3 = _itemUIBinder.GetText("DecText");
                    DecText3.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_9");//音效
                    Slider slider3 = _itemUIBinder.GetGameObject("Slider").GetComponent<Slider>();
                    slider3.onValueChanged.AddListener((value) =>
                    {
                        print("音效音量改变："+value);
                    });
                    break;
                case 4:
                    var DecText4 = _itemUIBinder.GetText("DecText");
                    DecText4.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_10");//白噪音
                    Slider slider4 = _itemUIBinder.GetGameObject("Slider").GetComponent<Slider>();
                    slider4.onValueChanged.AddListener((value) =>
                    {
                        print("白噪音音量改变："+value);
                    });
                    break;
                case 5:
                    var Text5 = _itemUIBinder.GetText("Text");
                    Text5.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_11");//窗口分辨率
                    break;
                case 6:
                    break;
            }
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
