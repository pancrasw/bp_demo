using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleState
{
    public float speed;
    public float hp;
    Direction faceDirection;

    public void init()
    {
        speed = 5;
        hp = 0;
    }
}
