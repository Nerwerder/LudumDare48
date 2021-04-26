using UnityEngine;
using UnityEngine.Assertions;

public class Asteroid : Interactable
{
    public int lifePoints = 2;
    public int collisionDamage = 1;
    public float minDamageVelocity = 2f;
    public float collisionForce = 2;
    public GameObject lootPrefab;
    public int minLoot = 3;
    public int maxLoot = 7;
    public float lootPer100UnitsFactor = 0.1f;
    public float lootOffset = 4f;

    public GameObject explosionPrefab;

    LootParent lootParent;
    public GameObject lootParentPrefab;

    void Start() {
        lootParent = FindObjectOfType<LootParent>();
        if(lootParent ==null) {
            Instantiate(lootParentPrefab);
            lootParent = FindObjectOfType<LootParent>();
        }
        Assert.IsNotNull(lootPrefab);
        Assert.IsNotNull(lootParent);
    }
    public override void interact(GameObject other) {
        throw new System.NotImplementedException();
    }

    public override void interact(GameObject other, Collision2D collision) {
        Assert.IsTrue(other.CompareTag("Player"));
        PlayerState state = other.GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        PlayerMovement movement = other.GetComponent<PlayerMovement>();
        if(movement.rb.velocity.magnitude > minDamageVelocity) {
            state.takeDamage(collisionDamage);
        }
        switch (state.movementState) {
            case PlayerState.MovementState.charge:
                int rLoot = Random.Range(minLoot, maxLoot);
                int fLoot = (int)((float)rLoot * Mathf.Max(1, ((transform.position.magnitude/100f) * lootPer100UnitsFactor)));
                for (int k = 0; k < fLoot; ++k) {
                    var offset = new Vector2(Random.Range(-lootOffset, lootOffset), Random.Range(-lootOffset, lootOffset));
                    var g = Instantiate(lootPrefab, ((Vector2)transform.position + offset), Quaternion.identity, lootParent.transform);
                    lootParent.registerInactiveLoot(g.GetComponent<Loot>());
                }
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            default:
                Assert.IsNotNull(movement);
                movement.addExternalForce(collision.relativeVelocity * collisionForce);
                break;
        }
    }

}
