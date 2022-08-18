using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Normal,
    Bleed,//流血
    Mine,//地雷
    Chest,//宝箱
}
public enum Direction
{
    UP, DOWN, LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT
}

public delegate void saveFunc(int id);

public class Game
{
    //json配置文件路径
    public const string CONFIG_PATH = "D:/unity/bp_demo/Assets/Scripts/json";

    //游戏显示配置
    public const int BLOCK_TYPE_COUNT = 4;//地块种类数量
    public const float BLOCK_SIZE = 1;//地块尺寸
    public const float ANGLE = 90;//俯视角度，90度为垂直向下正投影


    public static readonly float ANGLE_FACTOR = Mathf.Sin(ANGLE * Mathf.PI / 180);//计算后的俯视变形因子
    public static readonly float BLOCK_LENGTH = BLOCK_SIZE * ANGLE_FACTOR;
    public static readonly float BLOCK_WIDTH = BLOCK_SIZE;

    public event callback GameStart;
    public event callback Pause;
    public event saveFunc Save;
    public event saveFunc Load;
    public static Game game;
    public BuffManager buffManager;
    public ConfigManager configManager;
    public SettlementManager settlementManager;

    public BoardController boardController;
    public RoleController mainCharacterController;

    public static Game getInstance()
    {
        if (game == null)
        {
            game = new Game();
        }
        return game;
    }

    private Game()
    {
        boardController = new BoardController();
        mainCharacterController = new RoleController();
        configManager = new ConfigManager(CONFIG_PATH);
        settlementManager = new SettlementManager();
    }

    public void init()
    {
        Debug.Log("game init!");
        initManager();
        mainCharacterController.init();
        boardController.init();
        
    }

    public void onLoad(int id)
    {
        Load(id);
    }

    public void onSave(int id)
    {
        Save(id);
    }

    public void main()
    {
        
    }

    private void initManager()
    {
        configManager.init();
        settlementManager.init();
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}