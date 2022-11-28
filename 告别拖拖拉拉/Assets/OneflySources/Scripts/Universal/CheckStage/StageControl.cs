﻿using OneFlyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.CheckStage
{
    public class StageControl : MonoBehaviour
    {
        #region Editor Properties
        [HideInInspector]
        public bool showLockCursor;
        [HideInInspector]
        public bool showStageCount;
        #endregion

        [Header("是否锁定鼠标")]
        public bool isLockCursor = true;
        [Header("实验步骤数")]
        public int StageCheckCount;

        /// <summary>跳转到开始</summary>
        public static string GoToBegin = "GoToBegin";

        /// <summary>跳转到结束</summary>
        public static string GoToEnd = "GoToEnd";

        /// <summary>跳转到当前步骤</summary>
        public static string GoToCurrStage = "GoToCurrStage";

        public static string StageEntered = "StageEntered";
        public static string StageFinished = "StageFinished";
        public static string StageExited = "StageExited";

        private List<StageCheck> stageCheckList = new List<StageCheck>();
        private int stageCheckIndex = -1;
        private Coroutine stageCoroutine = null;

        private void Awake()
        {
            InitAllStages();
        }

        //[Header("当前实验配置的步骤名称")]
        public string stepHandler = "StageCheck";
        private void InitAllStages()
        {
            for (int i = 0; i <= StageCheckCount; i++)
            {
                var nameSpace = typeof(StageControl).Namespace;
                string classFullName = string.Format(stepHandler + "{0}", i.ToString());
                if (!string.IsNullOrEmpty(nameSpace))
                    classFullName = nameSpace + "." + classFullName;
                Type type = Type.GetType(classFullName);
                if (type != null)
                {
                    var check = Activator.CreateInstance(type, i) as StageCheck;
                    stageCheckList.Add(check);
                }
            }
        }

        private void Start()
        {
            //ManagerEvent.Send(DownScreenUIControl.SetIfCheckCardAvailable, true); //开启卡牌识别 部分项目可以不需要这一句
            GoToBeginHandler();
        }

        private void OnEnable()
        {
            ManagerEvent.Register(GoToBegin, GoToBeginHandler);
            ManagerEvent.Register(GoToEnd, GoToEndHandler);
            ManagerEvent.Register(StageEntered, StageEnteredHandler);
            ManagerEvent.Register(StageFinished, StageFinishedHandler);
            ManagerEvent.Register(StageExited, StageExitedHandler);
            ManagerEvent.Register(GoToCurrStage, GoToCurrStageHandler);
        }

        private void OnDisable()
        {
            ManagerEvent.Register(GoToBegin, GoToBeginHandler);
            ManagerEvent.Register(GoToEnd, GoToEndHandler);
            ManagerEvent.Unregister(StageEntered, StageEnteredHandler);
            ManagerEvent.Unregister(StageFinished, StageFinishedHandler);
            ManagerEvent.Unregister(StageExited, StageExitedHandler);
            ManagerEvent.Unregister(GoToCurrStage, GoToCurrStageHandler);
        }

        private void Update()
        {
            lockCursor();
        }

        private void lockCursor()
        {
            if (isLockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        // --------------------------------------------------------------
        //开始步骤执行
        private void GoToBeginHandler(params object[] args)
        {
            if (stageCheckList != null && stageCheckList.Count > 0)
            {
                for (int i = 0; i < stageCheckList.Count; i++)
                {
                    stageCheckList[i].ResetStage();
                }
            }

            EnterStage(0);
        }

        private void GoToEndHandler(params object[] args)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
        }

        // --------------------------------------------------------------
        //进入当前步骤
        private void EnterStage(object arg)
        {
            if (arg != null && arg is int)
            {
                int index = (int)arg;
                //Debug.Log("EnterStageHandler 指定 stage：" + index);
                StartStageEnterOrExit(index, true);
            }
            else
            {
                //Debug.Log("EnterStageHandler 下一个");
                StartStageEnterOrExit(stageCheckIndex + 1, true);
            }
        }

        private void StartStageEnterOrExit(int index, bool isEnter)
        {
            if (0 <= index && index < stageCheckList.Count && stageCoroutine == null)
            {
                //Debug.Log("StartStageEnterOrExit ~~ isEnter :" + isEnter);
                stageCheckIndex = index;
                if (isEnter)
                {
                    stageCoroutine = StartCoroutine(stageCheckList[index].StartEnter());
                }
                else
                {
                    stageCoroutine = StartCoroutine(stageCheckList[index].StartExit());
                }
            }
        }

        // --------------------------------------------------------------
        private bool CheckStageIndex(params object[] args)
        {
            if (args != null && args.Length == 1 && args[0] is int)
            {
                if (stageCheckIndex == (int)args[0])
                {
                    if (stageCoroutine != null)
                    {
                        StopCoroutine(stageCoroutine);
                        stageCoroutine = null;
                    }
                    return true;
                }
                else return false;
            }
            else return false;
        }

        private void StageEnteredHandler(params object[] args)
        {
            if (CheckStageIndex(args))
            {
                //Debug.Log("StageEnteredHandler stage: " + stageCheckIndex);
            }
        }

        private void StageFinishedHandler(params object[] args)
        {
            if (CheckStageIndex(args))
            {
                //Debug.Log("StageFinishedHandler stage: " + stageCheckIndex);
                StartStageEnterOrExit(stageCheckIndex, false);
            }
        }

        private void StageExitedHandler(params object[] args)
        {
            if (CheckStageIndex(args))
            {
                //Debug.Log("StageExitedHandler stage: " + stageCheckIndex);
                EnterStage(null);
            }
        }

        /// <summary>
        /// 切换到当前步骤
        /// </summary>
        /// <param name="index">步骤号</param>
        public void ChangeCurrStage(int index)
        {
            //所有此步骤前的目前状态和模型状态重置
            if (stageCheckList != null && stageCheckList.Count > 0)
            {
                for (int i = stageCheckList.Count; i > index; i--)
                {
                    stageCheckList[i].ResetStage();
                }
            }
        }

        int currIndex;
        //跳转到当前步骤
        private void GoToCurrStageHandler(params object[] args)
        {
            if (args.Length > 0)
                currIndex = (int)args[0];
            if (currIndex >= stageCheckList.Count) return;
            //所有此步骤前的目前状态和模型状态重置 
            if (stageCheckList != null && stageCheckList.Count > 0)
            {
                //重置步骤中所有已完成的状态     
                for (int i = 0; i < stageCheckList.Count; i++)
                {
                    stageCheckList[i].ResetStage();
                }

                //停止所有协程
                StopAllCoroutines();
                if (stageCoroutine != null)
                {
                    StopCoroutine(stageCoroutine);
                    stageCoroutine = null;
                }
            }
            //达到当前步骤的初始状态
            for (int i = 1; i <= currIndex; i++)
            {
                stageCheckList[i].InitCurrStage();
            }
            
            //进入当前步骤
            EnterStage(currIndex);
        }
    }
}
