using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地块结算函数书写，只负责传递结算信息，不负责表现
//暂时弃用
public class SettlementManager : MonoBehaviour
{
    RoleController roleController;

    public void Init()
    {
        roleController = Game.GetInstance().mainCharacterController;
    }

}
