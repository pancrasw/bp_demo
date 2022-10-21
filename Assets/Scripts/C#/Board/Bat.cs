using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Creater
{
    public float forceFactor;//击退的力度
    public float firstDamage;//初始伤害
    public float damage;//伤害
    public float attackCD;//攻击后停顿
    public float stayTime;//停留时间
    bool isFirstAttack = true;
    Follower follower;
    Timer attackCDTimer;
    public void Init(BlockView startBlock, Transform roleTransform)
    {
        Init();

        transform.position = new Vector3(startBlock.transform.position.x, startBlock.transform.position.y, transform.position.z);

        follower = GetComponent<Follower>();
        follower.Init(roleTransform);
        follower.Play();

        Game.delayCall(() =>
        {
            if (attackCDTimer != null)
            {
                Game.GetInstance().timerController.removeTimer(attackCDTimer);
                attackCDTimer = null;
            }
            if (this != null)
                Destroy(gameObject);
        }, stayTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<RoleView>() != null)//撞到玩家
        {
            RoleView roleView = other.GetComponent<RoleView>();
            Vector3 force = Vector3.Normalize(roleView.transform.position - transform.position) * forceFactor;
            roleView.Knockback(force);
            if (isFirstAttack)
            {
                if (firstDamage != 0)
                    roleView.roleController.ReduceBlood(firstDamage);
                isFirstAttack = false;
            }
            else
            {
                roleView.roleController.ReduceBlood(damage);
            }


            follower.Stop();
            attackCDTimer = Game.delayCall(() =>
            {
                attackCDTimer = null;
                follower.Play();
            }, attackCD);
        }
    }
}
