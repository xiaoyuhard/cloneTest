using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Audio;
using System;

namespace Universal
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: MatchPosManager.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    public class MatchPosManager : MonoBehaviour {

        [Serializable]
        public class MatchPosObj
        {
            [Header("匹配位置的模型")]
            public GameObject model;
            [Header("匹配位置的UI提示")]
            public GameObject hint;
            [Header("匹配位置后模型设置的本地坐标")]
            public Vector3 targetVec;
            [Header("卡牌失效序号")]
            public int index;
            private float distance;
            public bool IsDone { get; set; }

            public void Setvisible(bool visible)
            {
                if (hint != null)
                    hint.SetActive(visible);
            }

            /// <summary>
            /// 获取当前匹配位置的信息
            /// </summary>
            /// <returns></returns>
            public bool GetVisible()
            {
                if (model.activeSelf && hint.activeSelf)
                    return true;
                return false;
            }

            /// <summary>
            /// 处理模型物体的位置匹配
            /// </summary>
            public void HandlerMacthPos()
            {
                distance = Vector3.Distance(model.transform.position, hint.transform.position);
                if (distance <= 0.7f)
                {
                    //显示位置匹配音频
                    AudioPlayer.Instance.PlayAudio(Audio.AudioType.effect, 1);
                    ManagerEvent.Call(Tips.SetCardsAvailable, false, index);
                    //隐藏位置提示
                    hint.SetActive(false);
                   
                    //将当前位置赋予model
                    model.transform.localPosition = targetVec;
                    IsDone = true;
                }
            }
        }

        [Serializable]
        public class StageMatch {
            
            [Header("当前匹配位置的下标")]
            public int id;
            [Header("需要匹配位置的集合")]
            public List<MatchPosObj> matchHints;

            public void Init()
            {
                for (int i = 0; i < matchHints.Count; i++)
                {
                    matchHints[i].Setvisible(false);
                }
            }

            /// <summary>
            /// 当前步骤的匹配是否完成
            /// </summary>
            public bool IsDone
            {
                get
                {
                    if (matchHints != null && matchHints.Count > 0)
                    {
                        for (int i = 0; i < matchHints.Count; i++)
                        {
                            if (!matchHints[i].IsDone)
                                return false;
                        }
                        return true;
                    }
                    return false;
                }
            }

            /// <summary>
            /// 设置位置提示的显隐
            /// </summary>
            /// <param name="visible"></param>
            public void Setvisible(bool visible)
            {
                for (int i = 0; i < matchHints.Count; i++)
                {
                    matchHints[i].Setvisible(visible);
                }
            }

            /// <summary>
            /// 处理当前步骤的模型匹配
            /// </summary>
            public void HandlerMatchPos()
            {
                if (matchHints.Count > 0)
                {
                    for (int i = 0; i < matchHints.Count; i++)
                    {
                        if (matchHints[i].GetVisible() && !matchHints[i].IsDone)
                            matchHints[i].HandlerMacthPos();
                    }
                }
            }

            public void OnReset()
            {
                if (matchHints != null && matchHints.Count > 0)
                {
                    for (int i = 0; i < matchHints.Count; i++)
                    {
                        matchHints[i].IsDone = false;
                    }
                  
                }
            }
        }

        public static MatchPosManager Instance;

        public static string macthPos = "MatchPos";
        public static string showPosHint = "ShowPosHint";
        public static string hideAllPosHint = "HideAllPosHint";
        //public List<MatchPosObj> matchHints;
        public List<StageMatch> stages;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < stages.Count; i++)
            {
                stages[i].Init();
            }
        }

        void OnEnable()
        {
            ManagerEvent.Register(showPosHint, HandlerShowPosHintEvent);
            ManagerEvent.Register(hideAllPosHint, HandlerHideAllPosHintEvent);
        }

        //HandlerMacthPosEvent
        private void HandlerShowPosHintEvent(params object[] args)
        {
            if (args.Length > 1)
            {
                bool visible = (bool)args[0];
                for (int i = 1; i < args.Length; i++)
                {
                    int index = (int)args[i];
                    if (index < stages.Count)
                        stages[index].Setvisible(visible);
                }
            }
        }

        private void HandlerHideAllPosHintEvent(params object[] args)
        {
            for (int i = 0; i < stages.Count; i++)
            {
                stages[i].Setvisible(false);
            }
        }

        void Update()
        {
            HandlerMacthPosEvent();
        }

        private void HandlerMacthPosEvent()
        {
            if (stages.Count > 0)
            {
                for (int i = 0; i < stages.Count; i++)
                {
                    if (!stages[i].IsDone)
                        stages[i].HandlerMatchPos();
                }
            }
        }


        void OnDisbale()
        {
            ManagerEvent.Unregister(showPosHint, HandlerShowPosHintEvent);
            ManagerEvent.Unregister(hideAllPosHint, HandlerHideAllPosHintEvent);
        }

    }
}
