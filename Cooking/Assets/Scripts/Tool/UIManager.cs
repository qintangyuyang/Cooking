using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    //一开始就获取游戏场景中的Canvas，以便后续操作
    private Transform CanvasTransform;

    private UIManager()
    {
        CanvasTransform = GameObject.Find("Canvas").transform;
        if (CanvasTransform != null)
        {
            GameObject.DontDestroyOnLoad(CanvasTransform.transform.parent.gameObject);
        }
        else
        {
            Debug.LogError("Canvas not found");
        }
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
