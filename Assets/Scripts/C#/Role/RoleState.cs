using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色当前状态，用于存档
public class RoleState
{
    public float speed;
    public float hp;
    public float hpLimit;
    Direction faceDirection;
    public int level;

    public void init(int level, RoleConfigData roleConfigData)
    {
        this.level = level;
        Debug.Log("RoleState init.");
        speed = 5;
        hp = 100;
        hpLimit = roleConfigData.getRoleConfigItemByLevel(level).hpLimit;
    }

    //加载存档
    public void load(int id) { }

    //存存档
    public void save(int id) { }
}
