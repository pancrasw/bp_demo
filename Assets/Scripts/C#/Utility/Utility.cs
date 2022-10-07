using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//排序用比较函数
public delegate bool CompFunc<T>(T t1, T t2);
//无状态共用函数
public class Utility
{
    public static void sort<T>(List<T> list, CompFunc<T> getKey)
    {
        if (list.Count >= 10)
        {

        }
    }

    public static void exchange<T>(List<T> list, int a, int b)
    {
        if (a == b)
            return;
        T temp = list[a];
        list[a] = list[b];
        list[b] = temp;
    }

    //start包含，end不包含
    private void helpQuickSort<T>(List<T> list, CompFunc<T> compFunc, int start, int end)
    {
        int less_i = start;
        int more_i = start;
        int pivot = end - 1;
        for (int i = 0; i < list.Count; i++)
        {
            //Todo
        }
    }

    public static void swap<T>(T a, T b)
    {
        T temp = b;
        b = a;
        a = temp;
    }
}

//最小堆
public class PriorityHeap<T>
{
    CompFunc<T> compFunc;
    List<T> data;
    public int Count { get { return data.Count; } }
    public PriorityHeap(CompFunc<T> compFunc)
    {
        this.compFunc = compFunc;
        data = new List<T>();
    }

    public void Add(T t)
    {
        data.Add(t);
        siftUp(data.Count - 1);
    }

    public T popup()
    {
        if (data.Count == 0)
            return default(T);
        T result = data[0];
        data[0] = data[data.Count - 1];
        data.RemoveAt(data.Count - 1);
        siftDown(0);
        return result;
    }

    public T getTop()
    {
        if (data.Count == 0)
            return default(T);
        else
            return data[0];
    }

    public void remove(T t)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].Equals(t))
            {
                data[i] = data[data.Count - 1];
                data.RemoveAt(data.Count - 1);
                siftDown(i);
                return;
            }
        }
    }

    private void siftUp(int index)
    {
        if (index == 0)
            return;
        int fatherIndex = (index - 1) / 2;
        if (!compFunc(data[fatherIndex], data[index]))
        {
            Utility.exchange(data, fatherIndex, index);
            siftUp(fatherIndex);
        }
    }

    private void siftDown(int index)
    {
        int leftChildIndex = index * 2 + 1;
        int rightChildIndex = index * 2 + 2;
        int minIndex = min(index, leftChildIndex, rightChildIndex);
        if (minIndex != index)
        {
            Utility.exchange(data, minIndex, index);
            siftDown(minIndex);
        }
    }

    private int min(params int[] indexs)
    {
        if (indexs == null || indexs.Length == 0)
            return -1;
        int minIndex = indexs[0];
        for (int i = 1; i < indexs.Length; i++)
        {
            if (indexs[i] >= 0 && indexs[i] < data.Count && !compFunc(data[minIndex], data[indexs[i]]))
            {
                minIndex = indexs[i];
            }
        }
        return minIndex;
    }
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
            if (Random.Range(0, 2) == 0)//一半几率为负数
            {
                randomResult = -randomResult;
            }
        }
        return randomResult;
    }
}