using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Card;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: UISceneStep.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
public class UISceneStep : UIScene
{

    CardControl cardcontrol;
    public List<ItemStep> items;

    int index = 0;
    private void Awake()
    {
        cardcontrol = CourseControl.instance.objs[4].ui.GetComponent<CardControl>();
    }
    protected override void Start()
    {
        base.Start();

    }


    public void SetTween(Vector3 vec)
    {
        if (!gameObject.activeSelf) return;
        StopCoroutine(WaitFor(0.1f));
        Vector3 pos = transform.InverseTransformPoint(vec);
        items.ForEach(p => p.SetTween(pos));
    }


    public int GetCurrSelected(Vector3 pos)
    {
        if (!gameObject.activeSelf) return -1;

        for (int i = 0; i < items.Count; i++)
        {
            if (Vector3.Distance(pos, items[i].transform.position) < 50)
            {
                items[i].selected.SetActive(true);
                string temp = items[i].name.Split('_')[1];

                return int.Parse(temp);
            }
            else
            {

                index++;
                items[i].selected.SetActive(false);

            }
        }
        if (index == items.Count)
        {
            cardcontrol.right.SetActive(true);
        }
        else
        {
            index = 0;
        }


        return -1;
    }


    public void SetTweenBack(Vector3 vec)
    {
        if (!gameObject.activeSelf) return;
        Vector3 pos = transform.InverseTransformPoint(vec);
        items.ForEach(p => p.SetTweenBack(pos));
        if (gameObject.activeInHierarchy)
            StartCoroutine(WaitFor(0.1f));
    }

    IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);
        SetVisible(false);
    }

    public override void SetVisible(bool visible)
    {
        //base.SetVisible(visible);
        if (!visible)
            items.ForEach(p => p.Reset());
        gameObject.SetActive(visible);
    }

}