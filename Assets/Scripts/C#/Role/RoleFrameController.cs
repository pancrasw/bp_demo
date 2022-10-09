using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    UP, DOWN, LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT
}

public enum RoleMotionState
{
    Idle,//空闲状态
    Walk,//走路
}


//控制角色相关的表现
public class RoleFrameController : MonoBehaviour
{
    Direction direction = Direction.DOWN;
    RoleMotionState roleMotionState;
    bool isWalking;
    int frameOffset = 1;
    int frameCount;
    float playTime = 0;//动画播放时间
    public bool selfJudgeDirection;//自行根据自身位置变化判断方向
    public bool alwaysWalk = true;//一直在走，用于飞行角色
    public float walkPeriod;//动画播放周期
    Vector3 lastPosition;
    public Sprite[] frames;//图片按照上空闲，上走1，上走2，下空闲，下走1，下走二，以此类推的顺序排列

    void Start()
    {
        frameCount = frames.Length / 4;//共四个方向
    }

    public void Walk(Direction direction)
    {
        this.direction = direction;
        roleMotionState = RoleMotionState.Walk;
        isWalking = true;
    }

    public void Stop()
    {
        isWalking = false;
    }

    void updateFrameOffset(float time)
    {
        float timeOffset = time - (int)(time / walkPeriod) * walkPeriod;
        frameOffset = (int)(timeOffset / walkPeriod * frameCount);
    }

    float oldFrameIndex = -1;
    void Update()
    {
        if (selfJudgeDirection)
        {
            //判断静止与否
            Vector3 newPosition = transform.localPosition;
            if (newPosition.Equals(lastPosition))
            {
                isWalking = false;
            }
            else
            {
                //自行判断方向
                Vector3 directionVector = Vector3.Normalize(newPosition - lastPosition);
                directionVector = Quaternion.AngleAxis(-45, Vector3.forward) * directionVector;//顺时针旋转旋转45度
                if (directionVector.x > 0)
                {
                    if (directionVector.y > 0)//第一象限为上
                        direction = Direction.UP;
                    else//第四象限为右
                        direction = Direction.RIGHT;
                }
                else
                {
                    if (directionVector.y > 0)
                        direction = Direction.LEFT;
                    else
                        direction = Direction.DOWN;
                }
                isWalking = true;
                roleMotionState = RoleMotionState.Walk;

                lastPosition = newPosition;
            }
        }

        if (isWalking || alwaysWalk)
        {
            playTime += Time.deltaTime;
            updateFrameOffset(playTime);

            int frameIndex = (int)(direction) * frameCount + frameOffset;
            if (oldFrameIndex == frameIndex)
                return;

            GetComponent<SpriteRenderer>().sprite = frames[frameIndex];

            oldFrameIndex = frameIndex;
        }
        else if (roleMotionState == RoleMotionState.Walk)
        {
            int frameIndex = (int)(direction) * frameCount;
            if (oldFrameIndex == frameIndex)
                return;

            GetComponent<SpriteRenderer>().sprite = frames[frameIndex];

            roleMotionState = RoleMotionState.Idle;
            oldFrameIndex = frameIndex;
        }
    }
}
