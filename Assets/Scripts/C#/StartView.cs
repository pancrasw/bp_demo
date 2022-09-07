using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//一切场景切换逻辑
public class StartView : MonoBehaviour
{
    public Button startButton;
    public GameObject savePrefab;
    public void Init()
    {
        startButton.onClick.AddListener(()=>
        {
            SceneManager.LoadScene("Game");
        });
    }

    void Start()
    {
        Init();
    }
}