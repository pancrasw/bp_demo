using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float endTime;
    public Timer(float milisecond)
    {

    }
}

//每秒调用一次函数
public delegate void CallbackPerSecond();

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

    public bool updateTimer(float deltaTime)
    {
        if (times == 0) return false;
        startTime += deltaTime;
        if (startTime > 1)
        {
            startTime -= 1;
            times--;
            callback();
        }
        return true;
    }

    public void stopTimer()
    {
        times = 0;
    }
}
