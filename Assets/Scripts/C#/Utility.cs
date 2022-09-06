using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{

}

public class RangeFloat
{
    public float max;
    public float min;
    public RangeFloat(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    //randomNegative 是否随机取相反数
    public float getRandomFloat(bool randomNegative)
    {
        float randomResult = Random.Range(min, max);
        if (randomNegative)
        {
            if (Random.Range(0, 1) == 0)//一半几率为负数
            {
                randomResult = -randomResult;
            }
        }
        return randomResult;
    }
}