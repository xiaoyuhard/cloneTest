using Course;
using DevelopEngine;
using Newtonsoft.Json;
using OneFlyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Universal.Card;
using Universal.Flow;

public class CourseControl : CourseManager {

    public static CourseControl instance;
    public InfoType infoType = InfoType.OTHER;
    private List<CourseInfo> infos = new List<CourseInfo>();
    public static CourseInfo courseInfo;
    public static string jsonPath = "/courseInfo.json";

    #region Editor Properties
    [HideInInspector]
    public bool showSceneObjSetting;

    [HideInInspector]
    public bool showInfo;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void Start()
    {
        //base.Start();
        //StartCoroutine(Init());
        //Debug.LogError(Application.dataPath);
        InitObjs();
        InitCourse();
    }

    SceneObj cam;
    private void InitObjs()
    {
        cam = objs.Find(p => p.Name.Equals("ViewCard") && p.model.GetComponent<Camera>() == Camera.main);
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i] != cam)
                objs[i].SetVisible(false);
        }
        cam.ui.SetActive(false);
    }

    /// <summary>
    /// 获取卡牌上的脚本组件
    /// </summary>
    /// <param name="type">组件类型</param>
    /// <param name="index">在数组中的下标</param>
    /// <returns></returns>
    public T GetCardComponent<T>(int index) where T : Component
    {
        T com = null;

        if (index < objs.Count)
        {
            com = objs[index].ui.GetComponent<T>();
            if (com == null && objs[index].model != null)
                com = objs[index].model.GetComponent<T>();
        }

        return com;
    }

    /// <summary>
    /// 初始化实验信息
    /// </summary>
    /// <param name="index">传入的单签实验下标（不传入下标为单个实验）</param>
    public void InitCourse(int index = -1)
    {
        if (index == -1)
            StartCoroutine(Init());
        else
            StartCoroutine(Init(index));
    }

    IEnumerator Init()
    {
       
        while (!Configuration.IsDone)
            yield return null;
        switch (infoType)
        {
            case InfoType.OPERATION:
                courseInfo = new OperationInfo();
                break;
            case InfoType.COUPE:
                courseInfo = new CoupeInfo();
                break;
            case InfoType.OBSERVE:
                courseInfo = new ObserveInfo();
                break;
            case InfoType.OTHER:
                break;
            default:
                break;
        }
        if (courseInfo != null)
            infos.Add(courseInfo);      
    }

    IEnumerator Init(int index)
    {
        while (!Configuration.IsDone)
            yield return null;
        switch (infoType)
        {
            case InfoType.OPERATION:
                courseInfo = new OperationInfo(index);
                break;
            case InfoType.COUPE:
                courseInfo = new CoupeInfo();
                break;
            case InfoType.OBSERVE:
                courseInfo = new ObserveInfo();
                break;
            case InfoType.OTHER:
                break;
            default:
                break;
        }
        if (courseInfo != null)
            infos.Add(courseInfo);
    }

    public void ResetObjs()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i] != cam)
                objs[i].SetVisible(false);
            if (objs[i].model != null && objs[i].model.GetComponent<ModelControl>() != null)
                objs[i].model.GetComponent<ModelControl>().StageReset();
            if (objs[i].ui.GetComponent<CardControl>() != null)
                objs[i].ui.GetComponent<CardControl>().StageReset();
        }
    }

    void JsonWrite()
    {
        try
        {
            jsonPath = Application.dataPath + jsonPath;
            if (!File.Exists(jsonPath))
            {
                FileStream fs = File.Create(jsonPath);
                fs.Close();
            }
            string json = JsonConvert.SerializeObject(infos);
            File.WriteAllText(jsonPath, json);

        } catch (Exception e)
        {
            Debugger.Log(e.ToString());
        }
    }

    void OnApplicationQuit()
    {
        //测试添加错误信息
        //courseInfo.AddMessageInErrors(1);
        //操作结束后，或者退出时写入课程时间以及课程进度
        if (courseInfo != null)
            courseInfo.Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        JsonWrite();
        courseInfo = null;
    }

}
