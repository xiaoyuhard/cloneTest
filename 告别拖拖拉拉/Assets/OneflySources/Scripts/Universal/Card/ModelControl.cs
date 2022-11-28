using DevelopEngine;
using OneFlyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.Card
{

    public enum MoveType
    {
        REALTIME,
        DELTA,
        OTHER
    }
    /// <summary>
    /// 卡牌3D物体的控制
    /// </summary>
    public class ModelControl : LabObject
    {
        /// <summary>
        /// 当前卡牌位置
        /// </summary>
        //protected Vector3 currVec;
        /// <summary>
        /// 当前模型位置
        /// </summary>
        protected Vector3 currPos;

        #region Editor Properties
        [HideInInspector]
        public bool showMoveSetting;
        [HideInInspector]
        public bool showEvents;
        #endregion

        //操作锁
        [HideInInspector]
        public bool PointLock;

        [Header("3D物体移动方式")]
        public MoveType moveType = MoveType.REALTIME;

        [Header("增量移动的缩放系数")]
        public float ratio = 1;

        [HideInInspector]
        public bool canMove = true;

        public Action callback;

        GameObject ui;
        protected Vector3 initPos;
        protected Vector3 initRot;
        protected Vector3 initScale;
        protected bool isCheck = false;

        protected virtual void Awake()
        {
            isCheck = true;
            initPos = transform.localPosition;
            initRot = transform.localEulerAngles;
            initScale = transform.localScale;
            if (mainCam == null)
                mainCam = Camera.main;
        }

        /// <summary>
        /// 当前物体的初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            //ui = GetUI();
            //Debugger.Log(ui.name.ToString());
            //canMove = true;        
        }

        /// <summary>
        /// 设置放入卡牌的实时位置
        /// </summary>
        /// <param name="vec"></param>
        public override void SetCurrPos(Vector3 vec)
        {
            //base.SetCurrPos(vec);
            if (PointLock || !canMove) return;
            if (moveType == MoveType.REALTIME)
                transform.localPosition = vec;
        }

        /// <summary>
        /// 物体的入场动画（若有，只执行一次）
        /// </summary>
        public override void SetTween()
        {
            base.SetTween();
            currPos = transform.localPosition;
        }
        public bool single = false;
        protected Vector3 offset;
        protected float distance;
        /// <summary>
        /// 通过位置处理事件
        /// </summary>
        /// <param name="pos"></param>
        public override void HandlerEventByPos(Vector3 pos)
        {
            if (PointLock || !canMove) return;
            //base.HandlerModelByPos(pos);
            //增量移动
            switch (moveType)
            {
                case MoveType.REALTIME:
                    transform.localPosition = pos;
                    break;
                case MoveType.DELTA:
                    if (!single)
                    {
                        InitDeltaVec(pos);
                        currPos = transform.localPosition;
                        single = true;
                    }
                    MovementDelta(pos);
                    break;
                case MoveType.OTHER:
                    break;
                default:
                    break;
            }

        }

        //增量移动
        protected virtual void MovementDelta(Vector3 pos)
        {
            offset = pos - currVec;
            distance = Vector3.Distance(pos, currVec);
            if (distance < 0.1f)
                return;

            //模型的父级为零点时可以直接用offset向量
            //Vector3 tempPos = currPos + offset * ratio;
            //否则需要将当前offset转化为相对于当前父级的向量
            Vector3 local = transform.parent.InverseTransformDirection(offset);
            Vector3 tempPos = currPos + local * ratio;
            //tempPos = new Vector3(Mathf.Clamp(tempPos.x, minX, maxX), Mathf.Clamp(tempPos.y, minX, maxX), Mathf.Clamp(tempPos.z, minZ, maxZ));
            //Debug.LogError(tempPos);
            switch (moveSpace)
            {
                case MOVE_SPACE.XY:
                    tempPos = isLimit ? new Vector3(Mathf.Clamp(tempPos.x, minX, maxX), Mathf.Clamp(tempPos.y, minY, maxY), tempPos.z) : new Vector3(tempPos.x, tempPos.y, tempPos.z);
                    break;
                case MOVE_SPACE.XZ:
                    tempPos = isLimit ? new Vector3(Mathf.Clamp(tempPos.x, minX, maxX), tempPos.y, Mathf.Clamp(tempPos.z, minZ, maxZ)) : new Vector3(tempPos.x, tempPos.y, tempPos.z);
                    break;
                case MOVE_SPACE.YZ:
                    tempPos = isLimit ? new Vector3(tempPos.x, Mathf.Clamp(tempPos.y, minY, maxY), Mathf.Clamp(tempPos.z, minZ, maxZ)) : new Vector3(tempPos.x, tempPos.y, tempPos.z);
                    break;
                case MOVE_SPACE.X:
                    tempPos = isLimit ? new Vector3(Mathf.Clamp(tempPos.x, minX, maxX), tempPos.y, tempPos.z) : new Vector3(tempPos.x, tempPos.y, tempPos.z);
                    break;
                case MOVE_SPACE.Y:
                    tempPos = isLimit ? new Vector3(tempPos.x, Mathf.Clamp(tempPos.y, minY, maxY), tempPos.z) : new Vector3(tempPos.x, transform.position.y, tempPos.z);
                    break;
                case MOVE_SPACE.Z:
                    tempPos = isLimit ? new Vector3(tempPos.x, tempPos.y, Mathf.Clamp(tempPos.z, minZ, maxZ)) : new Vector3(tempPos.x, transform.position.y, tempPos.z);
                    break;
                default:
                    break;
            }
            transform.localPosition = tempPos;
            currPos = transform.localPosition;
            currVec = pos;
        }

        /// <summary>
        /// 设置增量移动的起始位置
        /// </summary>
        /// <param name="pos"></param>
        public void SetCurrDeltaStartPos(Vector3 pos)
        {
            currPos = transform.parent.InverseTransformPoint(pos);
            transform.localPosition = currPos;
        }

        /// <summary>
        /// 通过角度处理事件
        /// </summary>
        /// <param name="angle"></param>
        public override void HandlerEventByAngle(float angle)
        {
            if (PointLock) return;
            //base.HandlerEventByAngle(angle);
            //Debugger.Log("模型角度:" + angle.ToString());
        }

        /// <summary>
        /// 物体的出场动画（若有，只执行一次）
        /// </summary>
        public override void SetTweenBack()
        {
            //base.SetTweenBack();
            if (callback != null)
                callback();
        }

        /// <summary>
        /// 物体的重置（只执行一次）
        /// </summary>
        public override void EquipReset()
        {
            base.EquipReset();

        }


        private void OnDisable()
        {
            single = false;
        }
        /// <summary>
        /// 当前模型物体的状态重置
        /// </summary>
        public virtual void StageReset()
        {
            if (!isCheck) return;
            transform.localPosition = initPos;
            transform.localEulerAngles = initRot;
            transform.localScale = initScale;
            canMove = true;
        }

    }
}
