using UnityEngine;

public class Bullet : Interactable
{
    Vector3 mVelocity;
    public Vector3 velocity {
        set { mVelocity = value; }
    }
    Vector2 mSource;
    public Vector2 source {
        set { mSource = value; }
    }
    float mRange;
    public float range {
        set { mRange = value; }
    }
    int mDamage;
    public int damage {
        set { mDamage = value; }
    }

    public override void interact(GameObject other, Collision2D collision) {
        throw new System.NotImplementedException();
    }

    public override void interact(GameObject other) {
        var state = other.GetComponent<PlayerState>();
        state.takeDamage(mDamage);
        Destroy(gameObject);
    }

    private void Update() {
        transform.position += (mVelocity * Time.deltaTime);
        if (Vector2.Distance(mSource, (Vector2)transform.position) >= mRange)
            Destroy(gameObject);
    }
}
