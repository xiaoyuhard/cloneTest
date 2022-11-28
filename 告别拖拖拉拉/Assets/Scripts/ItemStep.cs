using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: ItemStep.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：
***********************************************/
public class ItemStep : MonoBehaviour {

    private Vector3 pos;//初始的位置
    private DOTweenAnimation fadeAnim;
    private Image image;
    public GameObject selected;
	void Awake () {
        pos = GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log(pos + "   " + transform.localPosition);
        fadeAnim = GetComponent<DOTweenAnimation>();
        image = GetComponent<Image>();
    }


    public void SetTween(Vector3 vec)
    {
        transform.localPosition = vec;
        //开始fade动画
        fadeAnim.DOPlayForward();
        //开始位移动画
        transform.DOLocalMove(pos, 0.3f);
    }


    public void SetTweenBack(Vector3 vec)
    {
        transform.DORewind();
        fadeAnim.DOPlayBackwards();
        transform.DOLocalMove(vec, 0.3f);
    }


    //当前物体重置
    public void Reset()
    {
        //重置当前图片的透明度
        if (image != null)
            image.color = new Color(1, 1, 1, 0);
        selected.SetActive(false);
    }
	
}
