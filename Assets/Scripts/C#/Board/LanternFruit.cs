using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//跟踪
public class LanternFruit : MonoBehaviour
{
    public float healPoint;
    public void Init(BlockView startBlock)
    {
        transform.position = Game.GetInstance().boardController.getBlockPosition(startBlock.coordinate);
        GetComponent<CircleCollider2D>().enabled = false;

        float animateDuration = 0.5f;
        transform.DOJump(transform.position, 5, 1, animateDuration);

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
        }
    }
}
