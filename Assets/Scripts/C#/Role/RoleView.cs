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
    BloodView bloodView;

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

    //获取当前脚底下的Block
    public BlockView getCurBlock()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            return hitInfo.collider.gameObject.transform.parent.GetComponent<BlockView>();
        }
        return null;
    }

    BlockView lastBlockView = null;

    void Update()
    {
        onMove();
        BlockView curBlock = getCurBlock();
        if (curBlock != null)
        {
            if (curBlock != lastBlockView)
            {
                if (lastBlockView != null)
                    lastBlockView.setSelected(false);
                curBlock.setSelected(true);
                lastBlockView = curBlock;
            }
            if (Input.GetKey(KeyCode.Space))//空格挖地
            {
                curBlock.onUse();
            }
        }
        else if (lastBlockView != null)
            lastBlockView.setSelected(false);
    }


}
