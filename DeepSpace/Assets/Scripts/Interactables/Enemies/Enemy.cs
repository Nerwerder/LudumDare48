using UnityEngine;
using UnityEngine.Assertions;

public abstract class Enemy : Interactable
{
    public int difficulty = 1;
    public float rotationSpeed = 5;
    public float movementForce = 300;
    public int collisionDamage = 2;
    public bool destroyedAfterCollision = false;

    public GameObject destroyAnim;

    protected Transform target;
    protected Rigidbody2D rb;

    public override void interact(GameObject other, Collision2D collision) {
        Assert.IsTrue(other.CompareTag("Player"));
        PlayerState state = other.GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        state.takeDamage(collisionDamage);
        switch (state.movementState) {
            case PlayerState.MovementState.charge:
                Instantiate(destroyAnim, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            default:
                if (destroyedAfterCollision)
                {
                    Instantiate(destroyAnim, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                    
                break;
        }
    }

    public override void interact(GameObject other) {
        throw new System.NotImplementedException();
    }

    protected void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        Assert.IsNotNull(target);
        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb);
    }

    protected float getAngleToTaget() {
        var tDir = target.position - transform.position;
        return Mathf.Atan2(tDir.y, tDir.x) * Mathf.Rad2Deg;
    }

    protected void turnToTaget() {
        Quaternion rot = Quaternion.AngleAxis(getAngleToTaget(), Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotationSpeed * Time.deltaTime));
    } 

    void Update()
    {
        //Nothing
    }
}
