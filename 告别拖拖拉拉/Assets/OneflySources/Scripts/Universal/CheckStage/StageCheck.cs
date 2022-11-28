﻿using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Audio;

namespace Universal.CheckStage
{
    public class StageCheck
    {
        protected Dictionary<string, List<StageGoal>> eventMsgToGoals;
        protected Dictionary<string, ManagerEvent.Handler> eventMsgToHandler;
        protected Dictionary<string, HashSet<StageGoal>> achievedGoals;

        protected List<SceneObj> objs;

        protected List<MatchPosManager.StageMatch> stages;
        protected UIOptionControl option;

        protected bool _IsFinished = false;
        public bool IsFinished
        {
            get { return _IsFinished; }
        }

        protected int _Index = -1;
        public int Index
        {
            get { return _Index; }
        }

        public StageCheck(int index)
        {
            objs = CourseControl.instance.objs;
            stages = MatchPosManager.Instance.stages;
            option = GameObject.FindObjectOfType<UIOptionControl>();
            InitStage(index);
        }

        private void InitStage(int index)
        {
            _Index = index;
            _IsFinished = false;
            eventMsgToGoals = new Dictionary<string, List<StageGoal>>();
            eventMsgToHandler = new Dictionary<string, ManagerEvent.Handler>();
            achievedGoals = new Dictionary<string, HashSet<StageGoal>>();
            SetGoals(new StageGoal(Tips.CheckStage + index));
        }

        public virtual void InitCurrStage()
        {

            //隐藏所有提示
            ManagerEvent.Call(MatchPosManager.hideAllPosHint);

            //所有UI旋转事件重置
            ManagerEvent.Call(UIOptionControl.stageReset);

            //隐藏所有卡牌物体
            CourseControl.instance.ResetObjs();

            //UI面板重置
            UIManager.Instance.StageReset();

        }

        protected virtual void SetGoals(params StageGoal[] goals)
        {
            if (goals != null && goals.Length > 0)
            {
                var it = goals.GetEnumerator();
                while (it.MoveNext())
                {
                    StageGoal goal = (StageGoal)it.Current;
                    if (!eventMsgToGoals.ContainsKey(goal.EventMsg))
                    {
                        eventMsgToGoals.Add(goal.EventMsg, new List<StageGoal>());
                    }
                    eventMsgToGoals[goal.EventMsg].Add(goal);

                    if (!eventMsgToHandler.ContainsKey(goal.EventMsg))
                    {
                        eventMsgToHandler.Add(goal.EventMsg, null);
                    }

                    if (!achievedGoals.ContainsKey(goal.EventMsg))
                    {
                        achievedGoals.Add(goal.EventMsg, new HashSet<StageGoal>());
                    }
                }
            }
        }

        public virtual void RegisterEventMsgOrNot(bool isRegister)
        {
            //Debug.Log("StageCheck " + Index + " RegisterEventMsgOrNot " + isRegister);
            if (isRegister)
            {
                var it_msg = eventMsgToGoals.Keys.GetEnumerator();
                while (it_msg.MoveNext())                  
                {
                    string eventMsg = it_msg.Current;
                    List<StageGoal> goalList = eventMsgToGoals[eventMsg];
                    HashSet<StageGoal> achieved = achievedGoals[eventMsg];
                    eventMsgToHandler[eventMsg] = (args) => { CheckingHandler(goalList, achieved, args); };
                    ManagerEvent.Register(it_msg.Current, eventMsgToHandler[eventMsg]);
                }
            }
            else
            {
                var it_handler = eventMsgToHandler.GetEnumerator();
                while (it_handler.MoveNext())
                {
                    string eventMsg = it_handler.Current.Key;
                    ManagerEvent.Handler handler = it_handler.Current.Value;
                    ManagerEvent.Unregister(eventMsg, handler);
                }
            }
        }

        protected virtual void CheckingHandler(List<StageGoal> goalList, HashSet<StageGoal> achieved, object[] args)
        {
            if (_IsFinished) return;
            HashSet<StageGoal> achievedThisTime = new HashSet<StageGoal>();
            var it_goal = goalList.GetEnumerator();
            while (it_goal.MoveNext())
            {
                it_goal.Current.SetValue(args);
                if (it_goal.Current.Achieved)
                {
                    achievedThisTime.Add(it_goal.Current);
                }
            }

            if (achievedThisTime.Count > 0)
            {
                var it_achieved = achievedThisTime.GetEnumerator();
                while (it_achieved.MoveNext())
                {
                    goalList.Remove(it_achieved.Current);
                    achieved.Add(it_achieved.Current);
                }
            }
            // 检查整个状态是否完成
            _IsFinished = true;
            var it = eventMsgToGoals.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Value.Count != 0)
                {
                    _IsFinished = false;
                    break;
                } 
            }

            if (_IsFinished)
            {
                Debug.Log("StageControl.StageFinished index : " + Index);
                ManagerEvent.Call(StageControl.StageFinished, Index);
            }
        }

        public IEnumerator StartEnter()
        {
            ResetStage();
            RegisterEventMsgOrNot(true);
            yield return Entering();
            ManagerEvent.Call(StageControl.StageEntered, Index);
        }

        protected virtual IEnumerator Entering()
        {
            yield return null;
        }

        public IEnumerator StartExit()
        {
            //ResetStage();
            yield return Exiting();
            //改变当前进度显示
            ManagerEvent.Call(StageControl.GoToCurrStage, Index + 1);
            ManagerEvent.Call(StageControl.StageExited, Index);
        }
        protected virtual IEnumerator Exiting()
        {
            yield return null;
        }

        public virtual void ResetStage()
        {
            RegisterEventMsgOrNot(false);
            var it_achieved = achievedGoals.GetEnumerator();
            while (it_achieved.MoveNext())
            {
                if (it_achieved.Current.Value.Count > 0)
                {
                    List<StageGoal> goals = eventMsgToGoals[it_achieved.Current.Key];
                    goals.AddRange(it_achieved.Current.Value);
                    var it_goal = goals.GetEnumerator();
                    while (it_goal.MoveNext())
                    {
                        it_goal.Current.ResetValue();
                    }
                    it_achieved.Current.Value.Clear();
                }
            }
            _IsFinished = false;
        }

        /// <summary>
        /// 获取卡牌物体上的脚本（CardControl/ModelCOntrol）
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="index">卡牌配置下标</param>
        /// <returns></returns>
        public T GetCardComponent<T>(int index) where T : Component
        {
            return CourseControl.instance.GetCardComponent<T>(index);
        }


        /// <summary>
        /// 通过下标获取音频长度
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float GetAudioLength(int index)
        {
            return AudioPlayer.Instance.GetLength(index);
        }

        /// <summary>
        /// 设置卡牌选项的状态
        /// </summary>
        /// <param name="optionName">选项名称</param>
        /// <param name="visible">选项状态</param>
        public void SetOption(string optionName, bool visible)
        {
            ManagerEvent.Call(UIOptionControl.cardEventDone, optionName, visible);
        }

        /// <summary>
        /// 获取选项卡牌的状态
        /// </summary>
        /// <param name="optionName"></param>
        /// <returns></returns>
        public bool GetOptionVisible(string optionName)
        {
            return option.cardCount[optionName];
        }


        /// <summary>
        /// 重置Animation动画
        /// </summary>
        /// <param name="anim"></param>
        public void ResetAnimation(Animation anim)
        {
            foreach (AnimationState state in anim)
            {
                anim.Play(state.name);
                state.time = 0;
                state.enabled = true;
                anim.Sample();
                state.enabled = false;
            }
        }
    }

}

