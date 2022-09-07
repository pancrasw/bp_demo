using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//一切场景切换逻辑
public class SceneManager
{
    enum Scene
    {
        StartScene,
        GameScene,
    }

    public void init() { }

    public void enterGame()
    {
        GameObject.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            
        });
    }
}
