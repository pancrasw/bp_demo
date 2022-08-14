using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockView : MonoBehaviour
{
    BoardView boardView;
    Vector2Int _coordinate;
    public Vector2Int coordinate
    {
        get { return _coordinate; }
        set
        {
            transform.position = boardView.getPosition(value);
            _coordinate = value;
        }
    }
    bool _used;
    public GameObject normalPrefab;
    public GameObject usedPrefab;

    public void init(BoardView boardView)
    {
        this.boardView = boardView;
    }

    private void setUsed(bool used)
    {
        if (used)
        {
            normalPrefab.SetActive(false);
            usedPrefab.SetActive(true);
        }
        else
        {
            normalPrefab.SetActive(true);
            usedPrefab.SetActive(false);
        }
        _used = used;
    }

    public void onUse()
    {

        
    }
}
