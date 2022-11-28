using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Universal.Card;
using Universal.CheckStage;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: StepReceiver.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
public class StepReceiver : CardOptionControl
{
    CourseControl courseControl;
    CameraControl camControl;
    StageControlCardOption stageControlCard;
    UISceneStep step;
    UISceneHint hint;
    private void Start()
    {
        camControl = Camera.main.GetComponent<CameraControl>();
        step = UIManager.Instance.GetUI<UISceneStep>(UIName.UISceneStep);
        hint = FindObjectOfType<UISceneHint>();
        stageControlCard = CourseControl.instance.objs[4].ui.GetComponent<StageControlCardOption>();
        courseControl = GameObject.Find("ControlCenter").GetComponent<CourseControl>();
    }
   

    protected override void CardRightEvent(GameObject obj)
    {
        if (obj.name.Equals("StageControl"))
        {
            if (step.gameObject.activeSelf)
            {
                CourseControl.instance.objs[4].ui.GetComponent<CardControl>().right.SetActive(false);
                int num = stageControlCard.GetStepNum();
                if (num == -1) return;

                ManagerEvent.Call(StageControl.GoToCurrStage, num);
            }
        }
    }
    public void CardAddHandler(SceneObj so)
    {
        if (so.Name.Equals("StageControl")) {
        Debug.Log("111");
        CourseControl.instance.objs[4].ui.GetComponent<CardControl>().right.SetActive(true);
            foreach (SceneObj item in courseControl.objs)
            {
                if (item.ui == gameObject) continue;
                if (!item.ui.name.Equals("StageControl") )
                {
                    if (item.ui.GetComponent<CardControl>() != null)
                        item.ui.GetComponent<CardControl>().PointLock = true;
                    if (item.model != null && item.model.GetComponent<ModelControl>() != null)
                        item.model.GetComponent<ModelControl>().PointLock = true;
                }
            }
            camControl.IsRotate = false;
            if (hint != null && hint.gameObject.activeSelf)
                hint.gameObject.SetActive(false);
        }
    }

    public void CardRemoveHandler(SceneObj so)
    {
        if (so.Name.Equals("StageControl"))
        {
            camControl.IsRotate = true;
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

  


}