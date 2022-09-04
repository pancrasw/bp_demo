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
            Debug.Log("hpLimit:" + roleController.getHpLimit().ToString());
            if (value > 0 && value <= roleController.getHpLimit())
            {
                Debug.Log(hp);
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
    List<SecondTimer> bleedTimers;//流血计时器，可叠加

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
        if (bleedTimers == null)
            bleedTimers = new List<SecondTimer>();
        bleedTimers.Add(new SecondTimer(duration, () => { reduceBlood(hpPerSecond); }));
    }

    public void reduceBlood(float damage)
    {
        hp = hp - damage;
    }

    public void restoreBlood(float hp)
    {
        this.hp += hp;
    }

    //停止流血
    public void stopBleed()
    {
        foreach (SecondTimer bleedTimer in bleedTimers)
        {
            bleedTimer.stopTimer();
        }
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
        if (bleedTimers != null && bleedTimers.Count > 0)//有流血计时器在计时
        {
            for (int i = bleedTimers.Count - 1; i >= 0; i--)
            {
                if (!bleedTimers[i].updateTimer(Time.deltaTime))
                {
                    bleedTimers.RemoveAt(i);
                }
            }
        }
    }
}
