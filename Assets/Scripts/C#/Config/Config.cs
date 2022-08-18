using UnityEngine;

public abstract class ConfigItem
{
    public abstract string getMainKey();
}

//≈‰÷√Œƒº˛»›∆˜¿‡
public class ConfigItemAry
{
    ConfigItem[] configs;
    public ConfigItemAry(ConfigItem[] configItems)
    {
        configs = configItems;
    }

    public ConfigItem getByMainKey(string mainKey)
    {
        foreach (ConfigItem config in configs)
        {
            if (config.getMainKey() == mainKey)
            {
                return config;
            }
        }
        return null;
    }
}