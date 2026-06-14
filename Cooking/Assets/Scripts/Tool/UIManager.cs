using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// UI管理器（Monobehaviour单例）
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    //自动挂载到场景上的UIRoot物体上
                    var UIRoot = GameObject.Find("UIRoot");
                    if (UIRoot != null)
                    {
                        _instance = UIRoot.AddComponent<UIManager>();
                    }
                    else
                    {
                        Debug.LogError("Can't find UIRoot");
                    }
                }
            }
            return _instance;
        }
    }
    
    //是否初始化的bool值
    private bool isInitialized = false;

    //一开始就获取游戏场景中的Canvas，以便后续操作
    private Transform CanvasTransform;

    private Stack<UIBase> UIStack = new Stack<UIBase>(); 

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Init()
    {
        if (isInitialized)
            return;
        CanvasTransform = this.transform.Find("Canvas") == null ? null : this.transform.Find("Canvas").transform;
        if (CanvasTransform != null)
        {
            //将场景中Canvas的父对象设置为过场景不移除
            //也就是该脚本挂载的节点 UIRoot
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogError("Canvas not found");
        }
        isInitialized = true;
    }
    
    //打开面板
    public async UniTask<T> OpenUI<T>() where T : UIBase
    {
        
        string uiName = typeof(T).Name;
        //重复打开防护
        var existUI = GetUI<T>();
        if (existUI != null)
        {
            Debug.LogWarning($"{uiName}已经被打开，不可多次打开");
            return existUI;
        }
        //加载预制体
        GameObject prefab = await LoadPrefab(uiName);
        if (prefab == null)
        {
            Debug.LogError($"Can't find UI: {uiName}");
            return null;
        }
        //实例化
        GameObject go = Instantiate(prefab, CanvasTransform);
        T ui = go.GetComponent<T>();
        if (ui == null)
        {
            Debug.LogError($"Can't find UI: {uiName}，当前预制没有挂载相对应的代码组件");
            Destroy(go);
            return null;
        }
        
        //查看当前栈顶有没有存在页面
        //存在就需要暂停
        if (UIStack.Count > 0)
        {
            UIBase TopUI = UIStack.Peek();
            //栈顶的UI不是当前打开的UI
            if (TopUI != ui)
            {
                //暂停当前栈顶UI
                TopUI.OnPause();
            }
        }
        
        //将当前打开的UI压入栈顶
        UIStack.Push(ui);
        //打开UI
        ui.OnOpen();

        return ui;
    }
    
    //关闭面板
    public void CloseUI<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;

        //这里目前只考虑一种情况，关闭的界面就是栈顶UI
        if (UIStack.Count > 0 && UIStack.Peek().GetType() == typeof(T))
        {
            //当前弹出（关闭）的页面
            UIBase TopUI = UIStack.Pop();
            //关闭前需要做的事
            TopUI.OnClose();

            //恢复上一个界面
            if(UIStack.Count > 0)
                UIStack.Peek().OnResume();
            
            //直接销毁预制
            Destroy(TopUI.gameObject);
        }
        //如果不是栈顶UI
        else
        {
            Debug.LogError($"当前想要关闭的UI: {uiName}，不是在栈顶。");

            var tempList = new List<UIBase>();
            UIBase TargetUI = null;
            
            //1.弹出所有的面板来找目标
            while (UIStack.Count > 0)
            {
                var panel = UIStack.Pop();
                if (panel is T)
                {
                    TargetUI = panel;
                    break;
                }
                tempList.Add(panel);
            }

            if (TargetUI == null)
            {
                //没有找到，把弹出去的UI全部塞回去
                for (int i = tempList.Count - 1; i >= 0; i--)
                {
                    UIStack.Push(tempList[i]);
                }
                Debug.LogError($"没有找到{uiName}面板");
                return;
            }
            
            //在Target上面的面板被暂停了，现在恢复它们
            for (int t = tempList.Count - 1; t >= 0; t--)
            {
                var panel = tempList[t];
                UIStack.Push(panel);
            }
            
            //关闭目标UI
            TargetUI.OnClose();
            Destroy(TargetUI.gameObject);
        }
    }
    
    //获得面板，用于判断当前页面是否存在
    public T GetUI<T>() where T : UIBase
    {
        return UIStack.FirstOrDefault(p => p is T) as T;
    }

    //加载预制
    private async UniTask<GameObject> LoadPrefab(string prefabName)
    {
        var prefab = Resources.LoadAsync<GameObject>($"Prefabs/{prefabName}/" + prefabName);
        await prefab;
        return prefab.asset as GameObject;
    }
}
