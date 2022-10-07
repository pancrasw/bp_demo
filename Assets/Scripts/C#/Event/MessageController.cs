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
        Buff,
    }
    Dictionary<Type, GameObject> prefabs;
    public void Init()
    {
        Debug.Log("MessageController Init");
        prefabs = new Dictionary<Type, GameObject>();

        prefabs.Add(Type.Title, GameObject.Find("PopupTitle"));
        prefabs[Type.Title].SetActive(false);

        prefabs.Add(Type.Buff, GameObject.Find("Buff"));
        prefabs[Type.Buff].SetActive(false);
    }

    public void popupMessage(string text, Type type, bool destroyAfter = true, float destroyDelay = 0.5f)
    {
        GameObject tempGameObject = prefabs[Type.Title];
        tempGameObject.SetActive(true);
        tempGameObject.transform.localPosition = new Vector3(0, 0, 0);
        tempGameObject.GetComponent<Text>().text = text;
        switch (type)
        {
            case Type.Title:
                if (destroyAfter)
                {
                    //title.GetComponent<Text>().color.
                    Game.delayCall(() => { prefabs[Type.Title].SetActive(false); }, destroyDelay);
                }
                break;
        }
    }

    public void popupBuffMesage(string text, Color color)
    {
        GameObject tempGameObject = prefabs[Type.Buff];
        tempGameObject.SetActive(true);
        tempGameObject.transform.localPosition = new Vector3(0, 0, 0);
        tempGameObject.GetComponent<Text>().text = text;

        float duration = 0.7f;

        Vector3 oldPos = tempGameObject.transform.position;
        tempGameObject.transform.DOMove(new Vector3(oldPos.x, oldPos.y + 113f, 0), duration);

        Game.delayCall(() =>
        {
            tempGameObject.SetActive(false);
        }, duration);
    }
}
