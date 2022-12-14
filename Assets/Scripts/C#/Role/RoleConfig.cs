using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleConfigData
{
    RoleConfigItem[] data;
    public void load()
    {
        data = Game.GetInstance().configManager.GetConfigDataAry<RoleConfigItem>("Player.level");
    }

    public RoleConfigItem getRoleConfigItemByLevel(int level)
    {
        foreach (RoleConfigItem roleConfigItem in data)
        {
            if (roleConfigItem.level == level)
                return roleConfigItem;
        }
        return null;
    }
}

public class RoleConfigItem
{
    public int level;
    public int hp;//血量上限
    public float speed;
    public int energy;//体力值
}