using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色当前状态，用于存档
public class RoleState
{
    public float speed;
    public float hp;
    Direction faceDirection;
    public int leve;

    public void init()
    {
        speed = 5;
        hp = 0;
    }

    //加载存档
    public void load(int id) { }

    //存存档
    public void save(int id) { }
}
