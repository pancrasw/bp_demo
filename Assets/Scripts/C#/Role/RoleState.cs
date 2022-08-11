using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleState
{
    public float speed;
    public float hp;
    Game.Direction faceDirection;

    public void init()
    {
        speed = 0;
        hp = 0;
    }
}
