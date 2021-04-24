using UnityEngine;
using UnityEngine.Assertions;

public class FollowerCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 10f;

    private Vector3 offset;

    void Start()
    {
        Assert.IsNotNull(player);
        offset = transform.position - player.position;
    }

    private void FixedUpdate() {
        var desPosition = player.position + offset;
        var smoPosition = Vector3.Lerp(transform.position, desPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoPosition;
    }
}
