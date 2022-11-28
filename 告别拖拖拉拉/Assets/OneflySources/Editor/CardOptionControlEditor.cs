using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Universal.Card;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: CardOptionControlEditor.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
[CustomEditor(typeof(CardOptionControl), true)]
public class CardOptionControlEditor : Editor {


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

    //protected bool showEvents;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        this.DrawMonoScript();

        CardOptionControl control = target as CardOptionControl;

        control.showEvents = OneFlyGUITools.BeginFoldOut("Events", control.showEvents);
        if (control.showEvents)
        {
            EditorGUILayout.Space();
            serializedObject.Update();
            SerializedProperty onCardAdd = serializedObject.FindProperty("onCardAdd");
            EditorGUILayout.PropertyField(onCardAdd, true);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            SerializedProperty onCardUpdate = serializedObject.FindProperty("onCardUpdate");
            EditorGUILayout.PropertyField(onCardUpdate, true);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            SerializedProperty onCardRemove = serializedObject.FindProperty("onCardRemove");
            EditorGUILayout.PropertyField(onCardRemove, true);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            SerializedProperty onCardLeft = serializedObject.FindProperty("onCardLeft");
            EditorGUILayout.PropertyField(onCardLeft, true);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            SerializedProperty onCardRight = serializedObject.FindProperty("onCardRight");
            EditorGUILayout.PropertyField(onCardRight, true);
            serializedObject.ApplyModifiedProperties();
        }

        //Refresh
        if (GUI.changed)
            EditorUtility.SetDirty(control);
    }
}
