using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    PriorityHeap<Timer> timerHeap;
    List<SecondTimer> secondTimers;
    List<CompleteCallback> secondTimersCompeletCallback;
    Text gameTimeText;
    public void Init()
    {
        Debug.Log("TimerController init.");
        timerHeap = new PriorityHeap<Timer>((Timer a, Timer b) =>
        {
            return a.getEndTimeStamp() < b.getEndTimeStamp();
        });
        secondTimers = new List<SecondTimer>();
        secondTimersCompeletCallback = new List<CompleteCallback>();
        gameTimeText = GameObject.Find("Time").GetComponent<Text>();
    }
    public void addSecondTimer(int times, CallbackPerSecond callbackPerSecond, CompleteCallback completeCallback = null)
    {
        secondTimers.Add(new SecondTimer(times, callbackPerSecond));
        secondTimersCompeletCallback.Add(completeCallback);
    }
    public void addTimer(float durationMS, CompleteCallback completeCallback)
    {
        timerHeap.Add(new Timer(durationMS, completeCallback));
        Debug.Log(timerHeap.Count);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void updateSecondTimer()
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
    }

    private void updateTimer()
    {
        while (timerHeap != null && timerHeap.Count > 0 && Time.time >= timerHeap.getTop().getEndTimeStamp())
        {
            Timer timer = timerHeap.popup();
            timer.complete();
        }
    }

    void Update()
    {
        updateSecondTimer();
        updateTimer();
        gameTimeText.text = ((int)(Time.unscaledTime)).ToString();
    }
}
