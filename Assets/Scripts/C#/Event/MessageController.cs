using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageController : MonoBehaviour
{
    public enum Type
    {
        Title,
    }
    Dictionary<Type, GameObject> prefabs;
    public void Init()
    {
        Debug.Log("MessageController Init");
        prefabs = new Dictionary<Type, GameObject>();
        prefabs.Add(Type.Title, GameObject.Find("PopupTitle"));
        prefabs[Type.Title].SetActive(false);
    }

    public void popupMessage(string text, Type type, bool destroyAfter = true, float destroyDelay = 0.5f)
    {
        switch (type)
        {
            case Type.Title:
                GameObject titleGO = prefabs[Type.Title];
                titleGO.SetActive(true);
                titleGO.transform.localPosition = new Vector3(0, 0, 0);
                if (destroyAfter)
                {
                    //title.GetComponent<Text>().color.
                    Game.delayCall(() => { prefabs[Type.Title].SetActive(false); }, destroyDelay);
                }
                break;
        }
    }
}
