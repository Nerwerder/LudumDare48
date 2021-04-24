using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Loot : Interactable
{
    public float speed = 10f;
    private bool active = false;
    private Transform player = null;
    private LootParent lootParent = null;

    public override void interact(GameObject other, Collision2D collision) {
        throw new System.NotImplementedException();
    }

    public override void interact(GameObject other) {
        lootParent.deregisterInactiveLoot(gameObject.GetComponent<Loot>());
        Destroy(gameObject);
    }

    public void activate() {
        active = true;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        Assert.IsNotNull(player);
        lootParent = FindObjectOfType<LootParent>();
        Assert.IsNotNull(lootParent);
    }

    void Update()
    {
        if(active) {
            Vector3 dir = player.position - transform.position;
            transform.position += dir.normalized * Time.deltaTime * speed;
        }
    }


}