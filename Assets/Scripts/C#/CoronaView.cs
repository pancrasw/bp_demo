using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//轮盘
public class CoronaView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //轮盘外沿
    public GameObject corona;
    //摇杆
    public GameObject rockingBar;
    //摇杆拖动范围
    public float range;

    bool isDraging;
    Vector3 startPosition;
    Vector3 endPostion;
    RectTransform rectTransform;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        corona.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,
        Input.mousePosition, pointerEventData.enterEventCamera, out startPosition);

        corona.SetActive(true);
        corona.transform.position = startPosition;
        rockingBar.transform.position = startPosition;

        isDraging = true;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,
        Input.mousePosition, pointerEventData.enterEventCamera, out endPostion);

        if (Vector3.Distance(endPostion, startPosition) > range)
        {
            endPostion = startPosition + Vector3.Normalize(endPostion - startPosition) * range;
        }

        rockingBar.transform.position = endPostion;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        corona.SetActive(false);
        isDraging = false;
    }

    public bool TryGetDirection(out Vector3 direction)
    {
        if (isDraging)
            direction = Vector3.Normalize(endPostion - startPosition);
        else
            direction = new Vector3(0, 0, 0);
        return isDraging;
    }
}
