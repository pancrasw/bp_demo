using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//漂浮，上下简谐运动
public class Flotage : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public bool isPlay;
    public float range;
    //周期
    public float period;
    public Vector3 direction;
    //保留开始时的位置作为中心点
    Vector3 startPosition;
    bool isTweening;
    float time;
    public void Init()
    {
        startPosition = transform.localPosition;
        direction = direction.normalized;
        Play();
    }

    public void Play()
    {
        isPlay = true;
    }

    public void Stop()
    {
        isPlay = false;
        transform.localPosition = startPosition;
    }

    private void updatePosition(float time)
    {
        transform.localPosition = startPosition + animationCurve.Evaluate(time % period / period) * range * direction;
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay && !isTweening)
        {
            updatePosition(time += Time.deltaTime);
        }
    }
}
