using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Normal,
    Used
}
public enum Direction
{
    UP, DOWN, LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT
}


public class Game
{
    public const string CONFIG_PATH = "C:/Users/LEGION/Documents/GitHub/bp_demo/Assets/Scripts/json";

    //游戏显示配置
    public const int BLOCK_TYPE_COUNT = 2;//地块种类数量
    public const float BLOCK_SIZE = 10;//地块尺寸
    public const float ANGLE = 60;//俯视角度，90度为垂直向下正投影


    public static readonly float ANGLE_FACTOR = Mathf.Sin(ANGLE);//计算后的俯视变形因子
    public static readonly float BLOCK_LENGTH = BLOCK_SIZE * ANGLE_FACTOR;
    public static readonly float BLOCK_WIDTH = BLOCK_SIZE;

    public event callback GameStart;
    public event callback Pause;
    public event callback Save;
    public event callback Load;
    public static Game game;
    public BuffManager buffManager;
    public ConfigManager configManager;

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
        mainCharacterController = GameObject.Find("Player").GetComponent<RoleController>();
        boardController = new BoardController();
        configManager = new ConfigManager(CONFIG_PATH);
    }

    public void init()
    {
        Debug.Log("game init!");
        mainCharacterController.init();
        initManager();
    }

    public void main()
    {
        
    }

    private void initManager()
    {
        configManager.init();
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