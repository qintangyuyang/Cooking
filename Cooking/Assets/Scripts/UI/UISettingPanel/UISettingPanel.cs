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

        private GameObject _GeneralBtnSelectTitle;
        private GameObject _KeyBtnSelectTitle;
        private GameObject _TutorialBtnSelectTitle;
        private GameObject _OtherBtnSelectTitle;

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

                _GeneralBtnSelectTitle = _GeneralBtn.transform.Find("Selected").gameObject;
                _KeyBtnSelectTitle = _KeyBtn.transform.Find("Selected").gameObject;
                _TutorialBtnSelectTitle = _TutorialBtn.transform.Find("Selected").gameObject;
                _OtherBtnSelectTitle = _OtherBtn.transform.Find("Selected").gameObject;
                
                //禁用Button自带的颜色动画
                _GeneralBtn.transition = Selectable.Transition.None;
                _KeyBtn.transition = Selectable.Transition.None;
                _TutorialBtn.transition = Selectable.Transition.None;
                _OtherBtn.transition = Selectable.Transition.None;

                //事件监听
                _OutButton.onClick.AddListener(OnClickOutButton);
                _GeneralBtn.onClick.AddListener(() => OnClickTab(BtnType.General));
                _KeyBtn.onClick.AddListener(() => OnClickTab(BtnType.Key));
                _TutorialBtn.onClick.AddListener(() => OnClickTab(BtnType.Tutorial));
                _OtherBtn.onClick.AddListener(() => OnClickTab(BtnType.Other));

                LoadGeneralSettingConfig();
            }

            RefreshBtnType(BtnType.General);
        }

        private void LoadGeneralSettingConfig()
        {
            GeneralSettingDataList = JsonManager.Instance.LoadData<List<string>>("Config/GameSetting/" + GeneralCfgName);
            _PagePanelGeneralList.InitListView(GeneralSettingDataList.Count, InitPagePanelGeneralList);
        }
        
        private void RefreshBtnType(BtnType type)
        {
            _GeneralBtnSelectTitle.SetActive(type == BtnType.General);
            _KeyBtnSelectTitle.SetActive(type == BtnType.Key);
            _TutorialBtnSelectTitle.SetActive(type == BtnType.Tutorial);
            _OtherBtnSelectTitle.SetActive(type == BtnType.Other);
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
                    var Text5 = _itemUIBinder.GetText("Text");
                    Text5.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_10");//窗口分辨率
                    break;
                case 5:
                    
                    break;
                case 6:
                    var Text6 = _itemUIBinder.GetText("Text");
                    Text6.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_15");//语言
                    break;
                case 7:
                    break;
                case 8:
                    var Text8 = _itemUIBinder.GetText("Text");
                    Text8.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_16");//文本速度
                    break;
                case 9:
                    break;
                case 10:
                    var Text10 = _itemUIBinder.GetText("Text");
                    Text10.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_20");//是否开启提示
                    break;
                case 11:
                    break;
            }
            return item;
        }

        private void OnClickOutButton()
        {
            UIManager.Instance.CloseUI<UISettingPanel>();
        }

        private void OnClickTab(BtnType type)
        {
            if (CurrentBtnType == type)
                return;
            CurrentBtnType = type;
            RefreshBtnType(type);
        }

        public override void OnClose()
        {
            base.OnClose();
            _OutButton.onClick.RemoveListener(OnClickOutButton);
            _GeneralBtn.onClick.RemoveAllListeners();
            _KeyBtn.onClick.RemoveAllListeners();
            _TutorialBtn.onClick.RemoveAllListeners();
            _OtherBtn.onClick.RemoveAllListeners();
        }
    }
}
