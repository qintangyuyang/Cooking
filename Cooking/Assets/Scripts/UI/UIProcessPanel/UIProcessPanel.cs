using Cooking.Manager;
using TMPro;
using UnityEngine.UI;

namespace Cooking.UI
{
    public class UIProcessPanel : UIBase
    {
        private UIBinder _UIBinder;
        private TextMeshProUGUI text;

        private Button OrderBtn;
        
        public override void Init()
        {
            _UIBinder = this.transform.GetComponent<UIBinder>();
            if (_UIBinder != null)
            {
                //组件绑定
                OrderBtn = _UIBinder.GetButton("OrderBtn");

                //事件绑定
                OrderBtn.onClick.AddListener(OnClickOrderBtn);
            }
            
        }

        protected override void OnLanguageChanged()
        {
            RefreshText();
        }

        private void RefreshText()
        {

        }
        
        private void OnClickOrderBtn()
        {
            UIManager.Instance.OpenUI<UIRecipeBookPanel>();
        }
    }
}
