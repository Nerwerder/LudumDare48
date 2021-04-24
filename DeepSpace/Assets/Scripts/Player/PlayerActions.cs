using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerActions : MonoBehaviour
{
    public float collectCoolDown = 2f;
    float collectCoolDownTimer = 0f;
    public float collectRange = 30f;

    LootParent lootParent = null;

    void Start()
    {
        lootParent = FindObjectOfType<LootParent>();
        Assert.IsNotNull(lootParent);
    }

    void Update()
    {
        collectCoolDownTimer += Time.deltaTime;
    }

    public void collectLoot() {
        if (collectCoolDownTimer >= collectCoolDown) {
            collectCoolDownTimer = 0f;
            lootParent.activateLootInDistance((Vector2)transform.position, collectRange);
        }
    }

}
