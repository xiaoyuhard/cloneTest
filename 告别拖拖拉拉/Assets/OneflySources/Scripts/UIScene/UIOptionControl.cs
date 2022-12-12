using OneFlyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Card;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: UIOptionControl.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：UI操作控制
***********************************************/
public class UIOptionControl : MonoBehaviour
{

    public Dictionary<string, bool> cardCount;

    private GameObject root;
    public static string cardEventDone = "cardEventDone";
    public static string stageReset = "StageReset";
    private List<string> list;
    void Start()
    {
        cardCount = new Dictionary<string, bool>();
        root = GameObject.Find("DownUICanvas");
        Init();

    }

    private void Init()
    {
        var cards = root.transform.GetComponentsInChildren<CardControl>();
        for (int i = 0; i < root.transform.childCount; i++)
        {
            if (root.transform.GetChild(i).GetComponent<CardControl>() != null)
            {
                if (root.transform.GetChild(i).name.Equals("ClassControl")) continue;
                if (root.transform.GetChild(i).Find("Right") != null)
                    cardCount.Add(root.transform.GetChild(i).name + "Right", false);
                if (root.transform.GetChild(i).Find("Left") != null)
                    cardCount.Add(root.transform.GetChild(i).name + "Left", false);
            }
        }
        list = new List<string>(cardCount.Keys);
    }


    void OnEnable()
    {
        ManagerEvent.Register(Tips.ShowHint, ShowHintHandler);
        ManagerEvent.Register(cardEventDone, SetCardEventDoneHandler);
        ManagerEvent.Register(stageReset, StageResetHandler);
        ManagerEvent.Register("playVideo", SetPlayerVideo);

    }

    private void SetPlayerVideo(object[] args)
    {
        var uiScene = UIManager.Instance.GetUI<UISceneVideo>(UIName.UISceneVideo);
        uiScene.SetPlayVideo();
    }

    void OnDisable()
    {
        ManagerEvent.Unregister(Tips.ShowHint, ShowHintHandler);
        ManagerEvent.Unregister(cardEventDone, SetCardEventDoneHandler);
        ManagerEvent.Unregister(stageReset, StageResetHandler);
    }


    private void ShowHintHandler(params object[] args)
    {
        if (args.Length >= 2)
        {
            UISceneHint.HintType type = (UISceneHint.HintType)args[0];
            string content = (string)args[1];
            UIManager.Instance.SetVisible(UIName.UISceneHint, true);
            var uiScene = UIManager.Instance.GetUI<UISceneHint>(UIName.UISceneHint);
            uiScene.ShowHint(type, content);
        }
    }

    private void SetCardEventDoneHandler(params object[] args)
    {
        if (args.Length >= 2)
        {
            string temp = args[0].ToString();
            bool done = (bool)args[1];
            if (cardCount.ContainsKey(temp))
                cardCount[temp] = done;
        }
    }

    private void StageResetHandler(params object[] args)
    {
        var it = list.GetEnumerator();
        while (it.MoveNext())
        {
            //Debug.Log(it.Current);
            cardCount[it.Current] = false;
        }
    }

}
