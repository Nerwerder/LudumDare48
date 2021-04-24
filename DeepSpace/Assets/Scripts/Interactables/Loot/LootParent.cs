using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootParent : MonoBehaviour
{
    private List<Loot> inactiveLoot = new List<Loot>();
    void Start()
    {   
    }

    void Update()
    {    
    }

    public void registerInactiveLoot(Loot loot) {
        inactiveLoot.Add(loot);
    }

    public void activateLootInDistance(Vector2 pos, float distance) {
        foreach(var loot in inactiveLoot) {
            if(Vector2.Distance((Vector2)loot.transform.position, pos) <= distance) {
                loot.activate();
            }
        }
    }

    public void deregisterInactiveLoot(Loot loot) {
        if(inactiveLoot.Contains(loot)) {
            inactiveLoot.Remove(loot);
        }
    }
}
