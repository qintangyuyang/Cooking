using System.Collections;
using System.Collections.Generic;
using Cooking.Manager;
using Cooking.UI;
using UnityEngine;

namespace Cooking
{
    public class Main : MonoBehaviour
    {
        void Start()
        {
            LanguageManager.Instance.Initialize();
            UIManager.Instance.OpenUI<UIStartPanel>();
        }

        void Update()
        {

        }
    }
}
