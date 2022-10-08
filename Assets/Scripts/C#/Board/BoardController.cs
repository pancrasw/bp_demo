using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    BoardConfigItem cur_board;//当前关卡配置
    int cur_episode = -1;//当前关卡序号
    BoardConfigData boardConfigData;
    public int width { get { return cur_board.width; } }
    public int height { get { return cur_board.length; } }

    public BlockType[,] grid;//数据
    public int[,] gridIsBenefit;//是否为好方块，1为正面方块，0为中立，-1为负面方块
    BoardView boardView;//地块视图
    public callback selectBloclChangeCallback;
    public BlockView selectedBlock;
    public delegate bool JudegeFunction(Vector2Int coordinate);
    public void Init()
    {
        InitConfigData();
        setEpisode(1);
    }

    public void InitConfigData()
    {
        boardConfigData = new BoardConfigData();
        boardConfigData.load();
    }

    //关卡切换
    public void setEpisode(int episode)
    {
        cur_board = boardConfigData.getBoardConfigItemByEpisode(episode);
        if (cur_board != null)//能取到关卡配置
            cur_episode = episode;
        if (boardView == null)
        {
            boardView = GameObject.Find("BoardView").GetComponent<BoardView>();
            boardView.Init(this);
        }
        randomizeAllBlock();
        boardView.refreshBoard();
    }

    private void randomizeAllBlock()
    {
        grid = new BlockType[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int type = Random.Range(0, Game.BLOCK_TYPE_COUNT);
                grid[x, y] = (BlockType)type;
            }
        }
    }

    public BlockType getBlock(int x, int y)
    {
        return grid[x, y];
    }

    //注册地块触发逻辑
    public void onUse(BlockView blockView)
    {
        BlockType blockType = getBlock(blockView.coordinate.x, blockView.coordinate.y);
        RoleController roleController = Game.GetInstance().mainCharacterController;
        switch (blockType)
        {
            case BlockType.Normal:
                break;
            case BlockType.Mine:
                roleController.ReduceBlood(Random.Range(0, 10) * 5);
                break;
            case BlockType.Chest:
                roleController.RestoreBlood(20);//for test写死
                break;
            case BlockType.Key:
                GameObject keyGO = GameObject.Instantiate(boardView.keyPrefab);
                keyGO.transform.position = new Vector3(blockView.transform.position.x, blockView.transform.position.y, keyGO.transform.position.z);
                keyGO.GetComponent<Follower>().Init(roleController.GetRoleTransform()).Play();
                break;
            // case BlockType.Zombie:
            //     GameObject zombieGO = GameObject.Instantiate(boardView.zombiePrefab);
            //     zombieGO.transform.position = new Vector3(blockView.transform.position.x, blockView.transform.position.y - Game.BLOCK_HEIGHT, zombieGO.transform.position.z);
            //     break;
            case BlockType.Firefly:
                GameObject fireflyGO = GameObject.Instantiate(boardView.fireflyPrefab);
                fireflyGO.GetComponent<Firefly>().Init(blockView);
                break;
            case BlockType.Sheild:
                GameObject sheildGO = GameObject.Instantiate(boardView.sheildPrefab);
                sheildGO.GetComponent<Sheild>().Init(roleController.GetRoleTransform());
                Game.GetInstance().messageController.popupBuffMesage("护盾", Color.blue);
                break;
            case BlockType.Bat:
                GameObject batGO = GameObject.Instantiate(boardView.batPrefab);
                batGO.GetComponent<Bat>().Init(blockView, roleController.GetRoleTransform());
                break;
        }
    }

    public bool isValidCoordinate(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && coordinate.x <= width && coordinate.y >= 0 && coordinate.y <= height;
    }

    bool defaultJudgeFunction(Vector2Int coordinate) { return true; }

    //搜索满足条件的一定距离的方块，由近到远排列
    public List<List<Vector2Int>> searchBlockInDistance(Vector2Int startCoordinate, int distance, JudegeFunction judegeFunction = null)
    {
        if (judegeFunction == null)
        {
            judegeFunction = (Vector2Int) => { return true; };//默认为返回所有距离内的格子
        }
        List<List<Vector2Int>> searchResult = new List<List<Vector2Int>>();
        searchResult.Add(new List<Vector2Int>());
        int searchDistance = 0;

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Queue<Vector2Int> nextQueue = new Queue<Vector2Int>();
        queue.Enqueue(startCoordinate);

        while (true)
        {
            if (queue.Count == 0)
            {
                searchDistance++;
                Utility.swap<Queue<Vector2Int>>(queue, nextQueue);

                if (nextQueue.Count == 0 || searchDistance > distance)//如果没有添加新的元素或者搜索距离超出限制
                    break;

                searchResult.Add(new List<Vector2Int>());
            }
            else
            {
                Vector2Int coordinate = queue.Dequeue();
                if (Game.GetInstance().boardController.isValidCoordinate(coordinate))
                {
                    if (judegeFunction(coordinate) && !searchResult[searchDistance].Contains(coordinate))
                    {
                        searchResult[searchDistance].Add(coordinate);
                    }

                    Vector2Int[] nextCoordinates ={
                    new Vector2Int(coordinate.x+1,coordinate.y),
                    new Vector2Int(coordinate.x-1,coordinate.y),
                    new Vector2Int(coordinate.x,coordinate.y+1),
                    new Vector2Int(coordinate.x,coordinate.y-1),};
                    foreach (Vector2Int nextCoordinate in nextCoordinates)
                    {
                        if (!nextQueue.Contains(nextCoordinate))
                        {
                            nextQueue.Enqueue(nextCoordinate);
                        }
                    }
                }
            }
        }
        return searchResult;
    }

    //返回世界坐标系坐标
    public Vector3 getBlockPosition(Vector2Int coordinate)
    {
        if (isValidCoordinate(coordinate))
        {
            Vector3 blockPosition = boardView.blockGOs[coordinate.y, coordinate.x].transform.position;
            return new Vector3(blockPosition.x, blockPosition.y, 0);
        }
        return Vector3.zero;
    }

    public BlockView GetBlockView(Vector2Int coordinate)
    {
        if (isValidCoordinate(coordinate))
        {
            return boardView.blockGOs[coordinate.y, coordinate.x].GetComponent<BlockView>();
        }
        return null;
    }
}
