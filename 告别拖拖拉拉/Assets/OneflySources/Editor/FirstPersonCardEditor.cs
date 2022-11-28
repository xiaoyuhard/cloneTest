using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: FirstPersonCardEditor.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
[CustomEditor(typeof(FirstPersonCard))]
public class FirstPersonCardEditor : Editor {

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

    //protected bool showPoint;
    //protected bool leftOrRight;
    //protected bool showCourse;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        this.DrawMonoScript();

        FirstPersonCard card = target as FirstPersonCard;
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
        //EditorGUILayout.Space();

        card.leftOrRight = OneFlyGUITools.BeginFoldOut("左右旋转事件的UI", card.leftOrRight);
        //leftOrRight = EditorGUILayout.Foldout(leftOrRight, "左右旋转事件的UI");
        EditorGUILayout.BeginVertical(paddingStyle);

        if (card.leftOrRight)
        {
            EditorGUILayout.Space();
            card.left = (GameObject)EditorGUILayout.ObjectField("左转事件UI(left)", card.left, typeof(GameObject), true);
            card.left_select = (GameObject)EditorGUILayout.ObjectField("左转选中状态UI(left_select)", card.left_select, typeof(GameObject), true);
            card.right = (GameObject)EditorGUILayout.ObjectField("右转事件UI(right)", card.right, typeof(GameObject), true);
            card.right_select = (GameObject)EditorGUILayout.ObjectField("右转选中状态UI(right_select)", card.right_select, typeof(GameObject), true);
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndVertical();
        //EditorGUILayout.Space();
        card.showCourse = OneFlyGUITools.BeginFoldOut("课程控制器", card.showCourse);
        //showPoint = EditorGUILayout.Foldout(showPoint, "卡牌UI的指示箭头");
        EditorGUILayout.BeginVertical(paddingStyle);
        if (card.showCourse)
        {
            EditorGUILayout.Space();
            card.courseControl = (CourseControl)EditorGUILayout.ObjectField("课程控制脚本(courseControl)", card.courseControl, typeof(CourseControl), true);
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
