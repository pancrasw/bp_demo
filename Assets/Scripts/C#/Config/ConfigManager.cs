using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class ConfigManager : Manager
{
    Dictionary<string, string> configRawData;

    public ConfigManager()
    {
        configRawData = new Dictionary<string, string>();
    }

    public override void Init() { }
    public void LoadConfig(string name)
    {
        string json_str = Resources.Load<TextAsset>("Config/json/" + name).text;
        Debug.Log(json_str);
        configRawData.Add(name, json_str);
    }

    //获取指定配置数据
    public T[] GetConfigDataAry<T>(string name)
    {
        string json_str;
        if (configRawData.TryGetValue(name, out json_str))
        {
            return JsonMapper.ToObject<T[]>(json_str);
        }
        else
        {
            LoadConfig(name);
            if (configRawData.TryGetValue(name, out json_str))
                return JsonMapper.ToObject<T[]>(json_str);
        }
        Debug.Log(name + ".json doesn't exist");
        return null;
    }
}