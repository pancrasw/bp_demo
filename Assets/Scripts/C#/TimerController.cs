using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    List<SecondTimer> secondTimers;
    List<CompleteCallback> secondTimersCompeletCallback;
    public void init()
    {
        secondTimers = new List<SecondTimer>();
        secondTimersCompeletCallback = new List<CompleteCallback>();
    }
    public void addSecondTimer(int times, CallbackPerSecond callbackPerSecond, CompleteCallback completeCallback = null)
    {
        secondTimers.Add(new SecondTimer(times, callbackPerSecond));
        secondTimersCompeletCallback.Add(completeCallback);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (secondTimers != null && secondTimers.Count > 0)//有流血计时器在计时
        {
            for (int i = secondTimers.Count - 1; i >= 0; i--)
            {
                if (!secondTimers[i].updateTimer(Time.deltaTime))
                {
                    if (secondTimersCompeletCallback[i] != null)
                        secondTimersCompeletCallback[i]();
                    secondTimersCompeletCallback.RemoveAt(i);
                    secondTimers.RemoveAt(i);
                }
            }
        }
    }
}
