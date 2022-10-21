using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//普通地块
public class BlockView : MonoBehaviour
{
    BoardView boardView;
    Vector2Int _coordinate;
    public Vector2Int coordinate
    {
        get { return _coordinate; }
        set
        {
            transform.localPosition = new Vector3(value.x * Game.BLOCK_WIDTH, -value.y * Game.BLOCK_HEIGHT, 10);
            _coordinate = value;
        }
    }
    bool _used;
    public GameObject normalGameObject;
    public GameObject usedGameObject;
    public bool used { get { return _used; } }
    public void Init(BoardView boardView)
    {
        this.boardView = boardView;
        transform.localScale = new Vector3(Game.BLOCK_WIDTH, Game.BLOCK_HEIGHT);
        setSelected(false);
    }

    //是否被使用
    public void setUsed(bool used)
    {
        if (used)
        {
            normalGameObject.SetActive(false);
            usedGameObject.SetActive(true);
        }
        else
        {
            normalGameObject.SetActive(true);
            usedGameObject.SetActive(false);
        }
        _used = used;
    }

    //是否被选中
    public void setSelected(bool selected)
    {
        SpriteRenderer spriteRenderer = normalGameObject.GetComponent<SpriteRenderer>();
        if (selected)
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);//调为半透明
        else
            spriteRenderer.color = new Color(1, 1, 1, 0);//透明
    }

    public void onUse()
    {
        if (_used) return;
        setUsed(true);
        boardView.boardController.onUse(this);
    }
}
