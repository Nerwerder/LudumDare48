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
    public float spaceStationInteractionRange = 30f;

    LootParent lootParent = null;
    public GameObject lootParentPrefab;
    SpaceStation sHome = null;
    PlayerState pState = null;
    PlayerAnimation pAnim;

    void Start() {
        lootParent = FindObjectOfType<LootParent>();
        if (lootParent == null) {
            Instantiate(lootParentPrefab);
            lootParent = FindObjectOfType<LootParent>();
        }
        Assert.IsNotNull(lootParent);
        sHome = FindObjectOfType<SpaceStation>();
        Assert.IsNotNull(sHome);
        pState = GetComponent<PlayerState>();
        Assert.IsNotNull(pState);
        pAnim = GetComponent<PlayerAnimation>();
        Assert.IsNotNull(pAnim);
    }

    void Update() {
        collectCoolDownTimer += Time.deltaTime;
    }

    public void action(bool aCollectLoot, bool aDropLoot, bool aInteract) {
        if(aCollectLoot)
            collectLoot();
        if(aDropLoot)
            dropLoot();
        if(aInteract)
            interact();
    }

    public void collectLoot() {
        if (collectCoolDownTimer >= collectCoolDown) {
            collectCoolDownTimer = 0f;
            lootParent.activateLootInDistance((Vector2)transform.position, collectRange);
            pAnim.showPickUpRange();
        }
    }

    public void dropLoot() {
        float td = Vector2.Distance((Vector2)sHome.transform.position, (Vector2)transform.position);
        if ((td < dropOfRange) && (pState.metal > 0)) {
            for(var k = 0; k < pState.metal; ++k) {
                Vector3 dir = (sHome.transform.position-transform.position).normalized;
                dir = Quaternion.Euler(0, 0, Random.Range(-60f, 60f)) * dir;
                Instantiate(deliveryPrefab, transform.position + (dir.normalized * 5), Quaternion.identity, lootParent.transform);
            }
            pState.metal = 0;
        }
    }

    public void interact() {
        //Is the Space Station near?
        if(Vector3.Distance(sHome.transform.position, transform.position) < spaceStationInteractionRange) {
            sHome.interact();
        }
    }
}
