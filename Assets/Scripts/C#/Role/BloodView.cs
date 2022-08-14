using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodView : MonoBehaviour
{
    RoleController roleController;
    public float hp { get { return roleController.roleState.hp; } }

    public void init(RoleController roleController)
    {
        this.roleController = roleController;
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
