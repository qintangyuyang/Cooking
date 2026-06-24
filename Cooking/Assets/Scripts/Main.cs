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
            UIManager.Instance.OpenUI<UIProcessPanel>();
        }

        void Update()
        {

        }
    }
}
