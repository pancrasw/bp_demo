using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    void Start()
    {
        Game game = Game.GetInstance();
        game.Init();
    }
}
