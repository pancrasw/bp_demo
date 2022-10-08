using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPath
{
    // 配置相对路径
    //public const string PROJECT_PREFIX = "C:/Users/LEGION/Documents/GitHub/bp_demo/";
    public static string PROJECT_PREFIX = System.Environment.CurrentDirectory;

    //json配置文件路径
    public static string CONFIG_PATH = PROJECT_PREFIX + "/Assets/Scripts/json";
}
