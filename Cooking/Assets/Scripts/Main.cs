using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
