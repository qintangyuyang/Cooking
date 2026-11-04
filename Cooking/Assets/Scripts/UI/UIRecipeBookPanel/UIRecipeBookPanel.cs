using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cooking.Manager;
using Cooking.Model;
using SuperScrollView;
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
        private LoopListView2 ShowDishScrollView;
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
                ShowDishScrollView = _UIBinder.GetGameObject("ShowDishScrollView").GetComponent<LoopListView2>();

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

            GetListByMenuType(out var list, 1);
            ShowDishScrollView.InitListView(list.Count, InitMenuList);
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

        /// <summary>页面切换逻辑</summary>
        private void OnChangePage(int page)
        {
            if (nowPage != page)
            {
                GetListByMenuType(out var list, page);
                nowPage = page;
                ShowDishScrollView.SetListItemCount(list.Count);
                ShowDishScrollView.RefreshAllShownItem();
                print("打开页面：" + page);
            }
        }

        /// <summary>根据Type分类活动当前菜谱列表</summary>
        private void GetListByMenuType(out List<MenuData> list, int type)
        {
            list = new List<MenuData>();
            foreach (var item in MenuDatas)
            {
                if (item.Type == type)
                {
                    list.Add(item);
                }
            }
        }
        /// <summary>菜谱页面列表</summary>
        private LoopListViewItem2 InitMenuList(LoopListView2 list, int index)
        {
            if (index < 0)
            {
                return null;
            }
            LoopListViewItem2 item = list.NewListViewItem("DishImage");
            var itemBinder = item.GetComponent<UIBinder>();
            var image = itemBinder.GetImage("Icon");
            image.sprite = Resources.Load<Sprite>("Sprite/UI/Ingred_Tomato_icon");
            return item;
        }

    }
}
