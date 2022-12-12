using Course;
using DevelopEngine;
using HighlightingSystem;
using OneFlyLib;
using System.Collections;
using UnityEngine;
using Universal.Audio;
using Universal.Card;

namespace Universal.CheckStage
{
    public class StageCheck0 : StageCheck
    {
        public StageCheck0(int index) : base(index)
        {
            //这个阶段需要达成的目标 目标与目标之间关系是“和”而不是“或”当所有目标都打成以后才会进入到Exitng通常是stageGoal或者其子类

        }

        //跳步骤时，本步实现初始化
        public override void InitCurrStage()
        {

            base.InitCurrStage();

        }

        protected override IEnumerator Entering()
        {
            //阶段开始前的初始化，比如 那些卡牌可以被识别 上屏出现什么样的动画或者UI之类的 用发送消息来实习
            //ManagerEvent.Call(Tips.CheckStage);
            //OperationHint.Instance.ShowOperationHint("测试测试测试测试");

            UIManager.Instance.SetVisible(UIName.UISceneVideo, false);
            BoyManager.Instance.ObjectActive(false);
            UIManager.Instance.SetVisible(UIName.UISceneBegin, false);
            Camera.main.GetComponent<HighlightingRenderer>().enabled = false;
            UIManager.Instance.SetVisible(UIName.UIScenePlane, false);
            SortSpeech.Instance.gameObject.SetActive(false);

            yield return new WaitForSeconds(3);
            UIManager.Instance.SetVisible(UIName.UISceneBegin, true);

            OperationHint.Instance.ShowOperationHint(1);


            yield return null;
        }

        protected override IEnumerator Exiting()
        {
            //阶段结束时的操作，一般与开始的的操作相对应，比如关闭UI 或者设置某些卡牌不可用
            //Debugger.Log("完成第一步");
            //显示进度条界面
            OperationHint.Instance.CloseOperationHint();

            UIManager.Instance.SetVisible(UIName.UISceneProgress, true);
            ManagerEvent.Call(Tips.SetCardsAvailable, true, 6);

            yield return null;
        }

        //跳步骤需要重置的状态
        public override void ResetStage()
        {
            base.ResetStage();

        }

    }

    public class StageCheck1 : StageCheck
    {
        public StageCheck1(int index) : base(index)
        {
            //这个阶段需要达成的目标 目标与目标之间关系是“和”而不是“或”当所有目标都打成以后才会进入到Exitng通常是stageGoal或者其子类
            //列如：
            //SetGoals(new StageGoal("1"));

        }

        //跳步骤时，本步实现初始化
        public override void InitCurrStage()
        {
            base.InitCurrStage();

        }

        protected override IEnumerator Entering()
        {
            AudioHint.Instance.ShowHint(2, 1);

            yield return new WaitForSeconds(GetAudioLength(2));
            AudioHint.Instance.CloseHint();

            OperationHint.Instance.ShowOperationHint(2);

            //GameObject aObj = CourseControl.instance.objs[6].ui;
            //yield return new WaitUntil(() => aObj.activeSelf);
            var card6 = GetCardComponent<CardControl>(6);
            card6.right.SetActive(true);
            yield return new WaitUntil(() => GetOptionVisible("VideoRight"));

            ManagerEvent.Call("playVideo");
            OperationHint.Instance.CloseOperationHint();

            UIManager.Instance.SetVisible(UIName.UISceneVideo, true);

            yield return new WaitForSeconds(36);

            //AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 2);
            AudioHint.Instance.ShowHint(3, 2);
            yield return new WaitForSeconds(GetAudioLength(3));
            //AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 3);
            AudioHint.Instance.ShowHint(4, 3);
            yield return new WaitForSeconds(GetAudioLength(4));
            AudioHint.Instance.CloseHint();

            ManagerEvent.Call(Tips.SetCardsAvailable, true, 4, 7, 8);


            //AudioHint.Instance.ShowHint(0, 0);
            //OperationHint.Instance.ShowOperationHint(0);
            //阶段开始前的初始化，比如 那些卡牌可以被识别 上屏出现什么样的动画或者UI之类的 用发送消息来实习
            ManagerEvent.Call(Tips.CheckStage + 1);
            //ManagerEvent.Call(Tips.CheckStage + 2);
            //ManagerEvent.Call("1");


        }

