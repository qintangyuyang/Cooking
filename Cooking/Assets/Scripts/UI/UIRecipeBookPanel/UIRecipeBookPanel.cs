using System.Collections.Generic;
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
        private GameObject MenuListContent;

        /// <summary>菜品Item的图片</summary>
        private Image ItemImage;
        /// <summary>菜品列表行的Obj</summary>
        private GameObject LineItemObj;
        /// <summary>菜品Obj</summary>
        private GameObject MenuItemObj;

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
                MenuListContent = _UIBinder.GetGameObject("MenuListContent");

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

        /// <summary>页面切换逻辑</summary>
        private void OnChangePage(int page)
        {
            if (nowPage != page)
            {
                GetListByMenuType(out var list, page);
                RefreshMenuList(list);
                nowPage = page;
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

        /// <summary>当前列表下菜谱行的物体列表</summary>
        private List<GameObject> LineObjList = new List<GameObject>();
        /// <summary>刷新当前页签下展示菜谱列表逻辑</summary>
        private void RefreshMenuList(List<MenuData> list)
        {
            if (LineObjList.Count > 0)
            {
                for (int i = 0; i < LineObjList.Count; i++)
                {
                    Destroy(LineObjList[i]);
                }
                LineObjList.Clear();
            }
            //每行展示两个菜品
            //多的排到下一行

            //多少行
            int LineNum = list.Count / 2;
            if (list.Count % 2 != 0)
                LineNum += 1;

            LineItemObj = Resources.Load<GameObject>("Prefabs/UIRecipeBookPanel/LineItem");
            for (int i = 0; i < LineNum; i++)
            {
                MenuItemObj = Instantiate(LineItemObj, MenuListContent.transform);
                LineObjList.Add(MenuItemObj);
                MenuItemObj.SetActive(true);
                //判断当前行展示一个还是两个菜品
                //并且为item设置数据
                if ((i + 1) * 2 <= list.Count)
                {
                    MenuItemObj.transform.GetChild(0).gameObject.SetActive(true);
                    SetMenuData(MenuItemObj.transform.GetChild(0).gameObject, i * 2);
                    MenuItemObj.transform.GetChild(1).gameObject.SetActive(true);
                    SetMenuData(MenuItemObj.transform.GetChild(1).gameObject, i * 2 + 1);
                }
                else
                {
                    MenuItemObj.transform.GetChild(0).gameObject.SetActive(true);
                    SetMenuData(MenuItemObj.transform.GetChild(0).gameObject, i * 2);
                }
            }
        }

        /// <summary>
        /// 给菜谱item设置数据
        /// </summary>
        private void SetMenuData(GameObject obj, int index)
        {
            var uiBinder = obj.GetComponent<UIBinder>();
            if (uiBinder != null)
            {
                ItemImage = uiBinder.GetImage("Icon");
                ItemImage.sprite = Resources.Load<Sprite>("Sprite/UI/Ingred_Tomato_icon");
                Button itemBtn = uiBinder.GetButton("Button");
                itemBtn.onClick.AddListener(() =>
                {
                    print("当前点击的菜品index:" + index);
                });
            }
        }

        public override void OnClose()
        {
            base.OnClose();
            OutButton.onClick.RemoveListener(OnClickOutButton);
        }

    }
}
