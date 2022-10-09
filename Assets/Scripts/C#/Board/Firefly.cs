using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//跟踪
public class Firefly : MonoBehaviour
{
    public int checkDistance;//距离格子数
    public int stayTime;//存在时间
    Surrounder surrounder;
    callback blockChangeCallback;
    bool glow;//是否发光
    public void Init(BlockView startBlock)
    {
        //调整初始位置
        transform.position = new Vector3(startBlock.transform.position.x, startBlock.transform.position.y, Game.NORMAL_LAYER);

        surrounder = GetComponent<Surrounder>();
        surrounder.Init(Game.GetInstance().mainCharacterController.GetRoleTransform());
        surrounder.Play();

        RoleController mainCharacterController = Game.GetInstance().mainCharacterController;

        BoardController.JudegeFunction judegeFunction = (coordinate) =>
        {
            BlockType blockType = Game.GetInstance().boardController.getBlock(coordinate.x, coordinate.y);
            if (Game.GetInstance().blockBiasMap.ContainsKey(blockType) && Game.GetInstance().blockBiasMap[blockType] == BlockBias.Benifit)//如果是好方块
                return true;
            return false;
        };

        blockChangeCallback = () =>
        {
            var matrix = Game.GetInstance().boardController.searchBlockInDistance(mainCharacterController.characterCoordinate, checkDistance, judegeFunction);
            for (int distance = 0; distance < matrix.Count; distance++)
            {
                if (matrix[distance].Count != 0)//有满足条件的地块
                {
                    glow = true;
                    return;
                }
            }
            glow = false;
        };
        mainCharacterController.coordinateChange += blockChangeCallback;

        Game.delayCall(() =>
        {
            mainCharacterController.coordinateChange -= blockChangeCallback;
            Destroy(gameObject);
        }, stayTime);
    }

    void Update()
    {

    }
}

public class FireflyConfigData
{
    public struct FireflyConfigItem
    {
        int configID;
        int checkDistance;
    }

    public FireflyConfigItem[] configData;
    public void InitConfig()
    {
        configData = Game.GetInstance().configManager.GetConfigDataAry<FireflyConfigItem>("Board.Firefly");
    }
}