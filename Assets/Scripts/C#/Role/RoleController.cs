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
    public DamageView damageView;
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
        damageView = new DamageView();
        damageView.Init();
    }

    //duration持续时间，以s为单位
    public void Bleed(float hpPerSecond, int duration)
    {
        bloodView.Bleed(hpPerSecond, duration);
    }

    public void ReduceBlood(float damage)
    {
        bloodView.ReduceBlood(damage);
    }

    public void restoreBlood(float hp)
    {

    }

    public int GetHpLimit()
    {
        return roleConfigData.getRoleConfigItemByLevel(roleState.level).hp;//for test
    }

    public Transform GetRoleTransform()
    {
        if (roleView != null)
        {
            return roleView.transform;
        }
        return null;
    }

    //加载存档
    public void LoadSave(int id)
    {
        roleState.load(id);
    }

    //玩家死亡
    public void OnDead()
    {
        
    }

    //存存档
    public void Save(int id)
    {
        roleState.save(id);
    }
}
