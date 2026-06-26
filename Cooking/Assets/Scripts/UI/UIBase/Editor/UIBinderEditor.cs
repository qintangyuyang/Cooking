using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cooking.UI
{
    [CustomEditor(typeof(UIBinder))]
    public class UIBinderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UIBinder binder = (UIBinder)target;
            
            EditorGUILayout.Space();
            if (GUILayout.Button("刷新绑定", GUILayout.Height(30)))
            {
                binder.Refresh();
                
                //标记为脏 让Unity保存修改
                EditorUtility.SetDirty(binder);
            }
            
            EditorGUILayout.Space();

            DrawDefaultInspector();
        }
    }
}
