using UnityEngine;
using UnityEngine.Assertions;

public class Loot : Interactable
{
    public int metalValue = 1;
    public float speed = 10f;
    bool active = false;
    Transform player = null;
    public GameObject lootParentPrefab;
    LootParent lootParent = null;

    public override void interact(GameObject other, Collision2D collision) {
        throw new System.NotImplementedException();
    }

    public override void interact(GameObject other) {
        bool r = other.GetComponent<PlayerState>().addMetal(metalValue);
        if(r) {
            lootParent.deregisterInactiveLoot(gameObject.GetComponent<Loot>());
            Destroy(gameObject);
        } else {
            active = false;
        }
    }

    public void activate() {
        active = true;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        Assert.IsNotNull(player);
        Assert.IsNotNull(lootParentPrefab);
        lootParent = FindObjectOfType<LootParent>();
        if(lootParent == null) {
            Instantiate(lootParentPrefab);
            lootParent = FindObjectOfType<LootParent>();
        }
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
