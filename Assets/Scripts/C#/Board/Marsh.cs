using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marsh : MonoBehaviour
{
    public GameObject childMarshPrefab;
    public int initialRange;//初始的方块范围，只有本格则为0格
    public float spreadPeriod;//蔓延周期
    public int spreadCount;//蔓延格子数
    public int stayTime;
    public float initialDecelerationFactor;//初始减速比例
    public float decelerationLastTime;//减速持续时间
    public float finalDecelarationFactor;//最终减速比例
    RoleController mainCharacterController;
    List<GameObject> childMarshGOs;
    List<Vector2Int> childMarshCoordinates;
    bool _isDeceleration;
    int speedFactorID = -1;//-1表示空
    float decelerationTime;//进入减速状态的时间
    bool isDeceleration
    {
        get { return _isDeceleration; }
        set
        {
            if (_isDeceleration != value)//状态发生变化
            {
                if (value)//进入减速
                {
                    speedFactorID = mainCharacterController.registerSpeedFactor(initialDecelerationFactor);
                    decelerationTime = -Time.deltaTime;//第一次的时候刚好相加为0
                }
                else//脱离减速
                {
                    mainCharacterController.removeSpeedFactor(speedFactorID);
                    speedFactorID = -1;
                }
            };
            _isDeceleration = value;
        }
    }//是否处在减速状态
    public void Init(BlockView startBlock)
    {
        mainCharacterController = Game.GetInstance().mainCharacterController;

        childMarshGOs = new List<GameObject>();

        List<List<Vector2Int>> coordinateMartrix = Game.GetInstance().boardController.searchBlockInDistance(startBlock.coordinate, initialRange);
        for (int i = 0; i < coordinateMartrix.Count; i++)
        {
            if (coordinateMartrix[i].Count > 0)
            {
                foreach (Vector2Int coordinate in coordinateMartrix[i])
                {
                    createChildMash(coordinate);
                }
            }
        }



        callback coordinateChangeCallback = () =>
        {
            isDeceleration = childMarshCoordinates.Contains(Game.GetInstance().mainCharacterController.characterCoordinate);
        };

        Game.delayCall(() =>
        {
            OnDestroy();
        }, stayTime);
    }

    void OnDestroy()
    {
        for (int i = childMarshGOs.Count - 1; i >= 0; i++)
        {
            Destroy(childMarshGOs[i]);
        }
        if (speedFactorID != -1)
            mainCharacterController.removeSpeedFactor(speedFactorID);
        Destroy(gameObject);
    }

    void createChildMash(Vector2Int coordinate)
    {
        if (childMarshCoordinates.Contains(coordinate))
            return;
        Vector3 position = Game.GetInstance().boardController.getBlockPosition(coordinate);
        childMarshGOs.Add(GameObject.Instantiate(childMarshPrefab));
        childMarshCoordinates.Add(coordinate);
        childMarshGOs[childMarshGOs.Count - 1].transform.position = position;
    }

    void spread()
    {
        int index = Random.Range(0, childMarshCoordinates.Count);
        var newCoordinateMartix = Game.GetInstance().boardController.searchBlockInDistance(childMarshCoordinates[index], 1, (Vector2Int coordinate) =>
        {
            return !childMarshCoordinates.Contains(coordinate);
        });
    }

    //减速动画机
    bool inFinalFactor = false;//防止最后重复刷新
    void updateSpeedFactor(float time)
    {
        float timeFactor = time / decelerationLastTime;
        if (timeFactor >= 1)
        {
            if (inFinalFactor)
                return;
            else
            {
                mainCharacterController.changeSpeedFactor(speedFactorID, finalDecelarationFactor);
                inFinalFactor = true;
            }
        }
        else
        {
            float newSpeedFactor = initialDecelerationFactor - (initialDecelerationFactor - finalDecelarationFactor) * timeFactor;//均匀减速
            mainCharacterController.changeSpeedFactor(speedFactorID, finalDecelarationFactor);
            inFinalFactor = false;
        }
    }

    void Update()
    {
        if (isDeceleration)
        {
            decelerationTime += Time.deltaTime;
            updateSpeedFactor(decelerationTime);
        }
    }
}
