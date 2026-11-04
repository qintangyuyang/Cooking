using System.Collections;
using System.Collections.Generic;
using Cooking.Manager;
using Cooking.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.UI
{
    public class UIRecipeBookPanel : UIBase
    {
        private UIBinder _UIBinder;
        private Button OutButton;
        private TextMeshProUGUI RecipeStoryText;
        private const string MenuCfg = "MenuCfg";
        private Toggle TogPage1;
        private Toggle TogPage2;
        private Toggle TogPage3;
        private Toggle TogPage4;
        private int nowPage = -1;
        private List<MenuData> MenuDatas = new List<MenuData>();
        public override void Init()
        {
            _UIBinder = this.transform.GetComponent<UIBinder>();
            if (_UIBinder != null)
            {
                //组件绑定
                OutButton = _UIBinder.GetButton("OutButton");
                RecipeStoryText = _UIBinder.GetText("RecipeStoryText");
                TogPage1 = _UIBinder.GetGameObject("TogPage1").GetComponent<Toggle>();
                TogPage2 = _UIBinder.GetGameObject("TogPage2").GetComponent<Toggle>();
                TogPage3 = _UIBinder.GetGameObject("TogPage3").GetComponent<Toggle>();
                TogPage4 = _UIBinder.GetGameObject("TogPage4").GetComponent<Toggle>();

                //事件监听
                OutButton.onClick.AddListener(OnClickOutButton);
                TogPage1.onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        OnChangePage(1);
                    }
                });
                TogPage2.onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        OnChangePage(2);
                    }
                });
                TogPage3.onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        OnChangePage(3);
                    }
                });
                TogPage4.onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        OnChangePage(4);
                    }
                });

            }
            RefreshText();
            LoadMenuConfig();
            //默认打开时执行第一页逻辑
            if (nowPage == -1)
            {
                OnChangePage(1);
            }
        }

        /// <summary>加载食谱数据</summary>
        private void LoadMenuConfig()
        {
            MenuDatas = JsonManager.Instance.LoadData<List<MenuData>>("Config/Menu/" + MenuCfg);
        }

        /// <summary>
        /// 多语言刷新
        /// </summary>
        private void RefreshText()
        {

        }
        protected override void OnLanguageChanged()
        {
            RefreshText();
        }

        private void OnClickOutButton()
        {
            UIManager.Instance.CloseUI<UIRecipeBookPanel>();
        }

        private void OnChangePage(int page)
        {
            if (nowPage != page)
            {
                switch (page)
                {
                    case 1:
                        print("切换页面1");
                        nowPage = page;
                        break;
                    case 2:
                        print("切换页面2");
                        nowPage = page;
                        break;
                    case 3:
                        print("切换页面3");
                        nowPage = page;
                        break;
                    case 4:
                        print("切换页面4");
                        nowPage = page;
                        break;
                }
            }
        }

    }
}