        protected override IEnumerator Exiting()
        {
            //阶段结束时的操作，一般与开始的的操作相对应，比如关闭UI 或者设置某些卡牌不可用


            yield return null;
        }

        //跳步骤需要重置的状态
        public override void ResetStage()
        {

            base.ResetStage();

        }


    }

    public class StageCheck2 : StageCheck
    {
        public StageCheck2(int index) : base(index)
        {
            //这个阶段需要达成的目标 目标与目标之间关系是“和”而不是“或”当所有目标都打成以后才会进入到Exitng通常是stageGoal或者其子类
            //列如：
            //SetGoals(new StageGoal(CamControl.OpeningFinished));
            //SetGoals(new StageGoal("s2"));

        }

        //跳步骤时，本步实现初始化
        public override void InitCurrStage()
        {

            base.InitCurrStage();

        }

        protected override IEnumerator Entering()
        {
            Debugger.Log("2");

            OperationHint.Instance.ShowOperationHint(3);
            GameObject aObj4 = CourseControl.instance.objs[4].ui;
            yield return new WaitUntil(() => aObj4.activeSelf);
            UIManager.Instance.SetVisible(UIName.UISceneVideo, false);
            OperationHint.Instance.CloseOperationHint();

            //AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 4);
            AudioHint.Instance.ShowHint(5, 4);
            yield return new WaitForSeconds(GetAudioLength(5));

            AudioHint.Instance.ShowHint(6, 5);
            yield return new WaitForSeconds(GetAudioLength(6));

            Camera.main.GetComponent<HighlightingRenderer>().enabled = true;
            UIManager.Instance.SetVisible(UIName.UIScenePlane, true);

            BoyManager.Instance.ObjectActive(true);

            AudioHint.Instance.ShowHint(7, 6);
            yield return new WaitForSeconds(GetAudioLength(7));

            AudioHint.Instance.ShowHint(8, 7);

            SortSpeech.Instance.gameObject.SetActive(true);

            yield return new WaitForSeconds(GetAudioLength(8));

            OperationHint.Instance.ShowOperationHint(4);

            GameObject aObj7 = CourseControl.instance.objs[7].ui;
            yield return new WaitUntil(() => aObj7.activeSelf);

            SortSpeech.Instance.startIs = true;

            yield return new WaitUntil(() => SortSpeech.Instance.endIs);
            OperationHint.Instance.CloseOperationHint();

            AudioHint.Instance.ShowHint(17, 16);
            yield return new WaitForSeconds(GetAudioLength(17));
            UISceneUI.Instance.CloseUI();
            LoadPrefabManager.Instance.SetMonkeyObj(true);

            AudioHint.Instance.ShowHint(18, 17);
            yield return new WaitForSeconds(GetAudioLength(18));

            OperationHint.Instance.ShowOperationHint(5);
            GameObject aObj8 = CourseControl.instance.objs[8].ui;
            yield return new WaitUntil(() => aObj8.activeSelf);

            OperationHint.Instance.CloseOperationHint();

            UISceneUI.Instance.ShowUI("UI3");

            AudioHint.Instance.ShowHint(19, 18);
            yield return new WaitForSeconds(GetAudioLength(19));
            UISceneUI.Instance.CloseUI();

            ManagerEvent.Call(Tips.SetCardsAvailable, true, 3, 5);
            SortSpeech.Instance.gameObject.SetActive(false);


            //阶段开始前的初始化，比如 那些卡牌可以被识别 上屏出现什么样的动画或者UI之类的 用发送消息来实习
            //ManagerEvent.Call("s2");
            ManagerEvent.Call(Tips.CheckStage + 2);

        }

