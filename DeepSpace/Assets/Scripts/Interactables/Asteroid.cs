using UnityEngine;
using UnityEngine.Assertions;

public class Asteroid : Interactable
{
    public int lifePoints = 2;
    public int collisionDamage = 1;
    public float minDamageVelocity = 2f;
    public float collisionForce = 2;
    public float maxRotationSpeed = 45f;
    public GameObject lootPrefab;
    public int minLoot = 3;
    public int maxLoot = 7;
    public float lootOffset = 4f;

    public GameObject explosionPrefab;

    LootParent lootParent;
    float rotationSpeed = 0f;

    void Start() {
        lootParent = FindObjectOfType<LootParent>();
        Assert.IsNotNull(lootPrefab);
        Assert.IsNotNull(lootParent);
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
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
                for (int k = 0; k < Random.Range(minLoot, maxLoot); ++k) {
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

    private void Update() {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
