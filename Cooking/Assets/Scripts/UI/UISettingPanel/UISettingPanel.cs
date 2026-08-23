using System;
using System.Collections.Generic;
using Cooking.Controller;
using Cooking.Manager;
using Cooking.Model;
using SuperScrollView;
using TMPro;
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

        private LoopListView2 _PagePanelOtherList;

        private UIBinder _UIBinder;

        private BtnType CurrentBtnType = BtnType.General;

        /// <summary>装通用设置配置表里的预制名字</summary>
        private List<string> GeneralSettingDataList = new List<string>();

        /// <summary>装其他设置配置表里的预制名字和多语言</summary>
        private List<List<string>> OtherSettingDataList = new List<List<string>>();

        private const string GeneralCfgName = "GeneralSettingCfg";
        private const string OtherCfgName = "OtherSettingCfg";

        // 四个选中页面预制
        private GameObject _GeneralBtnSelectTitle;
        private GameObject _KeyBtnSelectTitle;
        private GameObject _TutorialBtnSelectTitle;
        private GameObject _OtherBtnSelectTitle;

        //按钮的文本(选中和未选择两个文本)
        private TextMeshProUGUI _GeneralBtnUnSelected;
        private TextMeshProUGUI _GeneralBtnSelected;
        private TextMeshProUGUI _KeyBtnUnSelected;
        private TextMeshProUGUI _KeyBtnSelected;
        private TextMeshProUGUI _TutorialBtnUnSelected;
        private TextMeshProUGUI _TutorialBtnSelected;
        private TextMeshProUGUI _OtherBtnUnSelected;
        private TextMeshProUGUI _OtherBtnSelected;

        /// <summary>当前支持的分辨率</summary>
        private List<List<string>> _ResolutionWidthHeight = new List<List<string>>()
        {
            new List<string>(){"1920x1080","COMMON_TEXT_KEY_26"},
            new List<string>(){"1280x720","COMMON_TEXT_KEY_27"},
        };

        /// <summary>当前拥有的所有多语言</summary>
        private Dictionary<LanguageType, string> _CurrentLanguageCodeMap = new Dictionary<LanguageType, string>();

        /// <summary>多语言类型加多语言文本的结构体</summary>
        struct LanguageTypeCode
        {
            public LanguageType languageType;
            public string languageString;
        }

        /// <summary>多语言类型加多语言文本的列表</summary>
        private List<LanguageTypeCode> _CurrentLanguageCodeList = new List<LanguageTypeCode>();

        public override void Init()
        {
            _UIBinder = this.transform.GetComponent<UIBinder>();
            if (_UIBinder != null)
            {
                //将多语言代码和多语言文本装进列表里，方便后续使用
                _CurrentLanguageCodeMap = LanguageManager.Instance.GetAllLanguageCodes();
                _CurrentLanguageCodeList.Clear();
                foreach (var item in _CurrentLanguageCodeMap)
                {
                    _CurrentLanguageCodeList.Add(new LanguageTypeCode()
                    {
                        languageType = item.Key,
                        languageString = "COMMON_TEXT_KEY_" + item.Value
                    });
                }



                //组件绑定
                _OutButton = _UIBinder.GetButton("OutButton");
                _GeneralBtn = _UIBinder.GetButton("GeneralBtn");
                _KeyBtn = _UIBinder.GetButton("KeyBtn");
                _TutorialBtn = _UIBinder.GetButton("TutorialBtn");
                _OtherBtn = _UIBinder.GetButton("OtherBtn");
                _PagePanelGeneralList = _UIBinder.GetGameObject("PagePanelGeneralList").GetComponent<LoopListView2>();
                _PagePanelOtherList = _UIBinder.GetGameObject("PagePanelOtherList").GetComponent<LoopListView2>();

                //按钮的多语言绑定
                _GeneralBtnUnSelected = _GeneralBtn.transform.Find("UnSelected").GetComponent<TextMeshProUGUI>();
                _GeneralBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_11");//通用设置
                _GeneralBtnSelected = _GeneralBtn.transform.Find("Selected").GetComponent<TextMeshProUGUI>();
                _GeneralBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_11");//通用设置
                _KeyBtnUnSelected = _KeyBtn.transform.Find("UnSelected").GetComponent<TextMeshProUGUI>();
                _KeyBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_12");//键位设置
                _KeyBtnSelected = _KeyBtn.transform.Find("Selected").GetComponent<TextMeshProUGUI>();
                _KeyBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_12");//键位设置
                _TutorialBtnUnSelected = _TutorialBtn.transform.Find("UnSelected").GetComponent<TextMeshProUGUI>();
                _TutorialBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_13");//教程
                _TutorialBtnSelected = _TutorialBtn.transform.Find("Selected").GetComponent<TextMeshProUGUI>();
                _TutorialBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_13");//教程
                _OtherBtnUnSelected = _OtherBtn.transform.Find("UnSelected").GetComponent<TextMeshProUGUI>();
                _OtherBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_14");//其他
                _OtherBtnSelected = _OtherBtn.transform.Find("Selected").GetComponent<TextMeshProUGUI>();
                _OtherBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_14");//其他

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
                LoadOtherSettingConfig();
            }

            RefreshBtnType(BtnType.General);

        }

        private void RefreshText()
        {
            _GeneralBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_11");//通用设置
            _GeneralBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_11");//通用设置
            _KeyBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_12");//键位设置
            _KeyBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_12");//键位设置
            _TutorialBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_13");//教程
            _TutorialBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_13");//教程
            _OtherBtnUnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_14");//其他
            _OtherBtnSelected.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_14");//其他
        }

        protected override void OnLanguageChanged()
        {
            RefreshText();
            _PagePanelGeneralList.RefreshAllShownItem();
            _PagePanelOtherList.RefreshAllShownItem();
        }


        //加载通用设置界面json数据
        private void LoadGeneralSettingConfig()
        {
            GeneralSettingDataList = JsonManager.Instance.LoadData<List<string>>("Config/GameSetting/" + GeneralCfgName);
            _PagePanelGeneralList.InitListView(GeneralSettingDataList.Count, InitPagePanelGeneralList);
        }

        //加载其他设置界面json数据
        private void LoadOtherSettingConfig()
        {
            OtherSettingDataList = JsonManager.Instance.LoadData<List<List<string>>>("Config/GameSetting/" + OtherCfgName);
            _PagePanelOtherList.InitListView(OtherSettingDataList.Count, InitPagePanelOtherList);
        }

        private void RefreshBtnType(BtnType type)
        {
            _GeneralBtnSelectTitle.SetActive(type == BtnType.General);
            _KeyBtnSelectTitle.SetActive(type == BtnType.Key);
            _TutorialBtnSelectTitle.SetActive(type == BtnType.Tutorial);
            _OtherBtnSelectTitle.SetActive(type == BtnType.Other);

            _PagePanelGeneralList.gameObject.SetActive(type == BtnType.General);
            _PagePanelOtherList.gameObject.SetActive(type == BtnType.Other);

            if (type == BtnType.General)
            {
                _PagePanelGeneralList.RefreshAllShownItem();
            }
            else if (type == BtnType.Key)
            {

            }
            else if (type == BtnType.Tutorial)
            {

            }
            else if (type == BtnType.Other)
            {
                _PagePanelOtherList.RefreshAllShownItem();
            }
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
                    var NumText1 = _itemUIBinder.GetText("NumText");
                    DecText1.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_7");//音乐
                    Slider slider1 = _itemUIBinder.GetGameObject("Slider").GetComponent<Slider>();
                    slider1.onValueChanged.RemoveAllListeners();
                    slider1.value = SettingController.Instance.GetMasterVolume();
                    NumText1.text = slider1.value.ToString("P0");
                    slider1.onValueChanged.AddListener((value) =>
                    {
                        NumText1.text = value.ToString("P0");
                        SettingController.Instance.SetMasterVolume(value);
                    });
                    break;
                case 2:
                    var DecText2 = _itemUIBinder.GetText("DecText");
                    var NumText2 = _itemUIBinder.GetText("NumText");
                    DecText2.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_8");//环境音
                    Slider slider2 = _itemUIBinder.GetGameObject("Slider").GetComponent<Slider>();
                    slider2.onValueChanged.RemoveAllListeners();
                    slider2.value = SettingController.Instance.GetAmbientSound();
                    NumText2.text = slider2.value.ToString("P0");
                    slider2.onValueChanged.AddListener((value) =>
                    {
                        NumText2.text = value.ToString("P0");
                        SettingController.Instance.SetAmbientSound(value);
                    });
                    break;
                case 3:
                    var DecText3 = _itemUIBinder.GetText("DecText");
                    var NumText3 = _itemUIBinder.GetText("NumText");
                    DecText3.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_9");//音效
                    Slider slider3 = _itemUIBinder.GetGameObject("Slider").GetComponent<Slider>();
                    slider3.onValueChanged.RemoveAllListeners();
                    slider3.value = SettingController.Instance.GetSfxVolume();
                    NumText3.text = slider3.value.ToString("P0");
                    slider3.onValueChanged.AddListener((value) =>
                    {
                        NumText3.text = value.ToString("P0");
                        SettingController.Instance.SetSfxVolume(value);
                    });
                    break;
                case 4:
                    var Text5 = _itemUIBinder.GetText("Text");
                    Text5.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_10");//窗口分辨率
                    break;
                case 5:
                    var WindowContent5 = _itemUIBinder.GetText("WindowContent");
                    var LeftBtn5 = _itemUIBinder.GetButton("LeftBtn");
                    var RightBtn5 = _itemUIBinder.GetButton("RightBtn");
                    var ResolutionWidthHeightListCount = _ResolutionWidthHeight.Count;
                    var CurrentResolution = SettingController.Instance.GetResolutionWidthHeight();
                    //当前分辨率在列表中的索引
                    var CurrentResolutionIndex = _ResolutionWidthHeight.FindIndex(x => x[0] == CurrentResolution);
                    if (CurrentResolutionIndex < 0)
                    {
                        CurrentResolutionIndex = 0;
                    }
                    WindowContent5.text = LanguageManager.Instance.GetText(_ResolutionWidthHeight[CurrentResolutionIndex][1]);
                    LeftBtn5.onClick.RemoveAllListeners();
                    LeftBtn5.onClick.AddListener(() =>
                    {
                        CurrentResolutionIndex--;
                        if (CurrentResolutionIndex >= 0)
                        {
                            WindowContent5.text = LanguageManager.Instance.GetText(_ResolutionWidthHeight[CurrentResolutionIndex][1]);
                        }
                        else if (CurrentResolutionIndex < 0)
                        {
                            CurrentResolutionIndex = ResolutionWidthHeightListCount - 1;
                            WindowContent5.text = LanguageManager.Instance.GetText(_ResolutionWidthHeight[CurrentResolutionIndex][1]);
                        }
                        SettingController.Instance.SetResolutionWidthHeight(_ResolutionWidthHeight[CurrentResolutionIndex][0]);
                    });
                    RightBtn5.onClick.RemoveAllListeners();
                    RightBtn5.onClick.AddListener(() =>
                    {
                        CurrentResolutionIndex++;
                        if (CurrentResolutionIndex < ResolutionWidthHeightListCount)
                        {
                            WindowContent5.text = LanguageManager.Instance.GetText(_ResolutionWidthHeight[CurrentResolutionIndex][1]);
                        }
                        else if (CurrentResolutionIndex >= ResolutionWidthHeightListCount)
                        {
                            CurrentResolutionIndex = 0;
                            WindowContent5.text = LanguageManager.Instance.GetText(_ResolutionWidthHeight[CurrentResolutionIndex][1]);
                        }
                        SettingController.Instance.SetResolutionWidthHeight(_ResolutionWidthHeight[CurrentResolutionIndex][0]);
                    });
                    break;
                case 6:
                    var Text6 = _itemUIBinder.GetText("Text");
                    Text6.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_15");//语言
                    break;
                case 7:
                    var WindowContent7 = _itemUIBinder.GetText("WindowContent");
                    var LeftBtn7 = _itemUIBinder.GetButton("LeftBtn");
                    var RightBtn7 = _itemUIBinder.GetButton("RightBtn");
                    var currentLanguage = LanguageManager.Instance.GetCurrentLanguage();
                    WindowContent7.text = LanguageManager.Instance.GetText(_CurrentLanguageCodeList.Find(v => v.languageType == currentLanguage).languageString);
                    //当前的多语言的列表索引
                    var CurrentLanguageIndex = _CurrentLanguageCodeList.FindIndex(v => v.languageType == currentLanguage);
                    LeftBtn7.onClick.RemoveAllListeners();
                    LeftBtn7.onClick.AddListener(() =>
                    {
                        CurrentLanguageIndex--;
                        if (CurrentLanguageIndex >= 0)
                        {
                            WindowContent7.text = LanguageManager.Instance.GetText(_CurrentLanguageCodeList[CurrentLanguageIndex].languageString);
                        }
                        else if (CurrentLanguageIndex < 0)
                        {
                            CurrentLanguageIndex = _CurrentLanguageCodeList.Count - 1;
                            WindowContent7.text = LanguageManager.Instance.GetText(_CurrentLanguageCodeList[CurrentLanguageIndex].languageString);
                        }
                        LanguageManager.Instance.SwitchLanguage(_CurrentLanguageCodeList[CurrentLanguageIndex].languageType);
                    });
                    RightBtn7.onClick.RemoveAllListeners();
                    RightBtn7.onClick.AddListener(() =>
                    {
                        CurrentLanguageIndex++;
                        if (CurrentLanguageIndex < _CurrentLanguageCodeList.Count)
                        {
                            WindowContent7.text = LanguageManager.Instance.GetText(_CurrentLanguageCodeList[CurrentLanguageIndex].languageString);
                        }
                        else if (CurrentLanguageIndex >= _CurrentLanguageCodeList.Count)
                        {
                            CurrentLanguageIndex = 0;
                            WindowContent7.text = LanguageManager.Instance.GetText(_CurrentLanguageCodeList[CurrentLanguageIndex].languageString);
                        }
                        LanguageManager.Instance.SwitchLanguage(_CurrentLanguageCodeList[CurrentLanguageIndex].languageType);
                    });
                    break;
                case 8:
                    var Text8 = _itemUIBinder.GetText("Text");
                    Text8.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_16");//文本速度
                    break;
                case 9:
                    //文本速度切换
                    var WindowContent9 = _itemUIBinder.GetText("WindowContent");
                    var TextSpeed = SettingController.Instance.GetTextSpeed();
                    if (TextSpeed == 0.5f)
                    {
                        WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_17");//慢
                    }
                    else if (TextSpeed == 1)
                    {
                        WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_18");//中
                    }
                    else if (TextSpeed == 1.5f)
                    {
                        WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_19");//快
                    }
                    var LeftBtn9 = _itemUIBinder.GetButton("LeftBtn");
                    var RightBtn9 = _itemUIBinder.GetButton("RightBtn");
                    LeftBtn9.onClick.RemoveAllListeners();
                    RightBtn9.onClick.RemoveAllListeners();
                    LeftBtn9.onClick.AddListener(() =>
                    {
                        if (TextSpeed == 0.5f)
                        {
                            TextSpeed = 1.5f;
                            WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_19");//快
                        }
                        else if (TextSpeed == 1)
                        {
                            TextSpeed = 0.5f;
                            WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_17");//慢
                        }
                        else if (TextSpeed == 1.5f)
                        {
                            TextSpeed = 1;
                            WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_18");//中
                        }
                        SettingController.Instance.SetTextSpeed(TextSpeed);
                    });
                    RightBtn9.onClick.AddListener(() =>
                    {
                        if (TextSpeed == 0.5f)
                        {
                            TextSpeed = 1;
                            WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_18");//中
                        }
                        else if (TextSpeed == 1)
                        {
                            TextSpeed = 1.5f;
                            WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_19");//快
                        }
                        else if (TextSpeed == 1.5f)
                        {
                            TextSpeed = 0.5f;
                            WindowContent9.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_17");//慢
                        }
                        SettingController.Instance.SetTextSpeed(TextSpeed);
                    });
                    break;
                case 10:
                    var Text10 = _itemUIBinder.GetText("Text");
                    Text10.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_20");//是否开启提示
                    break;
                case 11:
                    //是否开启提示切换
                    var WindowContent11 = _itemUIBinder.GetText("WindowContent");
                    var LeftBtn11 = _itemUIBinder.GetButton("LeftBtn");
                    var RightBtn11 = _itemUIBinder.GetButton("RightBtn");
                    LeftBtn11.onClick.RemoveAllListeners();
                    RightBtn11.onClick.RemoveAllListeners();
                    WindowContent11.text = SettingController.Instance.GetIsOpenTip() ? LanguageManager.Instance.GetText("COMMON_TEXT_KEY_21") : LanguageManager.Instance.GetText("COMMON_TEXT_KEY_22");
                    LeftBtn11.onClick.AddListener(() =>
                    {
                        if (SettingController.Instance.GetIsOpenTip())
                        {
                            SettingController.Instance.SetIsOpenTip(false);
                            WindowContent11.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_22");
                        }
                        else
                        {
                            SettingController.Instance.SetIsOpenTip(true);
                            WindowContent11.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_21");
                        }
                    });
                    RightBtn11.onClick.AddListener(() =>
                    {
                        if (SettingController.Instance.GetIsOpenTip())
                        {
                            SettingController.Instance.SetIsOpenTip(false);
                            WindowContent11.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_22");
                        }
                        else
                        {
                            SettingController.Instance.SetIsOpenTip(true);
                            WindowContent11.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_21");
                        }
                    });
                    break;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(item.CachedRectTransform);
            return item;
        }

        private LoopListViewItem2 InitPagePanelOtherList(LoopListView2 list, int index)
        {
            if (index < 0 || index >= OtherSettingDataList.Count)
                return null;
            var itemName = OtherSettingDataList[index][0];
            var item = list.NewListViewItem(itemName);
            UIBinder _itemUIBinder;
            _itemUIBinder = item.transform.GetComponent<UIBinder>();
            var Text = _itemUIBinder.GetText("Text");
            var contentText = OtherSettingDataList[index][1];
            Text.text = LanguageManager.Instance.GetText(contentText);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item.CachedRectTransform);
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

            SettingController.Instance.SaveSetting();
        }
    }

}
