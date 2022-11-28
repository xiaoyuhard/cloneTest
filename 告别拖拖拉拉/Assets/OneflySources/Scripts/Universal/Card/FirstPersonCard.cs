using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Card;

public class FirstPersonCard : CardControl
{
    float OldAngel = 0;
    public CourseControl courseControl;

    private bool visible;

    #region Editor 
    public bool showCourse;
    #endregion

    UISceneHint hint;
    CameraControl camControl;
    protected override void Start()
    {
        base.Start();
        hint = FindObjectOfType<UISceneHint>();
        camControl = GetModel().GetComponent<CameraControl>();
    }


    public override void HandlerEventByAngle(float angle)
    {
        base.HandlerEventByAngle(angle);
        //print(angle);
        if (!camControl.moveOrRotate&& camControl.IsRotate)
        {
            camControl.Rotate(angle - OldAngel);
            OldAngel = angle;
        }
    }


    private void OnEnable()
    {
        foreach (SceneObj item in courseControl.objs)
        {
            if (item.ui == gameObject) continue;
            if (item.ui.GetComponent<CardControl>() != null)
                item.ui.GetComponent<CardControl>().PointLock = true;
            if (item.model != null && item.model.GetComponent<ModelControl>() != null)
                item.model.GetComponent<ModelControl>().PointLock = true;
        }
        if (hint != null && hint.gameObject.activeSelf)
            hint.gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        camControl.ResetPos();
        OldAngel = 0;
        if (hint != null && !hint.gameObject.activeSelf)
            hint.gameObject.SetActive(true);
        foreach (SceneObj item in courseControl.objs)
        {
            if (item.ui == gameObject) continue;
            if (item.ui.GetComponent<CardControl>() != null)
                item.ui.GetComponent<CardControl>().PointLock = false;
            if (item.model != null && item.model.GetComponent<ModelControl>() != null)
                item.model.GetComponent<ModelControl>().PointLock = false;
        }
    }


}
