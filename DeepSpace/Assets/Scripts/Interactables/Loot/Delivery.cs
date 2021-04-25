using UnityEngine;
using UnityEngine.Assertions;


public class Delivery : MonoBehaviour
{
    public int metalValue = 1;
    public float force = 400f;
    public float minDeout = 10f;
    public float maxDetour = 30f;

    LootParent lootParent = null;
    SpaceStation station = null;
    Rigidbody2D rb = null;

    void Start()
    {
        station = FindObjectOfType<SpaceStation>();
        Assert.IsNotNull(station);
        lootParent = FindObjectOfType<LootParent>();
        Assert.IsNotNull(lootParent);
        Vector2 det = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = (Vector2)station.transform.position - (Vector2)transform.position;
        rb.AddForce(v.normalized * Time.deltaTime * force);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("SpaceStation")) {
            Destroy(gameObject);
            station.metal += metalValue;
        }
    }
}
