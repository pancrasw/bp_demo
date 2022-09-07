using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class SaveManager
{
    public int currentSaveID;

    public void Init()
    {

    }

    public T Load<T>(T t, string name = "")
    {
        string json = PlayerPrefs.GetString(t.GetType().ToString() + "___" + currentSaveID.ToString() + name, "");
        return JsonMapper.ToObject<T>(json);
    }

    public void Save<T>(T t, string name = "")
    {
        PlayerPrefs.SetString(t.GetType().ToString() + currentSaveID.ToString() + name, JsonMapper.ToJson(this));
    }
}
