using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageController
{
    GameObject damageTextPrefab;

    public void init()
    {
        damageTextPrefab = Resources.Load<GameObject>("Prefabs/DamageText");
    }

    public void createDamageText(Transform startTransform, float damage)
    {
        GameObject damageText = GameObject.Instantiate(damageTextPrefab);
        damageText.GetComponent<TextMesh>().text = "-" + damage.ToString();
        damageText.transform.position = startTransform.position;
        RangeFloat endPositionRange = new RangeFloat(0.5f, 1.2f);
        Vector3 endPosition = new Vector3(endPositionRange.getRandomFloat(true) + startTransform.position.x, startTransform.position.y - 0.3f, 0);
        float duration = 0.5f;
        damageText.transform.DOJump(endPosition, 1, 1, duration);
        Game.delayCall(() => { GameObject.Destroy(damageText); }, 0.51f);
    }
}
