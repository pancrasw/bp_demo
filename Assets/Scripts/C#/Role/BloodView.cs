using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//角色血条
public class BloodView : MonoBehaviour
{
    RoleController roleController;
    public float hp { get { return roleController.roleState.hp; }set { roleController.roleState.hp = value; } }
    Image hpImage;//血量条
    Image limitImage;//血量上限条

    public void init(RoleController roleController)
    {
        this.roleController = roleController;
        Game.getInstance().Pause += () => { onPause(true); };
    }

    //设置hp
    public void setHp(float hp)
    {

    }

    //流血，参数为每秒掉血量
    public void bleed(float hpPerSecond)
    {

    }

    //停止流血
    public void stopBleed()
    { }

    //血量变化通知函数
    public void onHpChange()
    {
        
    }

    //暂定
    public void onPause(bool pause)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
