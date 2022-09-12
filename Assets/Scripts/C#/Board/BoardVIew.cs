using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    public BoardController boardController;
    BlockType[,] grid;
    //地块水平方向上的个数
    int width { get { return boardController.width; } }
    //地块垂直方向上的个数
    int height { get { return boardController.height; } }
    GameObject[,] blockGOs;
    GameObject[] airWallGOs;
    public GameObject blockPrefab;
    public GameObject keyPrefab;
    public GameObject bombPrefab;
    public GameObject airWallPrefab;
    public GameObject zombiePrefab;
    public void Init(BoardController boardController)
    {
        this.boardController = boardController;
        grid = boardController.grid;
        InitPrefabs();
        //调整地块尺寸
        transform.localScale = new Vector3(Game.BLOCK_WIDTH, Game.BLOCK_HEIGHT);
    }


    //添加绑定
    private void InitPrefabs()
    {
        blockPrefab = Resources.Load<GameObject>("Prefabs/NormalBlock");
    }

    //刷新关卡
    public void refreshBoard()
    {
        UpdateAllBlock(true);
        ClearAllAirWall();
        SetAirWall();
    }

    //刷新关卡
    public void UpdateAllBlock(bool clearBeforeUpdate)
    {
        if (clearBeforeUpdate)
        {
            ClearAllBlock();
            blockGOs = new GameObject[height, width];
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                BlockType type = boardController.getBlock(x, y);
                blockGOs[y, x] = Instantiate(blockPrefab);
                BlockView blockView = blockGOs[y, x].GetComponent<BlockView>();
                blockView.Init(this);
                blockView.transform.SetParent(transform);
                blockView.coordinate = new Vector2Int(x, y);

            }
        }
    }

    //地块整数坐标换算实际位置
    public Vector3 GetPosition(Vector2Int coordinate)
    {
        return new Vector3(coordinate.x * Game.BLOCK_WIDTH, -coordinate.y * Game.BLOCK_HEIGHT, 10);
    }

    public void ClearAllBlock()
    {
        if (blockGOs == null) return;
        foreach (GameObject gameObject in blockGOs)
        {
            Destroy(gameObject);
        }
        blockGOs = null;
    }

    public void SetAirWall()
    {
        float airWallWidth = width * Game.BLOCK_WIDTH;
        float airWallHeight = height * Game.BLOCK_HEIGHT;
        List<Vector2> coordinates = new List<Vector2>();
        coordinates.Add(new Vector2(-1, height / 2f - 0.5f));//左边
        coordinates.Add(new Vector2(width / 2f - 0.5f, -1));//上边
        coordinates.Add(new Vector2(width, height / 2f - 0.5f));//右边
        coordinates.Add(new Vector3(width / 2f - 0.5f, height));//下边
        airWallGOs = new GameObject[4];
        for (int i = 0; i < coordinates.Count; i++)
        {
            GameObject airWall = GameObject.Instantiate(airWallPrefab);
            airWall.transform.SetParent(transform);
            airWall.transform.localPosition = GetAirWallPosition(coordinates[i]);
            if (i % 2 == 0)//如果为左右边
                airWall.transform.localScale = new Vector3(1, airWallHeight, 1);
            else
                airWall.transform.localScale = new Vector3(airWallWidth, 1, 1);
            airWallGOs[i] = airWall;
        }
    }

    public void ClearAllAirWall()
    {
        if (airWallGOs == null)
            return;
        for (int i = 0; i < airWallGOs.Length; i++)
        {
            Destroy(airWallGOs[i]);
        }
        airWallGOs = null;
    }

    //
    public void onUse(BlockView blockView)
    {
        BlockType type = boardController.getBlock(blockView.coordinate.x, blockView.coordinate.y);

    }

    private Vector3 GetAirWallPosition(Vector2 coordinate)
    {
        return new Vector3(coordinate.x * Game.BLOCK_WIDTH, -coordinate.y * Game.BLOCK_HEIGHT, 0);
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
