﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Normal,
    Bleed,//流血
    Mine,//地雷
    Chest,//宝箱
    Key,//钥匙
    Bomb,//炸弹
    Last,//空，用于计数
}

public enum Layer
{
    Block = -10,
    Role = 0,
}

public enum Direction
{
    UP, DOWN, LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT
}

public delegate void saveFunc(int id);

public class Game
{
    //游戏显示配置
    public const int BLOCK_TYPE_COUNT = (int)(BlockType.Last);//地块种类数量
    public const float BLOCK_SIZE = 1;//地块尺寸
    public const float ANGLE = 90;//俯视角度，90度为垂直向下正投影


    public static readonly float ANGLE_FACTOR = Mathf.Sin(ANGLE * Mathf.PI / 180);//计算后的俯视变形因子
    public static readonly float BLOCK_LENGTH = BLOCK_SIZE * ANGLE_FACTOR;
    public static readonly float BLOCK_WIDTH = BLOCK_SIZE;

    public event callback GameStart;
    public event callback Pause;

    public static Game game;

    public BuffManager buffManager;
    public ConfigManager configManager;
    public SettlementManager settlementManager;
    public SaveManager saveManager;

    public CameraController cameraController;
    public BoardController boardController;
    public RoleController mainCharacterController;
    public TimerController timerController;

    public static Game GetInstance()
    {
        if (game == null)
        {
            game = new Game();
        }
        return game;
    }

    private Game()
    {
        configManager = new ConfigManager();
        settlementManager = new SettlementManager();
        saveManager = new SaveManager();

        boardController = new BoardController();
        mainCharacterController = new RoleController();
    }

    public void Init()
    {
        Debug.Log("game init!");
        initManager();
        initController();
        main();
    }

    private void initManager()
    {
        configManager.Init();
        settlementManager.Init();
        saveManager.Init();
    }

    private void initController()
    {
        mainCharacterController.Init();
        boardController.Init();
        cameraController = GameObject.Find("MainCamera").GetComponent<CameraController>();
        cameraController.Init(mainCharacterController);
        timerController = GameObject.Find("Game").GetComponent<TimerController>();
        timerController.Init();
    }

    public static void delayCall(callback callback, float duration)
    {
        Game.GetInstance().timerController.addTimer(duration, () => { callback(); });
    }

    public void main()
    {

    }
}