using System;
using System.Collections;
using System.Collections.Generic;
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
                    var go = new GameObject("UIManager");
                    _instance = go.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }
    
    //是否初始化的bool值
    private bool isInitialized = false;

    //一开始就获取游戏场景中的Canvas，以便后续操作
    private Transform CanvasTransform;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Init()
    {
        if (isInitialized)
            return;
        CanvasTransform = this.transform.Find("Canvas").transform;
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
    public T OpenUI<T>() where T : UIBase
    {
        return default(T);
    }
    
    //关闭面板
    public void CloseUI<T>() where T : UIBase
    {
        
    }
    
    //获得面板
    public T GetUI<T>() where T : UIBase
    {
        return default(T);
    }
}
