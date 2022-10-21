using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//跟踪
public class LanternFruit : Creater
{
    public float healPoint;
    public float jumpForce;
    public float animateDuration;
    public void Init(BlockView startBlock)
    {
        Init();
        transform.position = Game.GetInstance().boardController.getBlockPosition(startBlock.coordinate);
        GetComponent<CircleCollider2D>().enabled = false;

        transform.DOJump(transform.position, jumpForce, 1, animateDuration);

        Game.delayCall(() =>
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }, animateDuration);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<RoleView>() != null)//撞到玩家
        {
            RoleView roleView = other.GetComponent<RoleView>();
            Game.GetInstance().mainCharacterController.RestoreBlood(healPoint);
            if (this != null)
                Destroy(gameObject);
        }
    }
}
