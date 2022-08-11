using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum Direction
    {
        UP,DOWN,LEFT,RIGHT,UP_LEFT,UP_RIGHT,DOWN_LEFT,DOWN_RIGHT
    }
    RoleState roleState;
    public BuffManager buffManager;

    void init()
    {
        roleState = new RoleState();
        buffManager = new BuffManager(roleState);
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
