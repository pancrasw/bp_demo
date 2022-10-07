using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//跟踪
public class Follower : MonoBehaviour
{
    Transform masterTransform;
    public float distance;
    public float speed;
    private bool isPlaying = false;
    public Follower Init(Transform masterTransform)
    {
        this.masterTransform = masterTransform;
        return this;
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
    }

    void Update()
    {
        if (isPlaying)
        {
            Vector3 curDistance = masterTransform.position - transform.position;
            if (curDistance.magnitude >= distance)
            {
                Vector3 direction = curDistance.normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
