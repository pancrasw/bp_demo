using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bramble : MonoBehaviour
{
    public float damage;
    public float attackCD;
    public float stayTime;
    Timer attackCDTimer;
    BlockView block;
    public void Init(BlockView block)
    {
        this.block = block;
        checkCharacterCoordinate();

        callback coordinateListener = () =>
        {
            checkCharacterCoordinate();
        };
        Game.GetInstance().mainCharacterController.coordinateChange += coordinateListener;

        Game.delayCall(() =>
        {
            Game.GetInstance().mainCharacterController.coordinateChange -= coordinateListener;
            if (attackCDTimer != null)
            {
                Game.GetInstance().timerController.removeTimer(attackCDTimer);
                attackCDTimer = null;
            }
            Destroy(gameObject);
        }, stayTime);
    }

    void checkCharacterCoordinate()
    {
        RoleController mainCharacterController = Game.GetInstance().mainCharacterController;
        if (mainCharacterController.characterCoordinate.Equals(block.coordinate))
        {
            if (attackCDTimer == null)
            {
                mainCharacterController.ReduceBlood(damage);
                attackCDTimer = Game.delayCall(() =>
                {
                    attackCDTimer = null;
                    checkCharacterCoordinate();
                }, attackCD);
            }
        }
    }
}
