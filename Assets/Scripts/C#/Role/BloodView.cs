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
            if (value > 0 && value <= roleController.GetHpLimit())
            {
                SetHP(value);
            }
            else if (value <= 0)
            {
                SetHP(0);
                roleController.OnDead();
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
    public Image hpImage;//血量条
    public Image limitImage;//血量上限条
    public Text hpText;
    public Text hpLimitTex;
    public bool stopBleed;//停止流血开关
    public bool isSheilding;//是否处在护盾无敌状态
    int bleedTimerCount;

    public void Init(RoleController roleController)
    {
        Debug.Log("BloodView Init.");
        this.roleController = roleController;
        Game.GetInstance().Pause += () => { onPause(true); };
        SetHP(hp);
    }

    //流血，参数为每秒掉血量
    public void Bleed(float hpPerSecond, int duration)
    {
        Game.GetInstance().timerController.addSecondTimer(duration, () =>
        {
            if (Game.GetInstance().isPause) return true;
            ReduceBlood(hpPerSecond);
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

    public void ReduceBlood(float damage)
    {
        if (isSheilding)//无敌状态不掉血
        {
            roleController.damageView.CreateDamageText(roleController.GetRoleTransform(), 0);
            return;
        }
        roleController.damageView.CreateDamageText(roleController.GetRoleTransform(), damage);
        hp = hp - damage;
    }

    public void RestoreBlood(float hp)
    {
        this.hp += hp;
        roleController.damageView.CreateHealText(roleController.GetRoleTransform(), hp);
    }

    //设置文本和血条
    private void SetHP(float hp)
    {
        hpText.text = hp.ToString();
        roleController.roleState.hp = hp;
        hpImage.transform.localScale = new Vector3(hp / hpLimit, 1, 1);
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
