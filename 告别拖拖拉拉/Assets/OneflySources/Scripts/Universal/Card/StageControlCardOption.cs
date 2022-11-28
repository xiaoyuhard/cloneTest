using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Universal.CheckStage;
using Universal.Card;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: StageControlCardOption.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
public class StageControlCardOption : CardControl {

    private StageControl stageControl;
    private float currStage = 0;
    private UISceneStep step;
	protected override void Start () {
        base.Start();
        stageControl = FindObjectOfType<StageControl>();
        step = UIManager.Instance.GetUI<UISceneStep>(UIName.UISceneStep);
    }


    public override void SetTween()
    {
        base.SetTween();
        if(step == null)
            step = UIManager.Instance.GetUI<UISceneStep>(UIName.UISceneStep);
        step.SetVisible(true);
        Vector3 vec = transform.position;
        step.SetTween(vec);
    }

    private int stepNum = -1;
    public override void HandlerEventByPos(Vector3 vec)
    {
        base.HandlerEventByPos(vec);
        //if(g)
        if (step != null)
        {
            stepNum = step.GetCurrSelected(transform.position);
        }
    }

    /// <summary>
    /// 获取当前步骤数
    /// </summary>
    /// <returns></returns>
    public int GetStepNum()
    {
        return stepNum;
    }
    //public override void HandlerEventByAngle(float angle)
    //{
    //    float temp = 360 / stageControl.StageCheckCount;
    //    //Debug.Log(angle);
    //    for (int i = 1; i <= stageControl.StageCheckCount; i++)
    //    {
    //        if (Mathf.Abs(angle - i * temp) <= 5 && currStage != i)
    //        {
    //            //触发当前步骤
    //            //Debug.Log(string.Format("跳转到第{0}步", i));
    //            ManagerEvent.Call(StageControl.GoToCurrStage, i);
    //            currStage = i;
    //            break;
    //        }
    //    }
    //}

    public override void SetTweenBack()
    {
        //base.SetTweenBack();
        if (!gameObject.activeInHierarchy) return;
        Vector3 vec = transform.position;
        if (step != null)
            step.SetTweenBack(vec);
        base.SetTweenBack();
    }

    public override void StageReset()
    {
        //base.StageReset();

    }

}
