using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//角色血条
public class BloodView : MonoBehaviour
{
    RoleController roleController;
    public float hp
    {
        get { return roleController.roleState.hp; }
        set
        {
            if (value > 0 && value <= roleController.getHpLimit())
            {
                hpText.text = value.ToString();
                roleController.roleState.hp = value;
            }
            else if (value <= 0)
            {
                roleController.roleState.hp = 0;
                roleController.onDead();
            }
            else
                hp = hpLimit;
        }
    }
    public float hpLimit
    {
        get { return roleController.roleState.hpLimit; }
        set { roleController.roleState.hpLimit = value; }
    }
    Image hpImage;//血量条
    Image limitImage;//血量上限条
    public Text hpText;
    public Text hpLimitTex;
    public bool stopBleed;//停止流血开关
    int bleedTimerCount;

    public void init(RoleController roleController)
    {
        Debug.Log("BloodView init.");
        this.roleController = roleController;
        Game.getInstance().Pause += () => { onPause(true); };
        Debug.Log("hp:" + hp.ToString());
        hp = hp;//刷新血量文本
    }

    //流血，参数为每秒掉血量
    public void bleed(float hpPerSecond, int duration)
    {
        Game.getInstance().timerController.addSecondTimer(duration, () =>
        {
            reduceBlood(hpPerSecond);
            if (stopBleed)
            {
                bleedTimerCount--;
                if (bleedTimerCount == 0)//已清空则暂停停止流血
                    stopBleed = false;
                return false;
            }
            return true;
        });
    }

    public void reduceBlood(float damage)
    {
        hp = hp - damage;
        Game.getInstance().damageController.createDamageText(roleController.getRoleTransform(), damage);
    }

    public void restoreBlood(float hp)
    {
        this.hp += hp;
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
