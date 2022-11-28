using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DevelopEngine;
using Universal.Card;

public class CameraControl : ModelControl {
    public GameObject[] Targets;
    public GameObject _camera;

    #region Editor Properties
    [HideInInspector]
    public bool showCamSetting;
    [HideInInspector]
    public bool moveOrRotate;
    [HideInInspector]
    public bool IsRotate=true;
    #endregion

    public float feildMin = 36;
    public float feildMax = 60;
    protected override void Awake()
    {
        base.Start();

    }

    protected override void Start()
    {
        base.Start();
        Init();
        ChangePos(0);
        fliedValue = _camera.GetComponent<Camera>().fieldOfView;
    }

    public override void Init()
    {
        base.Init();
        if (_camera == null)
        {
            _camera = GameObject.Find("Camera");
            Center = GameObject.Find("Center");
            GameObject cameraPos = GameObject.Find("CameraPos");
            var list = new List<GameObject>() { cameraPos };
            Targets = list.ToArray();
        }
    }


    public void ChangePos(int num, float time = 999)
    {
        lock (gameObject)
        {
            currentNumber = num;
            if (Targets == null || _camera == null) return;
            if (time != 999)
            {
                _camera.transform.DOMove(Targets[num].transform.position, time);
                _camera.transform.DORotate(Targets[num].transform.rotation.eulerAngles, time);
            }
            else
            {
                _camera.transform.position = Targets[num].transform.position;
                _camera.transform.rotation = Targets[num].transform.rotation;
            }
        }
    }

    public GameObject Center;
    /// <summary>
    /// 当前目标编号
    /// </summary>
    int currentNumber;
    public void ResetPos()
    {
        ChangePos(currentNumber);
    }
    public void Rotate(float angel)
    {
        lock (gameObject)
        {
            Init();
            _camera.transform.RotateAround(Center.transform.position, Vector3.up, angel);
        }
    }

    public override void SetCurrPos(Vector3 vec)
    { }

    public override void SetTweenBack()
    {

    }

    float fliedValue = 60;
    float currZ, offsetZ;
    protected override void MovementDelta(Vector3 pos)
    {
        if (!moveOrRotate) return;
        offset = pos - currVec;
        distance = Vector3.Distance(pos, currVec);
        if (distance < 0.1f)
            return;
        //Debug.Log(pos);
        //模型的父级为零点时可以直接用offset向量
        //Vector3 tempPos = currPos + offset * ratio;
        //否则需要将当前offset转化为相对于当前父级的向量
        Vector3 local = transform.parent.InverseTransformDirection(offset);
        Vector3 tempPos = currPos + local * ratio;
        tempPos = new Vector3(Mathf.Clamp(tempPos.x, minX, maxX), tempPos.y, Mathf.Clamp(tempPos.z, minZ, maxZ));

        #region 相机推进
        float angle = Vector3.Angle(offset, Vector3.forward);
        if (offset == Vector3.zero)
            angle = 0;
        bool moveOrBoost = pos.z > currVec.z && angle > 30 && angle < 180 - 30;
        if (moveOrBoost)
        {
            transform.localPosition = new Vector3(tempPos.x, 0, 0);
        }
        else
        {
            offsetZ = (pos.z - currVec.z);
            fliedValue = fliedValue - offsetZ;
            _camera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(fliedValue, feildMin, feildMax);
        }
        #endregion

        currPos = transform.localPosition;
        currVec = pos;


        //Debugger.Log("模型位置:" + pos.ToString());
    }

    public override void EquipReset()
    {
        base.EquipReset();
        fliedValue = 60;
        _camera.GetComponent<Camera>().fieldOfView = 60;
    }
}
