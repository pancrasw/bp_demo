using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//跟踪
public class Sheild : MonoBehaviour
{
    public float stayTime;
    public void Init(Transform mainCharacterTransform)
    {
        transform.SetParent(mainCharacterTransform);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);

        Game.GetInstance().mainCharacterController.isSheilding = true;
        Game.delayCall(() =>
        {
            Game.GetInstance().mainCharacterController.isSheilding = false;
            Destroy(gameObject);
        }, stayTime);
    }
}
