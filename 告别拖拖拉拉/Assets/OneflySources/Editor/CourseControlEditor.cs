using Course;
using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: CourseControlEditor.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
[CustomEditor(typeof(CourseControl))]
[CanEditMultipleObjects]
public class CourseControlEditor : Editor {

    protected GUIStyle paddingStyle;
    protected Object monoScript;
    protected virtual void OnEnable()
    {
        paddingStyle = new GUIStyle();
        paddingStyle.padding = new RectOffset(15, 0, 0, 0);
        this.monoScript = MonoScript.FromMonoBehaviour(target as MonoBehaviour);
        //获取当前类中可序列话的属性
         objsProperty = serializedObject.FindProperty("objs");
    }

    protected void DrawMonoScript()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", this.monoScript, typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();
    }
     
    //protected bool showSceneObjSetting;
    //protected bool showInfo;
   
    //序列化属性
    protected SerializedProperty objsProperty;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        this.DrawMonoScript();
        EditorGUILayout.Space();
        CourseControl control = target as CourseControl;
        control.showSceneObjSetting = OneFlyGUITools.BeginFoldOut("配置场景卡牌物体", control.showSceneObjSetting);
      
        
        if (control.showSceneObjSetting)
        {
            OneFlyGUITools.BeginGroup();
            EditorGUILayout.BeginVertical(paddingStyle);
            EditorGUILayout.Space();
            serializedObject.Update();
            EditorGUILayout.PropertyField(objsProperty, true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            OneFlyGUITools.EndGroup();
        }


        control.showInfo = OneFlyGUITools.BeginFoldOut("配置实验课程类型", control.showInfo);
       
        if (control.showInfo)
        {
            OneFlyGUITools.BeginGroup();
            EditorGUILayout.Space();

            control.infoType = (InfoType)EditorGUILayout.EnumPopup("课程类别(infoType)", control.infoType);
            EditorGUILayout.Space();
            OneFlyGUITools.EndGroup();
        }

        //Refresh
        if (GUI.changed)
            EditorUtility.SetDirty(control);
    }
}
