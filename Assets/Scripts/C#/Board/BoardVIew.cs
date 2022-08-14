using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView: MonoBehaviour
{
    BoardController boardController;
    int width { get { return boardController.width; } }
    int length { get { return boardController.length; } }
    GameObject[,] blockGOs;
    GameObject gridRoot;

    Dictionary<BlockType, GameObject> blockPrefabsMap;
    public void init(BoardController boardController)
    {
        this.boardController = boardController;
        initPrefabs();
    }


    //添加绑定
    private void initPrefabs()
    {
        blockPrefabsMap.Add(BlockType.Normal, Resources.Load<GameObject>(""));
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
                    blockGOs[y, x] = Instantiate(blockPrefabsMap[type]);
                    BlockView blockView = blockGOs[y, x].GetComponent<BlockView>();
                    blockView.coordinate = new Vector2Int(x, y);
                }
            }
        }
    }

    public Vector3 getPosition(Vector2Int coordinate)
    {
        Vector3 position = gridRoot.transform.position;
        return new Vector3(position.x + coordinate.x * Game.BLOCK_WIDTH, position.y + coordinate.y * Game.BLOCK_LENGTH);
    }

    public void clearAllBlock()
    {
        foreach (GameObject gameObject in blockGOs)
        {
            Destroy(gameObject);
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
