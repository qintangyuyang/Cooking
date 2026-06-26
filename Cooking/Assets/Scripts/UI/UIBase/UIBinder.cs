using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.UI
{
    /// <summary>
    /// UI组件绑定器：扫描根节点下所有带@前缀的子物体，按类型存储组件
    /// </summary>
    public class UIBinder : MonoBehaviour
    {
        private const string BIND_PREFIX = "@";

        //存储组件的字典
        private Dictionary<string, TextMeshProUGUI> textDic = new Dictionary<string, TextMeshProUGUI>();
        private Dictionary<string, Button> buttonDic = new Dictionary<string, Button>();
        private Dictionary<string, Image> imageDic = new Dictionary<string, Image>();
        private Dictionary<string, GameObject> gameObjectDic = new Dictionary<string, GameObject>();
        
        //用于Inspector展示的绑定信息列表，可序列化，显示在Inspector中
        [SerializeField]
        private List<BindingInfo> bindingInfos = new List<BindingInfo>();

        //公开外界调用获取组件的方法
        public TextMeshProUGUI GetText(string name)
        {
            return textDic.TryGetValue(name, out TextMeshProUGUI text) ? text : null;
        }

        public Button GetButton(string name)
        {
            return buttonDic.TryGetValue(name, out Button button) ? button : null;
        }

        public Image GetImage(string name)
        {
            return imageDic.TryGetValue(name, out Image image) ? image : null;
        }

        public GameObject GetGameObject(string name)
        {
            return gameObjectDic.TryGetValue(name, out GameObject gameObject) ? gameObject : null;
        }

        private void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            textDic.Clear();
            buttonDic.Clear();
            imageDic.Clear();
            gameObjectDic.Clear();
            bindingInfos.Clear();

            ScanAndBind();
        }

        private void ScanAndBind()
        {
            Transform[] allChildens = GetComponentsInChildren<Transform>(true);
            foreach (var child in allChildens)
            {
                if(child==transform)
                    continue;
                if(!child.name.StartsWith(BIND_PREFIX))
                    continue;

                string key = child.name.Substring(BIND_PREFIX.Length);
                GameObject go = child.gameObject;

                //存储GameObject
                gameObjectDic[key] = go;
                bindingInfos.Add(new BindingInfo() { key = key, componentType = "GameObject", target = go });
                
                //依次检查各种组件
                TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    textDic[key] = text;
                    bindingInfos.Add(new BindingInfo() { key = key, componentType = "TextMeshProUGUI", target = go });
                }

                Button button = child.GetComponent<Button>();
                if (button != null)
                {
                    buttonDic[key] = button;
                    bindingInfos.Add(new BindingInfo() { key = key, componentType = "Button", target = go });
                }
                
                Image image = child.GetComponent<Image>();
                if (image != null)
                {
                    imageDic[key] = image;
                    bindingInfos.Add(new BindingInfo() { key = key, componentType = "Image", target = go });
                }
            }
        }

        [Serializable]
        public class BindingInfo
        {
            public string key; //去掉@后的名称
            public string componentType; //组件类型名称
            public GameObject target; //对应的GameObject
        }
    }
}
