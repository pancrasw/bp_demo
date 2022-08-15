using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView: MonoBehaviour
{
    BoardController boardController;
    BlockType[,] grid;
    int width { get { return boardController.width; } }
    int length { get { return boardController.length; } }
    GameObject[,] blockGOs;
    GameObject blockPrefab;
    public void init(BoardController boardController)
    {
        this.boardController = boardController;
        grid = boardController.grid;
        initPrefabs();
        //调整地块尺寸
        transform.localScale = new Vector3(Game.BLOCK_WIDTH, Game.BLOCK_LENGTH);
    }


    //添加绑定
    private void initPrefabs()
    {
        blockPrefab = Resources.Load<GameObject>("Prefabs/NormalBlock");
    }

    //刷新关卡
    public void updateAllBlock(bool clearBeforeUpdate)
    {
        if (clearBeforeUpdate)
        {
            clearAllBlock();
            blockGOs = new GameObject[length, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    BlockType type = boardController.getBlock(x, y);
                    blockGOs[y, x] = Instantiate(blockPrefab);
                    BlockView blockView = blockGOs[y, x].GetComponent<BlockView>();
                    blockView.init(this);
                    blockView.transform.SetParent(transform);
                    blockView.coordinate = new Vector2Int(x, y);
                    
                }
            }
        }
    }

    //获取地块坐标
    public Vector3 getPosition(Vector2Int coordinate)
    {
        return new Vector3(coordinate.x * Game.BLOCK_WIDTH, -coordinate.y * Game.BLOCK_LENGTH, 10);
    }

    public void clearAllBlock()
    {
        if (blockGOs == null) return;
        foreach (GameObject gameObject in blockGOs)
        {
            Destroy(gameObject);
        }
    }

    //
    public void onUse(BlockView blockView)
    {
        BlockType type = boardController.getBlock(blockView.coordinate.x, blockView.coordinate.y);
        switch (type)
        {
            case BlockType.Bleed:
                
        }
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
