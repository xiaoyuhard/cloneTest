using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Universal.Card;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: BeginCardEditor.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
[CustomEditor(typeof(BeginCard))]
public class BeginCardEditor : Editor {

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

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        this.DrawMonoScript();

        BeginCard card = target as BeginCard;
        EditorGUILayout.Space();
        card.showPoint = OneFlyGUITools.BeginFoldOut("卡牌UI的指示箭头", card.showPoint);
        //showPoint = EditorGUILayout.Foldout(showPoint, "卡牌UI的指示箭头");
        EditorGUILayout.BeginVertical(paddingStyle);
        if (card.showPoint)
        {
            EditorGUILayout.Space();
            card.point = (Transform)EditorGUILayout.ObjectField("请将箭头拖入此框(point)", card.point, typeof(Transform), true);
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndVertical();

        card.angleLimit = EditorGUILayout.BeginToggleGroup("是否限制角度(angleLimit)", card.angleLimit);
        EditorGUILayout.BeginVertical(paddingStyle);

        if (card.angleLimit)
        {
            EditorGUILayout.Space();
            card.maxAngle = EditorGUILayout.FloatField("旋转限制的最大角度(maxAngle)", card.maxAngle);
            card.minAngle = EditorGUILayout.FloatField("旋转限制的最小角度(minAngle)", card.minAngle);
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndToggleGroup();

        //Refresh
        if (GUI.changed)
            EditorUtility.SetDirty(card);
    }
}
