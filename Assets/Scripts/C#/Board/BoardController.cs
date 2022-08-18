using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    BoardConfigItem cur_board;//当前关卡配置
    int cur_episode = -1;//当前关卡序号
    BoardConfigData boardConfigData;
    public int width { get { return cur_board.width; } }
    public int length { get { return cur_board.length; } }

    public BlockType[,] grid;//数据
    BoardView boardView;//地块视图

    public void init()
    {
        initConfigData();
        setEpisode(1);
    }

    public void initConfigData()
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
            boardView.init(this);
        }
        randomizeAllBlock();
        boardView.updateAllBlock(true);
    }

    private void randomizeAllBlock()
    {
        grid = new BlockType[width, length];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Random random = new Random();
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
        switch (blockType)
        {
            case BlockType.Normal:
                break;
            case BlockType.Bleed:
                Game.getInstance().mainCharacterController.bleed(0.5f, 10);//for test假数据
                break;
            case BlockType.Mine:
                Game.getInstance().mainCharacterController.reduceBlood(Random.Range(0, 10) * 3);
                break;
            case BlockType.Chest:
                Game.getInstance().mainCharacterController.restoreBlood(20);//for test写死
                break;
        }
    }
}
