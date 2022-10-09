using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    callback passCallback;
    callback pauseCallback;
    callback continueCallback;

    protected bool isPause = false;

    protected virtual void Init()
    {
        passCallback = () =>
        {
            if (gameObject != null)
                Destroy(gameObject);
        };
        Game.GetInstance().Pass += passCallback;

        pauseCallback = () =>
        {
            isPause = true;
        };
        Game.GetInstance().Pause += pauseCallback;

        continueCallback = () =>
        {
            isPause = false;
        };
        Game.GetInstance().Continue += continueCallback;
    }

    void OnDestroy()
    {
        Game.GetInstance().Pass -= passCallback;
        Game.GetInstance().Pause -= pauseCallback;
        Game.GetInstance().Continue -= continueCallback;
    }
}
