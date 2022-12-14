using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色当前状态，用于存档
public class RoleState
{
    public float speed;
    public float hp;
    public float hpLimit;

    public int energy;
    Direction faceDirection;
    public int level;

    public void Init(int level, RoleConfigData roleConfigData)
    {
        this.level = level;
        Debug.Log("RoleState Init.");
        speed = roleConfigData.getRoleConfigItemByLevel(level).speed;
        hpLimit = roleConfigData.getRoleConfigItemByLevel(level).hp;
        energy = roleConfigData.getRoleConfigItemByLevel(level).energy;
        hp = hpLimit;
    }

    //加载存档
    public void load(int id) { }

    //存存档
    public void save(int id) { }
}
