using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class UserData
{
    public string dataName = "UserData";

    protected abstract Dictionary<string, string> createSave();

    public void init()
    {
        Game.getInstance().Load += (int id)=> { load(id); };
        Game.getInstance().Save += (int id) => { save(id); };
    }

    public void save(int id)
    {
        Dictionary<string, string> save = createSave();
        foreach (string key in save.Keys)
        {
            PlayerPrefs.SetString(dataName + id.ToString() + key, save[key]);
        }
        PlayerPrefs.Save();
    }

    public void load(int id)
    {
        Dictionary<string, string> save = createSave();
        foreach (string key in save.Keys)
        {
            PlayerPrefs.GetString(dataName + id.ToString() + key, "");
        }
    }
}
