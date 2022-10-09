using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfigData
{
    //配置文件名字
    const string config_file_name = "Board.Board";
    ConfigItemAry boardConfigItems;
    
    public void load()
    {
        BoardConfigItem[] data = Game.GetInstance().configManager.GetConfigDataAry<BoardConfigItem>(config_file_name);
        if (data != null)
        {
            boardConfigItems = new ConfigItemAry(data);
        }
    }

    //通过关卡id获取配置
    public BoardConfigItem getBoardConfigItemByEpisode(int episode)
    {
        return (BoardConfigItem)boardConfigItems.getByMainKey(episode.ToString());
    }
}

public class BoardConfigItem : ConfigItem
{
    public int episode;//关卡数，主键
    public int length;//长度
    public int width;//宽度

    public override string getMainKey()
    {
        return episode.ToString();
    }
}
