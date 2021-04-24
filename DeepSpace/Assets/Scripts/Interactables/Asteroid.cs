using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Asteroid : Interactable
{
    public int lifePoints = 2;
    public int collisionDamage = 1;
    public float collisionForce = 2;

    public override void interact(GameObject other, Collision2D collision) {
        Assert.IsTrue(other.CompareTag("Player"));
        PlayerState state = other.GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        switch(state.movementState) {
            case PlayerState.MovementState.charge:
                Destroy(gameObject);
                break;
            default:
                state.takeDamage(collisionDamage);
                PlayerMovement movement = other.GetComponent<PlayerMovement>();
                Assert.IsNotNull(movement);
                movement.addExternalForce(collision.relativeVelocity * collisionForce);
                break;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
