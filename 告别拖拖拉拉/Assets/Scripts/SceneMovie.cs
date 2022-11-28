
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneMovie : MonoBehaviour
{
    //private AsyncOperation _asyncOperation;
    public GameObject _Video;
    private VideoPlayer player;
    float time;
    private void Awake()
    {
        player = _Video.GetComponent<VideoPlayer>();
        time = (float)player.clip.length;
    }
    // Use this for initialization
   IEnumerator Start()
    {
        yield return new WaitForSeconds(time);       
        SceneManager.LoadSceneAsync(2);
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
  

}
