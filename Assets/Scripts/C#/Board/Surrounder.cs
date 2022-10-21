using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrounder : MonoBehaviour
{
    public float roundSpeed;//环绕速度
    public float chaseSpeed;//追逐速度
    public float distance;//距离
    public bool freezeRotation;//是否冻结自身旋转
    bool isPlaying;//是否正在播放
    bool isChasing;
    bool isDepart;//正在远离
    public void Init(Transform mainCharactorViewTransform)
    {
        transform.SetParent(mainCharactorViewTransform);
        transform.localScale = new Vector3(1, 1, 1);
    }
    // Start is called before the first frame update

    public void Play()
    {
        isPlaying = true;
        if (Vector3.Magnitude(transform.localPosition) > distance)//离得太远
        {
            isChasing = true;
        }
        else//离得太近
            isDepart = true;
    }

    public void Stop()
    {
        isPlaying = false;
        isChasing = false;
        isDepart = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (isChasing)
            {
                if (Vector3.Magnitude(transform.localPosition) > distance)
                    transform.localPosition = transform.localPosition - Vector3.Normalize(transform.localPosition) * chaseSpeed;
                else
                {
                    isChasing = false;
                    transform.localPosition = Vector3.Normalize(transform.localPosition) * distance;
                }
            }
            else if (isDepart)
            {
                if (Vector3.Magnitude(transform.localPosition) < distance)
                    transform.localPosition = transform.localPosition + Vector3.Normalize(transform.localPosition) * chaseSpeed;
                else
                {
                    isDepart = false;
                    transform.localPosition = Vector3.Normalize(transform.localPosition) * distance;
                }
            }
            else
            {
                transform.RotateAround(transform.parent.transform.position, Vector3.forward, roundSpeed * Time.deltaTime);
                if (freezeRotation)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
            }

        }
    }
}
