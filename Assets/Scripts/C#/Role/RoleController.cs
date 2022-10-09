using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色控制器，所有与角色相关的逻辑
public class RoleController
{
    public RoleState roleState;
    public float speed { get { return roleState.speed * totalFactor; } }
    public float natureBleedNum = 3;//自然留血量
    Vector3 _forwardDirection;
    Vector3 forwardDirection { set { _forwardDirection = value; } get { return _forwardDirection; } }

    public RoleView roleView;
    public BloodView bloodView;
    public DamageView damageView;
    public EnergyView energyView;

    RoleConfigData roleConfigData;
    public Vector3 characterPosition { get { return roleView.gameObject.transform.position; } }
    public Vector2Int characterCoordinate { get { return roleView.curBlock.coordinate; } }
    public callback coordinateChange;
    public bool isSheilding { get { return bloodView.isSheilding; } set { bloodView.isSheilding = value; } }

    public void Init()
    {
        Debug.Log("RoleController Init.");

        roleConfigData = new RoleConfigData();
        roleConfigData.load();


        roleState = new RoleState();
        roleState.Init(1, roleConfigData);

        roleView = GameObject.Find("Player").GetComponent<RoleView>();
        roleView.Init(this);

        bloodView = GameObject.Find("BloodBar").GetComponent<BloodView>();
        bloodView.Init(this);

        damageView = new DamageView();
        damageView.Init();

        energyView = GameObject.Find("EnergyBar").GetComponent<EnergyView>();
        energyView.Init(3);// 写死
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

    public void RestoreBlood(float hp)
    {
        bloodView.RestoreBlood(hp);
    }

    public void RestoreSpeed()
    {
        speedFactorList.Clear();
    }

    List<float> speedFactorList;//加速减速buff列表
    float totalFactor = 1;

    //返回注册的系数id
    public int registerSpeedFactor(float factor)
    {
        if (factor < 0) return -1;
        if (speedFactorList == null)
            speedFactorList = new List<float>();
        speedFactorList.Add(factor);
        totalFactor *= factor;
        return speedFactorList.Count - 1;
    }

    public void changeSpeedFactor(int id, float factor)
    {
        if (factor < 0 || id < 0 || id >= speedFactorList.Count) return;
        totalFactor *= factor /= speedFactorList[id];
        speedFactorList[id] = factor;
    }

    public void removeSpeedFactor(int id)
    {
        totalFactor /= speedFactorList[id];
        speedFactorList.RemoveAt(id);
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
        //人物翻倒
        roleView.transform.rotation = Quaternion.Euler(0, 0, 90);
        roleView.Locked = true;
        bloodView.stopBleed = true;
        Game.GetInstance().messageController.popupMessage("Game Over", MessageController.Type.Title, false);
    }

    //存存档
    public void Save(int id)
    {
        roleState.save(id);
    }

    public void OnCoordinateChange()
    {
        if (coordinateChange != null)
            coordinateChange();
    }

    public void ConsumeEnergy()
    {
        energyView.energy = energyView.energy - 1;
    }

    //自然体力流失
    public void natureBleed()
    {
        Bleed(natureBleedNum, 3600);
    }
}
