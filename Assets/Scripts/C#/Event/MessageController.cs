using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageController
{
    public enum Scene
    {
        Title,
    }
    Dictionary<Scene, GameObject> prefabs;
    public void init()
    {
        prefabs = new Dictionary<Scene, GameObject>();
        prefabs.Add(Scene.Title, Resources.Load<GameObject>("/Prefabs/PopupTitle"));
    }

    public void popupMessage(string text, Scene scene, bool destroyAfter = true, float destroyDelay = 0.5f)
    {
        switch (scene)
        {
            case Scene.Title:
                GameObject title = GameObject.Instantiate(prefabs[Scene.Title]);
                if (destroyAfter)
                {
                    //title.GetComponent<Text>().color.
                    Game.delayCall(() => { GameObject.Destroy(title); }, destroyDelay);
                }
                break;
        }
    }
}
