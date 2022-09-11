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
            transform.localPosition = boardView.GetPosition(value);
            _coordinate = value;
        }
    }
    bool _used;
    public GameObject normalGameObject;
    public GameObject usedGameObject;

    public void Init(BoardView boardView)
    {
        this.boardView = boardView;
        transform.localScale = boardView.gameObject.transform.localScale;
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
            spriteRenderer.color = new Color(0, 255, 255);//调为绿色
        else
            spriteRenderer.color = new Color(255, 255, 255);//原色
    }

    public void onUse()
    {
        setUsed(true);
        boardView.boardController.onUse(this);
    }
}
