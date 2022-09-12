using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    BoxCollider2D collider;
    RoleView mainCharactorView;
    public void Init(RoleView mainCharactorView)
    {
        this.mainCharactorView = mainCharactorView;
        collider = GetComponent<BoxCollider2D>();
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
