using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIBase基类，所有的UI都要继承此类
/// </summary>
public abstract class UIBase : MonoBehaviour
{
    public virtual void OnOpen(object param=null){}
    public virtual void OnClose(){}
    public virtual void OnPause(){}
    public virtual void OnResume(){}

    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 主要用于初始化 按钮事件监听等
    /// </summary>
    public abstract void Init();
}
