using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerActions : MonoBehaviour
{
    public float collectCoolDown = 2f;
    float collectCoolDownTimer = 0f;
    public float collectRange = 30f;
    public float dropOfRange = 20f;
    public GameObject deliveryPrefab;

    LootParent lootParent = null;
    PlayerState state = null;
    SpaceStation home = null;

    void Start()
    {
        lootParent = FindObjectOfType<LootParent>();
        Assert.IsNotNull(lootParent);
        home = FindObjectOfType<SpaceStation>();
        Assert.IsNotNull(home);
        state = GetComponent<PlayerState>();
        Assert.IsNotNull(state);
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

    public void dropLoot() {
        float td = Vector2.Distance((Vector2)home.transform.position, (Vector2)transform.position);
        if ((td < dropOfRange) && (state.metal > 0)) {
            for(var k = 0; k < state.metal; ++k) {
                Vector3 dir = (home.transform.position-transform.position).normalized;
                dir = Quaternion.Euler(0, 0, Random.Range(-60f, 60f)) * dir;
                Instantiate(deliveryPrefab, transform.position + (dir.normalized * 5), Quaternion.identity, lootParent.transform);
            }
            state.metal = 0;
        }
    }
}
