using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//跟踪
public class Follower : MonoBehaviour
{
    Transform masterTransform;
    public float distance;
    public float speed;
    public void Init(Transform masterTransform)
    {
        this.masterTransform = masterTransform;
    }

    void Update()
    {
        Vector3 curDistance = masterTransform.position - transform.position;
        if (curDistance.magnitude >= distance)
        {
            Vector3 direction = curDistance.normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
