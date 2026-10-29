using System.Collections;
using System.Collections.Generic;
using Cooking.Manager;
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
        public override void Init()
        {
            _UIBinder = this.transform.GetComponent<UIBinder>();
            if (_UIBinder != null)
            {
                //组件绑定
                OutButton = _UIBinder.GetButton("OutButton");
                RecipeStoryText = _UIBinder.GetText("RecipeStoryText");

                //事件监听
                OutButton.onClick.AddListener(OnClickOutButton);
            }
            RefreshText();
        }

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

    }
}
