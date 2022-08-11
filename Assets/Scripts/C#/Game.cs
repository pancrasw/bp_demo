using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public event callback GameStart;
    public event callback Pause;
    public event callback Save;
    public event callback Load;
    public static Game game;
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT
    }
    RoleState roleState;
    public BuffManager buffManager;

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
        roleState = new RoleState();
        buffManager = new BuffManager(roleState);
    }

    public void init()
    {
        roleState.init();
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