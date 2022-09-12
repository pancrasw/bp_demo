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
    BoardView boardView;//地块视图

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

    public void onUse(BlockView blockView)
    {
        BlockType blockType = getBlock(blockView.coordinate.x, blockView.coordinate.y);
        RoleController roleController = Game.GetInstance().mainCharacterController;
        switch (blockType)
        {
            case BlockType.Normal:
                break;
            case BlockType.Bleed:
                roleController.Bleed(1f, 10);//for test假数据
                break;
            case BlockType.Mine:
                roleController.ReduceBlood(Random.Range(0, 10) * 5);
                break;
            case BlockType.Chest:
                roleController.restoreBlood(20);//for test写死
                break;
            case BlockType.Key:
                GameObject keyGO = GameObject.Instantiate(boardView.keyPrefab);
                keyGO.transform.position = new Vector3(blockView.transform.position.x, blockView.transform.position.y, keyGO.transform.position.z);
                keyGO.GetComponent<Follower>().Init(roleController.GetRoleTransform());
                break;
        }
    }
}
