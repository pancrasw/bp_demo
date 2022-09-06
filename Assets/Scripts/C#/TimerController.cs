using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    List<Timer> timers;
    List<SecondTimer> secondTimers;
    List<CompleteCallback> secondTimersCompeletCallback;
    public void init()
    {
        Debug.Log("TimerController init.");
        timers = new List<Timer>();
        Debug.Log(timers.Count);
        secondTimers = new List<SecondTimer>();
        secondTimersCompeletCallback = new List<CompleteCallback>();
    }
    public void addSecondTimer(int times, CallbackPerSecond callbackPerSecond, CompleteCallback completeCallback = null)
    {
        secondTimers.Add(new SecondTimer(times, callbackPerSecond));
        secondTimersCompeletCallback.Add(completeCallback);
    }
    public void addTimer(float durationMS, CompleteCallback completeCallback)
    {
        timers.Add(new Timer(durationMS, completeCallback));
        Debug.Log(timers.Count);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (secondTimers != null && secondTimers.Count > 0)//有计时器在计时
        {
            for (int i = secondTimers.Count - 1; i >= 0; i--)
            {
                if (!(secondTimers[i].updateTimer(Time.deltaTime)))
                {
                    if (secondTimersCompeletCallback[i] != null)
                        secondTimersCompeletCallback[i]();
                    secondTimersCompeletCallback.RemoveAt(i);
                    secondTimers.RemoveAt(i);
                }
            }
        }
        if (timers != null && timers.Count > 0)
        {
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                if (!(timers[i].updateTimer(Time.deltaTime)))
                {
                    timers.RemoveAt(i);
                }
            }
        }
    }
}
