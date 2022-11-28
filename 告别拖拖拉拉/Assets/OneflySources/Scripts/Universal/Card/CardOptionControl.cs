using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Universal.Audio;

namespace Universal.Card
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: CardOptionControl.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    [System.Serializable]
    public class OnCardChangeHandler : UnityEvent<SceneObj> { }

    [System.Serializable]
    public class OnCardEventHandler : UnityEvent<GameObject> { }
    public class CardOptionControl : MonoBehaviour
    {
        [SerializeField]
        public OnCardChangeHandler onCardAdd;

        [SerializeField]
        public OnCardChangeHandler onCardUpdate;

        [SerializeField]
        public OnCardChangeHandler onCardRemove;

        [SerializeField]
        public OnCardEventHandler onCardLeft;

        [SerializeField]
        public OnCardEventHandler onCardRight;

        #region Editor Properties
        [HideInInspector]
        public bool showEvents;
        #endregion

        void OnEnable()
        {
            ManagerEvent.Register(Tips.CardAdd, CardAddHandler);
            ManagerEvent.Register(Tips.CardUpdate, CardUpdateHandler);
            ManagerEvent.Register(Tips.CardRemove, CardRemoveHandler);
            ManagerEvent.Register(CardOrientation.CardLeft.ToString(), CardLeftHandler);
            ManagerEvent.Register(CardOrientation.CardRight.ToString(), CardRightHandler);
        }

        void OnDisable()
        {
            ManagerEvent.Unregister(Tips.CardAdd, CardAddHandler);
            ManagerEvent.Unregister(Tips.CardUpdate, CardUpdateHandler);
            ManagerEvent.Unregister(Tips.CardRemove, CardRemoveHandler);
            ManagerEvent.Unregister(CardOrientation.CardLeft.ToString(), CardLeftHandler);
            ManagerEvent.Unregister(CardOrientation.CardRight.ToString(), CardRightHandler);
        }

        protected SceneObj so = null;
        protected GameObject tempObj = null;
        private void CardAddHandler(params object[] args)
        {
            if (args.Length > 0 && args[0] is SceneObj)
            {
                so = args[0] as SceneObj;
                if (so != null)
                {
                    onCardAdd.Invoke(so);
                }
            }
            
        }

        private void CardUpdateHandler(params object[] args)
        {
            if (args.Length > 0 && args[0] is SceneObj)
            {
                so = args[0] as SceneObj;
                if (so != null)
                    onCardUpdate.Invoke(so);
            }
        }


        private void CardRemoveHandler(params object[] args)
        {
            if (args.Length > 0 && args[0] is SceneObj)
            {
                so = args[0] as SceneObj;
                if (so != null)
                    onCardRemove.Invoke(so);
            }
        }

        private void CardLeftHandler(params object[] args)
        {
            if (args.Length > 0 && args[0] is GameObject)
            {
                tempObj = args[0] as GameObject;
                if (tempObj != null)
                {
                    CardLeftEvent(tempObj);
                    onCardLeft.Invoke(tempObj);
                }
            }
        }

        private void CardRightHandler(params object[] args)
        {
            if (args.Length > 0 && args[0] is GameObject)
            {
                tempObj = args[0] as GameObject;
                if (tempObj != null)
                {
                    CardRightEvent(tempObj);
                    onCardRight.Invoke(tempObj);
                }
            }
        }

        /// <summary>
        /// 卡牌的左转事件（只执行一次）
        /// </summary>
        /// <param name="obj">卡牌UI物体</param>
        protected virtual void CardLeftEvent(GameObject obj)
        {

        }

        /// <summary>
        /// 卡牌的右转事件（只执行一次）
        /// </summary>
        /// <param name="obj">卡牌UI物体</param>
        protected virtual void CardRightEvent(GameObject obj)
        {

        }
       
    }
}
