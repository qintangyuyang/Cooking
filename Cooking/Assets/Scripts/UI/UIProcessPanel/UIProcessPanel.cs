using System.Collections;
using System.Collections.Generic;
using Cooking.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.UI
{
    public class UIProcessPanel : UIBase
    {

        private TextMeshProUGUI text;
        
        public override void Init()
        {
            //text = transform.Find("Coin_Img/Num_Txt").GetComponent<TextMeshProUGUI>();

            RefreshText();
            
        }

        protected override void OnLanguageChanged()
        {
            RefreshText();
        }

        private void RefreshText()
        {
            //text.text = LanguageManager.Instance.GetText("text1");
        }
    }
}
