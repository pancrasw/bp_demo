﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色控制器，所有与角色相关的逻辑
public class RoleController
{
    public RoleState roleState;
    public float speed { get { return roleState.speed; }set { roleState.speed = value; } }
    Vector3 _forwardDirection;
    Vector3 forwardDirection { set { _forwardDirection = value; }get { return _forwardDirection; } }
    RoleView roleView;
    RoleConfigData roleConfigData;


    public void init()
    {
        roleState = new RoleState();
        roleState.init();
        roleView = GameObject.Find("Player").GetComponent<RoleView>();
        roleView.init(this);
        roleConfigData = new RoleConfigData();
        roleConfigData.load();
        
    }

    //加载存档
    public void loadSave(int id)
    {
        roleState.load(id);
    }

    //存存档
    public void save(int id)
    {
        roleState.save(id);
    }
}
