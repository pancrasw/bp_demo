using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//一切场景切换逻辑
public class ScenesManager
{
    enum Scene
    {
        StartScene,
        GameScene,
    }

    public void Init() { }

    public void enterGame()
    {
        GameObject.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });
    }
}
