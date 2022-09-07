using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色控制器，所有与角色相关的逻辑
public class RoleController
{
    public RoleState roleState;
    public float speed { get { return roleState.speed; } set { roleState.speed = value; } }
    Vector3 _forwardDirection;
    Vector3 forwardDirection { set { _forwardDirection = value; } get { return _forwardDirection; } }
    RoleView roleView;
    BloodView bloodView;
    RoleConfigData roleConfigData;
    public Vector3 characterPosition { get { return roleView.gameObject.transform.position; } }

    public void Init()
    {
        Debug.Log("RoleController Init.");
        roleState = new RoleState();
        roleView = GameObject.Find("Player").GetComponent<RoleView>();
        roleView.Init(this);
        roleConfigData = new RoleConfigData();
        roleConfigData.load();
        roleState.Init(1, roleConfigData);//for test
        bloodView = GameObject.Find("BloodBar").GetComponent<BloodView>();
        bloodView.Init(this);
    }

    //duration持续时间，以s为单位
    public void bleed(float hpPerSecond, int duration)
    {
        bloodView.bleed(hpPerSecond, duration);
    }

    public void reduceBlood(float damage)
    {
        bloodView.reduceBlood(damage);
    }

    public void restoreBlood(float hp)
    {

    }

    public int getHpLimit()
    {
        return roleConfigData.getRoleConfigItemByLevel(roleState.level).hp;//for test
    }

    public Transform getRoleTransform()
    {
        if (roleView != null)
        {
            return roleView.transform;
        }
        return null;
    }

    //加载存档
    public void loadSave(int id)
    {
        roleState.load(id);
    }

    //玩家死亡
    public void onDead()
    {
        
    }

    //存存档
    public void save(int id)
    {
        roleState.save(id);
    }
}
