using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RoleConfigData
{
    RoleConfigItem[] data;
    public void load()
    {
        data = Game.getInstance().configManager.getConfigDataAry<RoleConfigItem>("Player.level");
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

class RoleConfigItem
{
    public int level;
    public int hp;
    public int speed;
}