        protected override IEnumerator Exiting()
        {
            //阶段结束时的操作，一般与开始的的操作相对应，比如关闭UI 或者设置某些卡牌不可用

            yield return null;
        }

        //跳步骤需要重置的状态
        public override void ResetStage()
        {

            base.ResetStage();

        }
    }

    public class StageCheck3 : StageCheck
    {
        public StageCheck3(int index) : base(index)
        {
            //这个阶段需要达成的目标 目标与目标之间关系是“和”而不是“或”当所有目标都打成以后才会进入到Exitng通常是stageGoal或者其子类
            //列如：
            //SetGoals(new StageGoal(CamControl.OpeningFinished));

        }

        //跳步骤时，本步实现初始化
        public override void InitCurrStage()
        {

            base.InitCurrStage();

        }

        protected override IEnumerator Entering()
        {
            Debugger.Log("3");

            AudioHint.Instance.ShowHint(20, 19);
            yield return new WaitForSeconds(GetAudioLength(20));

            OperationHint.Instance.ShowOperationHint(6);

            GameObject aObj4 = CourseControl.instance.objs[4].ui;
            yield return new WaitUntil(() => aObj4.activeSelf);

            OperationHint.Instance.CloseOperationHint();

            LoadPrefabManager.Instance.SetAngleObj(true);

            AudioHint.Instance.ShowHint(21, 20);
            yield return new WaitForSeconds(GetAudioLength(21));

            AudioHint.Instance.ShowHint(22, 21);
            yield return new WaitForSeconds(GetAudioLength(22));

            OperationHint.Instance.ShowOperationHint(7);

            GameObject aObj3 = CourseControl.instance.objs[3].ui;
            yield return new WaitUntil(() => aObj3.activeSelf);
            OperationHint.Instance.CloseOperationHint();

            LoadPrefabManager.Instance.SetAngleAni(2);
            LoadPrefabManager.Instance.BlinkIs(true);
            AudioHint.Instance.ShowHint(23, 22);
            yield return new WaitForSeconds(GetAudioLength(23));

            SetClock.Instance.SetSevenHour();

            AudioHint.Instance.ShowHint(24, 23);
            yield return new WaitForSeconds(GetAudioLength(24));

            OperationHint.Instance.ShowOperationHint(8);

            var card5 = GetCardComponent<CardControl>(5);
            card5.right.SetActive(true);
            yield return new WaitUntil(() => GetOptionVisible("AngelOfReasonRight"));
            OperationHint.Instance.CloseOperationHint();

            UISceneUI.Instance.ShowUI("UI1Mon");
            LoadPrefabManager.Instance.SetMonkeyAni(true);
            BoyManager.Instance.SetAni(1);
            AudioHint.Instance.ShowHint(25);
            yield return new WaitForSeconds(GetAudioLength(25));
            OperationHint.Instance.ShowOperationHint(8);

            option.cardCount["AngelOfReasonRight"] = false;
            yield return new WaitUntil(() => GetOptionVisible("AngelOfReasonRight"));
            OperationHint.Instance.CloseOperationHint();

            UISceneUI.Instance.ShowUI("UI2Ang");
            AudioHint.Instance.ShowHint(26);
            LoadPrefabManager.Instance.SetAngleAni(3);

            UISceneUI.Instance.AttStar(1);
            yield return new WaitForSeconds(4);
            LoadPrefabManager.Instance.SetAngleAni(4);

            LoadPrefabManager.Instance.SetMonkeyMove();
            UISceneUI.Instance.SetUIMove("UI1Mon");
            yield return new WaitForSeconds(4);

            UISceneUI.Instance.CloseUIMon();

            UISceneUI.Instance.ShowUI("UI4Mon");
            AudioHint.Instance.ShowHint(27);
            yield return new WaitForSeconds(GetAudioLength(27));
            BoyManager.Instance.SetAni(11);
            yield return new WaitForSeconds(2);
            UISceneUI.Instance.CloseUI();
            UISceneUI.Instance.CloseAllUI();
            AudioHint.Instance.ShowHint(28, 27);
            yield return new WaitForSeconds(GetAudioLength(28));

            ManagerEvent.Call(Tips.CheckStage + 3);

            //阶段开始前的初始化，比如 那些卡牌可以被识别 上屏出现什么样的动画或者UI之类的 用发送消息来实习
        }

