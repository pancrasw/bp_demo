using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    BoardConfigItem cur_board;//当前关卡配置
    BoardConfigData boardConfigData;
    public int width { get { return cur_board.width; } }
    public int length { get { return cur_board.length; } }

    BlockType[,] grid;//数据
    BoardView boardView;//地块视图

    public void init()
    {
        boardConfigData = new BoardConfigData();
        boardConfigData.load();
        
    }

    //关卡切换
    public void setEpisode(int episode)
    {
        cur_board = boardConfigData.getBoardConfigItemByEpisode(episode);
        if (boardView == null)
            boardView = GameObject.Find("Board").GetComponent<BoardView>();
        randomizeAllBlock();
        boardView.updateAllBlock(true);
    }

    private void randomizeAllBlock()
    {
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
}
