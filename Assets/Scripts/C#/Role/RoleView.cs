using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//控制角色相关的表现
public class RoleView : MonoBehaviour
{
    RoleController roleController;
    float speed { get { return roleController.roleState.speed; } }
    float hp { get { return roleController.roleState.hp; } }

    GameObject blood;

    public void init(RoleController roleController)
    {
        this.roleController = roleController;
    }

    public void onMove()
    {
        float moveX = Input.GetAxisRaw("Horizontal");//按D键为1，A键为-1
        float moveY = Input.GetAxisRaw("Vertical");//按W键为1，按S键为-1
        Vector3 position = transform.position;
        if (moveX * moveY == 0)//正方向移动
            position += (moveX * speed * Time.deltaTime * transform.right + moveY * speed * Time.deltaTime * transform.up);
        else//斜方向移动，位移乘根号2，保证移动速度不变
            position += (moveX * speed * Time.deltaTime * transform.right * Mathf.Sqrt(2) / 2 + moveY * speed * Time.deltaTime * transform.up * Mathf.Sqrt(2) / 2);
        transform.position = position;
    }

    public void updateHP()
    {
        blood.
    }

    // Update is called once per frame
    void Update()
    {
        onMove();
    }


}
