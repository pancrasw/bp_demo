using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeConfigData
{
    //配置文件名字
    const string config_file_name = "Board.Episode";
    public EpisodeConfigItem[] data;

    public void load(int level)
    {
        data = Game.GetInstance().configManager.GetConfigDataAry<EpisodeConfigItem>(config_file_name + level.ToString());
    }

    //通过关卡id获取配置
    public EpisodeConfigItem getBlockConfigItemByBlockType(BlockType blockType)
    {
        foreach (EpisodeConfigItem episodeConfigItem in data)
        {
            if (Game.GetInstance().translation[blockType] == episodeConfigItem.blockType)
            {
                return episodeConfigItem;
            }
        }
        return null;
    }

    public float getTotalWeight()
    {
        float totalWeight = 0;
        foreach (EpisodeConfigItem episodeConfigItem in data)
        {
            totalWeight += episodeConfigItem.weight;
        }
        return totalWeight;
    }
}

public class EpisodeConfigItem
{
    public string blockType;//地块类型
    public int configID;//配置ID
    public float weight;//权重
    public float stayTime;
}
