using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CompleteCallback();

public class Timer
{
    float endTime;
    float count;
    CompleteCallback completeCallback;
    public Timer(float durationMS, CompleteCallback completeCallback)
    {
        endTime = durationMS;
        this.completeCallback = completeCallback;
        count = 0;
    }

    public bool updateTimer(float deltaTime)
    {
        count += deltaTime;
        if (count >= endTime)
        {
            completeCallback();
            return false;
        }
        return true;
    }
}

//每秒调用一次函数，返回值的布尔值表示是否继续进行
public delegate bool CallbackPerSecond();

//每秒一跳
//times跳的次数
//返回值：是否计时完毕
public class SecondTimer
{
    int times = -1;
    CallbackPerSecond callback;
    float startTime = 0;
    public SecondTimer(int times, CallbackPerSecond func)
    {
        this.times = times;
        callback = func;
    }

    public SecondTimer(CallbackPerSecond func)
    {
        callback = func;
    }

    //返回true表示依然在运行
    public bool updateTimer(float deltaTime)
    {
        if (times == 0) return false;
        startTime += deltaTime;
        if (startTime > 1)
        {
            startTime -= 1;
            times--;
            if (!callback())
            {
                times = 0;
                return false;
            }
        }
        return true;
    }
}
