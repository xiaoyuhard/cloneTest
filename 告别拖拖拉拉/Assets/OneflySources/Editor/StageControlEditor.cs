using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Universal.CheckStage;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: StageControlEditor.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
[CustomEditor(typeof(StageControl))]
[CanEditMultipleObjects]
public class StageControlEditor : Editor {

    protected GUIStyle paddingStyle;
    protected Object monoScript;

    protected virtual void OnEnable()
    {
        paddingStyle = new GUIStyle();
        paddingStyle.padding = new RectOffset(15, 0, 0, 0);
        this.monoScript = MonoScript.FromMonoBehaviour(target as MonoBehaviour);
    }

    protected void DrawMonoScript()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", this.monoScript, typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();
    }

    //protected bool showLockCursor;
    //protected bool showStageCount;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawMonoScript();
        EditorGUILayout.Space();
        StageControl control = target as StageControl;
        control.showLockCursor = OneFlyGUITools.BeginFoldOut("配置鼠标锁", control.showLockCursor);
        if (control.showLockCursor)
        {
            OneFlyGUITools.BeginGroup();
            EditorGUILayout.Space();

            control.isLockCursor = OneFlyGUITools.Toggle("是否锁定鼠标(isLockCursor)", control.isLockCursor, true);
            EditorGUILayout.Space();
            OneFlyGUITools.EndGroup();
        }

        control.showStageCount = OneFlyGUITools.BeginFoldOut("配置实验步骤", control.showStageCount);

        if (control.showStageCount)
        {
            OneFlyGUITools.BeginGroup();
            EditorGUILayout.Space();
            control.stepHandler = EditorGUILayout.TextField("当前实验配置的步骤名称", control.stepHandler);
            control.StageCheckCount = EditorGUILayout.IntField("实验步骤数(StageCheckCount)", control.StageCheckCount);
            //EditorGUILayout.Space();
            OneFlyGUITools.EndGroup();
        }

        if (GUI.changed)
            EditorUtility.SetDirty(control);
    }

}
