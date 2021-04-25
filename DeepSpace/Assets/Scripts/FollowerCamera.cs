using UnityEngine;
using UnityEngine.Assertions;

public class FollowerCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 10f;
    public float zoomFactor = 3f;

    public Vector3 offset;
    Camera cam;
    void Start()
    {
        Assert.IsNotNull(player);
        cam = GetComponent<Camera>();
        Assert.IsNotNull(cam);
    }

    private void FixedUpdate() {
        var desPosition = player.position + offset;
        var smoPosition = Vector3.Lerp(transform.position, desPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoPosition;
    }

    public void zoom(float c) {
        cam.orthographicSize += (-c * zoomFactor);
    }
}
