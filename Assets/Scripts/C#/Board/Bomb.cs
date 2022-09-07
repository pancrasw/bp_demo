using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public float duration;
    public Color targetColor;
    CompleteCallback completeCallback;
    public void Init(float duration, CompleteCallback completeCallback)
    {
        if (duration != 0)
            this.duration = duration;
        this.completeCallback = completeCallback;
    }

    public void Play()
    {
        GetComponent<SpriteRenderer>().DOColor(targetColor, duration);
        Game.delayCall(() => { completeCallback(); }, duration);
    }
}
