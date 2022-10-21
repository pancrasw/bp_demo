using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marsh : Creater
{
    public GameObject childMarshPrefab;

    public int initialRange;//初始的方块范围，只有本格则为0格
    public float spreadPeriod;//蔓延周期
    public int spreadCount;//蔓延格子数
    public int stayTime;//存在时间
    public float initialDecelerationFactor;//初始减速比例
    public float decelerationLastTime;//减速持续时间
    public float finalDecelarationFactor;//最终减速比例

    RoleController mainCharacterController;
    List<GameObject> childMarshGOs;
    List<Vector2Int> childMarshCoordinates;
    bool _isDeceleration = false;
    int speedFactorID = -1;//-1表示空
    float decelerationTime;//进入减速状态的时间
    Timer spreadTimer;//扩散计时器
    callback coordinateChangeCallback;

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
        childMarshCoordinates = new List<Vector2Int>();

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

        isDeceleration = true;//打开瞬间肯定有减速

        coordinateChangeCallback = () =>
        {
            isDeceleration = childMarshCoordinates.Contains(Game.GetInstance().mainCharacterController.characterCoordinate);
        };
        Game.GetInstance().mainCharacterController.coordinateChange += coordinateChangeCallback;

        Game.delayCall(() =>
        {
            OnDestroy();
        }, stayTime);

        loopSpread();
    }

    new void OnDestroy()
    {

        Game.GetInstance().mainCharacterController.coordinateChange -= coordinateChangeCallback;

        for (int i = childMarshGOs.Count - 1; i >= 0; i--)
        {
            if (childMarshGOs[i] != null)
                Destroy(childMarshGOs[i]);
        }
        if (speedFactorID != -1)
            mainCharacterController.removeSpeedFactor(speedFactorID);
        if (spreadTimer != null)
        {
            Game.GetInstance().timerController.removeTimer(spreadTimer);
            spreadTimer = null;
        }

        if (this != null)
            Destroy(gameObject);

        base.OnDestroy();
    }

    void createChildMash(Vector2Int coordinate)
    {
        if (childMarshCoordinates.Contains(coordinate))
            return;
        Vector3 position = Game.GetInstance().boardController.getBlockPosition(coordinate);
        GameObject newGameObject = GameObject.Instantiate(childMarshPrefab);
        childMarshGOs.Add(newGameObject);
        childMarshCoordinates.Add(coordinate);
        Vector3 boardViewScale = GameObject.Find("BoardView").transform.localScale;
        newGameObject.transform.localScale = new Vector3(0.5809942f, 0.7f * boardViewScale.y, 1);
        newGameObject.transform.position = new Vector3(position.x, position.y, position.z + 1);
    }

    public void loopSpread()
    {
        spreadTimer = Game.delayCall(() =>
        {
            if (!isPause)
                spread();
            loopSpread();
        }, spreadPeriod);
    }

    void spread(int index = -1)
    {
        if (index == -1)
            index = Random.Range(0, childMarshCoordinates.Count);

        var newCoordinateMartix = Game.GetInstance().boardController.searchBlockInDistance(childMarshCoordinates[index], 2, (Vector2Int coordinate) =>
        {
            return !childMarshCoordinates.Contains(coordinate);
        });

        if (newCoordinateMartix[1].Count > 0)
        {
            if (newCoordinateMartix[1].Count == 1)
                createChildMash(newCoordinateMartix[1][0]);
            else
            {
                index = Random.Range(0, newCoordinateMartix[1].Count);
                createChildMash(newCoordinateMartix[1][index]);
            }
        }
        else
        {
            spread((index + 1) % childMarshCoordinates.Count);
        }

    }

    //减速动画机
    bool inFinalFactor = false;//防止最后重复刷新
    void updateSpeedFactor(float time)
    {
        float timeFactor = time / decelerationLastTime;
        Debug.Log(timeFactor);
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
            mainCharacterController.changeSpeedFactor(speedFactorID, newSpeedFactor);
            inFinalFactor = false;
        }
    }

    void Update()
    {
        if (_isDeceleration)
        {
            decelerationTime += Time.deltaTime;
            updateSpeedFactor(decelerationTime);
        }
    }
}
