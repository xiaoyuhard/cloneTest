using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: CameraControlEditor.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
[CustomEditor(typeof(CameraControl))]
[CanEditMultipleObjects]
public class CameraControlEditor : ModelControlEditor {


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CameraControl control = target as CameraControl;

        control.showCamSetting = OneFlyGUITools.BeginFoldOut("相机配置面板", control.showCamSetting, false);
        if (control.showCamSetting)
        {
            EditorGUILayout.BeginVertical(paddingStyle);
            EditorGUILayout.Space();
            serializedObject.Update();
            SerializedProperty targets = serializedObject.FindProperty("Targets");
            EditorGUILayout.PropertyField(targets, true);
            serializedObject.ApplyModifiedProperties();

            control._camera = (GameObject)EditorGUILayout.ObjectField("Camera", control._camera, typeof(GameObject), true);
            control.Center = (GameObject)EditorGUILayout.ObjectField("Center", control.Center, typeof(GameObject), true);
            control.moveOrRotate = OneFlyGUITools.Toggle("moveOrRotate(选择相机移动或推进，true为推进，false为旋转)", control.moveOrRotate, true);
            if (control.moveOrRotate)
            {
                EditorGUILayout.LabelField("相机的fieldofview限制");
                control.feildMax = EditorGUILayout.FloatField("feildMax", control.feildMax);
                control.feildMin = EditorGUILayout.FloatField("feildMin", control.feildMin);
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.Space();
        if (GUI.changed)
            EditorUtility.SetDirty(control);
    }
}