        protected override IEnumerator Exiting()
        {
            //阶段结束时的操作，一般与开始的的操作相对应，比如关闭UI 或者设置某些卡牌不可用

            yield return null;
        }

        //跳步骤需要重置的状态
        public override void ResetStage()
        {

            base.ResetStage();

        }
    }

    public class StageCheck4 : StageCheck
    {
        public StageCheck4(int index) : base(index)
        {
            //这个阶段需要达成的目标 目标与目标之间关系是“和”而不是“或”当所有目标都打成以后才会进入到Exitng通常是stageGoal或者其子类
            //列如：
            //SetGoals(new StageGoal(CamControl.OpeningFinished));

        }

        //跳步骤时，本步实现初始化
        public override void InitCurrStage()
        {
            base.InitCurrStage();

        }

        protected override IEnumerator Entering()
        {
            Debugger.Log("4");
            AudioHint.Instance.ShowHint(29, 28);

            OperationHint.Instance.ShowOperationHint(9);
            var card9 = GetCardComponent<CardControl>(8);
            card9.right.SetActive(true);
            yield return new WaitUntil(() => GetOptionVisible("SummaryRight"));

            UISceneUI.Instance.ShowUI("UI5");

            AudioHint.Instance.ShowHint(30, 29);
            yield return new WaitForSeconds(GetAudioLength(30));

            ManagerEvent.Call(Tips.SetCardsAvailable, true, 1);

            ManagerEvent.Call(Tips.CheckStage + 4);

            //阶段开始前的初始化，比如 那些卡牌可以被识别 上屏出现什么样的动画或者UI之类的 用发送消息来实习
            yield return null;
        }

        protected override IEnumerator Exiting()
        {
            //阶段结束时的操作，一般与开始的的操作相对应，比如关闭UI 或者设置某些卡牌不可用

            yield return null;
        }

        //跳步骤需要重置的状态
        public override void ResetStage()
        {

            base.ResetStage();

        }
    }

    public class StageCheck5 : StageCheck
    {
        public StageCheck5(int index) : base(index)
        {
            //这个阶段需要达成的目标 目标与目标之间关系是“和”而不是“或”当所有目标都打成以后才会进入到Exitng通常是stageGoal或者其子类
            //列如：
            //SetGoals(new StageGoal(CamControl.OpeningFinished));

        }

        //跳步骤时，本步实现初始化
        public override void InitCurrStage()
        {

            base.InitCurrStage();

        }

        protected override IEnumerator Entering()
        {
            Debugger.Log("5");

            OperationHint.Instance.ShowOperationHint(10);

            GameObject aObj1 = CourseControl.instance.objs[1].ui;
            yield return new WaitUntil(() => aObj1.activeSelf);

            //阶段开始前的初始化，比如 那些卡牌可以被识别 上屏出现什么样的动画或者UI之类的 用发送消息来实习
            yield return null;
        }

        protected override IEnumerator Exiting()
        {
            //阶段结束时的操作，一般与开始的的操作相对应，比如关闭UI 或者设置某些卡牌不可用

            yield return null;
        }

        //跳步骤需要重置的状态
        public override void ResetStage()
        {

            base.ResetStage();

        }
    }

}


