using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float forceScale;//击退角色的力度大小
    public float damage;//给角色造成的伤害
    RoleView mainCharactorView;
    public void Init(RoleView mainCharactorView, float speed)
    {
        this.mainCharactorView = mainCharactorView;
        GetComponent<Follower>().speed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<RoleView>() != null && other.GetComponent<RoleView>() == mainCharactorView)//撞到玩家
        {
            Vector3 force = (mainCharactorView.transform.position - transform.position).normalized * forceScale;
            mainCharactorView.Knockback(force);
            mainCharactorView.bloodView.RestoreBlood(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
