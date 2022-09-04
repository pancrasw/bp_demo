using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    RoleController roleController;

    public void init(RoleController followRoleController)
    {
        roleController = followRoleController;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (roleController != null)
        {
            Vector3 followPosition = roleController.characterPosition;
            gameObject.transform.position = new Vector3(followPosition.x, followPosition.y, -10);
        }

    }
}
