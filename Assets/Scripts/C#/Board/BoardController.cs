using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//地块排布和关卡选择
public class BoardController
{
    Vector3[] place = { new Vector3(6.7f, 4.68f, 10f), new Vector3(6.85f, -12.21f, 10f) };

    BoardConfigItem cur_board;//当前关卡配置
    BoardConfigData boardConfigData;

    public int curEpisodeID { get { return _cur_episode; } }
    int _cur_episode = -1;//当前关卡序号
    EpisodeConfigData episodeConfigData;

    public int width { get { return cur_board.width; } }
    public int height { get { return cur_board.length; } }

    public BlockType[,] grid;//数据
    public int[,] gridIsBenefit;//是否为好方块，1为正面方块，0为中立，-1为负面方块
    BoardView boardView;//地块视图

    public callback selectBloclChangeCallback;
    public BlockView selectedBlock;

    int keyCount = 0;//胜利条件

    public delegate bool JudegeFunction(Vector2Int coordinate);
    public void Init()
    {
        episodeConfigData = new EpisodeConfigData();

        InitConfigData();
        setEpisode(1);
    }

    public void InitConfigData()
    {
        boardConfigData = new BoardConfigData();
        boardConfigData.load();
    }

    //关卡切换
    void setEpisode(int episode)
    {
        episodeConfigData.load(episode);

        cur_board = boardConfigData.getBoardConfigItemByEpisode(episode);
        if (cur_board != null)//能取到关卡配置
            _cur_episode = episode;
        if (boardView == null)
        {
            boardView = GameObject.Find("BoardView").GetComponent<BoardView>();
            boardView.Init(this);
        }

        randomizeAllBlock();

        boardView.refreshBoard();

        GameObject.Find("BackGround").transform.position = place[episode - 1];
    }

    //随机算法
    private void randomizeAllBlock()
    {
        grid = new BlockType[width, height];

        float totalWeight = episodeConfigData.getTotalWeight();
        int blockTotalCount = width * height;
        List<BlockType> randomBlockList = new List<BlockType>();
        int blockCount;
        foreach (EpisodeConfigItem episodeConfigItem in episodeConfigData.data)
        {
            if (episodeConfigItem.fixedCount != 0)
                blockCount = episodeConfigItem.fixedCount;
            else
                blockCount = (int)(episodeConfigItem.weight * blockTotalCount / totalWeight);
            for (int i = 0; i < blockCount; i++)
            {
                randomBlockList.Add(episodeConfigItem.blockTypeEnum);
            }
        }

        if (randomBlockList.Count < blockTotalCount)//如果还有剩余方块未生成，则按概率随机生成
        {
            int remainBlockCount = blockTotalCount - randomBlockList.Count;
            for (int i = 0; i < remainBlockCount; i++)
            {
                float randInt = Random.Range(0, totalWeight);
                float tempWeight = 0;
                for (int j = 0; j < episodeConfigData.data.Length; j++)
                {
                    tempWeight += episodeConfigData.data[j].weight;
                    if (tempWeight >= randInt)
                    {
                        randomBlockList.Add(episodeConfigData.data[j].blockTypeEnum);
                        break;
                    }
                }
            }
        }

        Utility.shuffle(randomBlockList);

        //填入grid;
        for (int x = 0, i = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = randomBlockList[i];
                i++;
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
            // case BlockType.Mine:
            //     roleController.ReduceBlood(Random.Range(0, 10) * 5);
            //     break;
            case BlockType.Chest:
                roleController.RestoreBlood(20);//for test写死
                break;
            case BlockType.Key:
                GameObject keyGO = GameObject.Instantiate(boardView.keyPrefab);
                keyGO.transform.position = new Vector3(blockView.transform.position.x, blockView.transform.position.y, keyGO.transform.position.z);
                keyGO.GetComponent<Follower>().Init(roleController.GetRoleTransform()).Play();
                keyCount++;
                if (keyCount == 3)//胜利
                {
                    keyCount = 0;
                    onWin();
                }
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
            case BlockType.LanternFruit:
                GameObject lanternFruitGO = GameObject.Instantiate(boardView.lanterFruitPrefab);
                lanternFruitGO.GetComponent<LanternFruit>().Init(blockView);
                break;
            case BlockType.Marsh:
                GameObject marshGO = GameObject.Instantiate(boardView.marshPrefab);
                marshGO.GetComponent<Marsh>().Init(blockView);
                break;
        }
    }

    public bool isValidCoordinate(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < width && coordinate.y >= 0 && coordinate.y < height;
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

                Queue<Vector2Int> temp = nextQueue;//此处不知为何用Utility.swap交换无效
                nextQueue = queue;
                queue = temp;

                if (searchDistance > distance)//如果没有添加新的元素或者搜索距离超出限制
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
                    new Vector2Int(coordinate.x,coordinate.y-1)};
                    foreach (Vector2Int nextCoordinate in nextCoordinates)
                    {
                        if (!nextQueue.Contains(nextCoordinate) && isValidCoordinate(nextCoordinate))
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
            return new Vector3(blockPosition.x, blockPosition.y, Game.NORMAL_LAYER);
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

    void onWin()
    {
        Game.delayCall(() =>
        {
            changeEpisode(_cur_episode + 1);
        }, 1);
    }

    //切场景，包含动画
    public void changeEpisode(int episode)
    {
        float episodeChangeDuration = 1.5f;

        Image mask = GameObject.Find("Mask").GetComponent<Image>();
        mask.DOFade(1, episodeChangeDuration);

        Game.GetInstance().OnPause();

        //角色上锁
        Game.GetInstance().mainCharacterController.roleView.Locked = true;
        //消耗体力
        Game.GetInstance().mainCharacterController.ConsumeEnergy();

        Game.delayCall(() =>
        {
            Game.GetInstance().onPass();
            setEpisode(episode);

            Game.delayCall(() =>
                {
                    Game.GetInstance().OnContinue();
                    mask.DOFade(0, episodeChangeDuration);
                    //角色解锁
                    Game.GetInstance().mainCharacterController.roleView.Locked = false;
                }, episodeChangeDuration);
        }, episodeChangeDuration);
    }
}
