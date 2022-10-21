using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//控制角色相关的表现
public class RoleView : MonoBehaviour
{
    public RoleController roleController;
    public float speed { get { return roleController.speed; } }
    float hp { get { return roleController.roleState.hp; } }
    public BloodView bloodView;
    public bool freeze;//禁止移动
    public BlockView curBlock { get { return getCurBlock(); } }
    public CoronaView coronaView;
    public Button digBtn;
    bool _locked;
    public bool Locked
    {
        get { return _locked; }
        set
        {
            freeze = value;
            _locked = value;
        }
    }//禁止操作

    public void Init(RoleController roleController)
    {
        this.roleController = roleController;
        digBtn.onClick.AddListener(() =>
        {
            if (curBlock && !_locked)
            {
                curBlock.onUse();
            }
        });
    }

    public void onMove()
    {
        if (speed == 0) return;
        
        //键盘操作
        float moveX = Input.GetAxisRaw("Horizontal");//按D键为1，A键为-1
        float moveY = Input.GetAxisRaw("Vertical");//按W键为1，按S键为-1
        Vector3 position = transform.position;
        if (moveX * moveY == 0)//正方向移动
            position += (moveX * speed * Time.deltaTime * transform.right + moveY * speed * Time.deltaTime * transform.up);
        else//斜方向移动，位移乘根号2，保证移动速度不变
            position += (moveX * speed * Time.deltaTime * transform.right * Mathf.Sqrt(2) / 2 + moveY * speed * Time.deltaTime * transform.up * Mathf.Sqrt(2) / 2);
        transform.position = position;

        //轮盘操作
        Vector3 direction;
        if (coronaView.TryGetDirection(out direction))
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void Knockback(Vector3 force, float duration = 0.3f)
    {
        freeze = true;
        transform.DOMove(transform.position + force, duration);
        Game.delayCall(() => { freeze = false; }, duration);
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
        if (!freeze)
        {
            onMove();
        }

        if (curBlock != null)
        {
            if (curBlock != lastBlockView)//踩到了新的方块
            {
                if (lastBlockView != null)
                    lastBlockView.setSelected(false);
                curBlock.setSelected(true);
                lastBlockView = curBlock;
                roleController.OnCoordinateChange();
            }
            if (!_locked && Input.GetKey(KeyCode.Space))//空格挖地
            {
                curBlock.onUse();
            }
        }
        else if (lastBlockView != null)
            lastBlockView.setSelected(false);
    }
}
