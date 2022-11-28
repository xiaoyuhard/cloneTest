using Course;
using DevelopEngine;
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
            OperationHint.Instance.ShowOperationHint(1);


            AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 1);
            AudioHint.Instance.ShowHintByConf(1);
            OperationHint.Instance.ShowOperationHint(2);
            yield return new WaitForSeconds(GetAudioLength(2));

            yield return null;
        }

        protected override IEnumerator Exiting()
        {
            //阶段结束时的操作，一般与开始的的操作相对应，比如关闭UI 或者设置某些卡牌不可用
            //Debugger.Log("完成第一步");
            //显示进度条界面
            UIManager.Instance.SetVisible(UIName.UISceneProgress, true);
            ManagerEvent.Call(Tips.SetCardsAvailable, true, 4);
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
            //SetGoals(new StageGoal(CamControl.OpeningFinished));

        }

        //跳步骤时，本步实现初始化
        public override void InitCurrStage()
        {

            base.InitCurrStage();

        }

        protected override IEnumerator Entering()
        {
            Debugger.Log("1");

            ManagerEvent.Call(Tips.SetCardsAvailable, true, 5);
            //AudioHint.Instance.ShowHint(0, 0);
            //OperationHint.Instance.ShowOperationHint(0);
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

    public class StageCheck2 : StageCheck
    {
        public StageCheck2(int index) : base(index)
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
            Debugger.Log("2");
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

    public class StageCheck6 : StageCheck
    {
        public StageCheck6(int index) : base(index)
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
            Debugger.Log("6");
           
            AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 2);
            AudioHint.Instance.ShowHintByConf(2);
            yield return new WaitForSeconds(GetAudioLength(3));
            AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 3);
            AudioHint.Instance.ShowHintByConf(3);
            yield return new WaitForSeconds(GetAudioLength(4));

            OperationHint.Instance.ShowOperationHint(3);


            AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 4);
            AudioHint.Instance.ShowHintByConf(4);
            yield return new WaitForSeconds(GetAudioLength(5));


            AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 5);
            AudioHint.Instance.ShowHintByConf(5);
            yield return new WaitForSeconds(GetAudioLength(6));



            AudioPlayer.Instance.PlayAudio(Audio.AudioType.speech, 6);
            AudioHint.Instance.ShowHintByConf(6);
            yield return new WaitForSeconds(GetAudioLength(7));


            ManagerEvent.Call(Tips.SetCardsAvailable, true, 5);
            var card5 = GetCardComponent<CardControl>(5);
            card5.right.SetActive(true);
            SetOption("videoRight", false);
            yield return new WaitUntil(() => GetOptionVisible("videoRight"));


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

    public class StageCheck7 : StageCheck
    {
        public StageCheck7(int index) : base(index)
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
            Debugger.Log("7");
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

    public class StageCheck8 : StageCheck
    {
        public StageCheck8(int index) : base(index)
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
            Debugger.Log("8");
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

    public class StageCheck9 : StageCheck
    {
        public StageCheck9(int index) : base(index)
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
            Debugger.Log("9");
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


