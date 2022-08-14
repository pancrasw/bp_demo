using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class ConfigManager:Manager
{
    readonly string CONFIG_PATH;
    Dictionary<string,string> configRawData;

    public ConfigManager(string configPath)
    {
        CONFIG_PATH = configPath;
        configRawData = new Dictionary<string, string>();
    }

    public override void init()
    {
        loadAllConfig();
    }

    public void loadAllConfig()
    {
        DirectoryInfo dir = new DirectoryInfo(CONFIG_PATH);
        if (!dir.Exists)
        {
            Debug.Log("CONFIG_PATH dir doesn't exist!");
            return;
        }
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo fileInfo in files)
        {
            if (fileInfo.Name.EndsWith(".json"))
            {
                Debug.Log(fileInfo.Name);
                loadConfig(fileInfo.Name);
            }
        }
    }

    public void loadConfig(string name)
    {
        string json_str = File.ReadAllText(CONFIG_PATH + "/" + name);
        Debug.Log(json_str);
        configRawData.Add(name, json_str);
    }

    //获取指定配置数据
    public T[] getConfigDataAry<T>(string name)
    {
        string json_str;
        if (configRawData.TryGetValue(name + ".json", out json_str)) 
        {
            return JsonMapper.ToObject<T[]>(json_str);
        }
        return null;
    }
}