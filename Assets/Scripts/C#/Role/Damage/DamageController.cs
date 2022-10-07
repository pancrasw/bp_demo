using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageView
{
    public GameObject damageTextPrefab;
    public GameObject healTextPrefab;

    public void Init()
    {
        damageTextPrefab = Resources.Load<GameObject>("Prefabs/DamageText");
        healTextPrefab = Resources.Load<GameObject>("Prefabs/HealText");
    }

    public void CreateDamageText(Transform startTransform, float damage)
    {
        GameObject damageText = GameObject.Instantiate(damageTextPrefab);
        damageText.GetComponent<TextMesh>().text = "-" + damage.ToString();
        damageText.transform.position = new Vector3(startTransform.position.x, startTransform.position.y, Game.UI_LAYER);
        if (damage >= 10)//伤害高时字体调大
        {
            damageText.transform.localScale = new Vector3(1.3f, 1.3f, 0);
        }
        RangeFloat endPositionRange = new RangeFloat(0.5f, 1.2f);
        Vector3 endPosition = new Vector3(endPositionRange.getRandomFloat(true) + startTransform.position.x, startTransform.position.y - 0.3f, Game.UI_LAYER);
        float duration = 0.5f;//写死
        damageText.transform.DOJump(endPosition, 1, 1, duration);
        Game.delayCall(() => { GameObject.Destroy(damageText); }, 0.51f);
    }

    public void CreateHealText(Transform startTransform, float healPoint)
    {
        GameObject healText = GameObject.Instantiate(healTextPrefab);
        healText.GetComponent<TextMesh>().text = "+" + healPoint.ToString();
        healText.transform.position = new Vector3(startTransform.position.x, startTransform.position.y, Game.UI_LAYER);
        if (healPoint >= 10)//治疗量高时字体调大
        {
            healText.transform.localScale = new Vector3(1.3f, 1.3f, 0);
        }
        RangeFloat endPositionRange = new RangeFloat(0.5f, 1.2f);
        Vector3 endPosition = new Vector3(startTransform.position.x, startTransform.position.y + 0.3f, Game.UI_LAYER);
        float duration = 0.5f;
        healText.transform.DOMove(endPosition, duration);
        Game.delayCall(() => { GameObject.Destroy(healText); }, 0.51f);
    }
}
