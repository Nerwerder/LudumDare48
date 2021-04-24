using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool rotateTowardsMouse = true;
    public float rotationSpeed = 5f;
    public float movementSpeedForward = 3f;
    public float movementSpeedSideways = 2f;

    void Start()
    {
        //Nothing
    }

    void Update()
    {
        //Nothing
    }

    public bool useMouseRotation() {
        return rotateTowardsMouse;
    }

    public void rotatePlayer(Vector3 mousePos) {
        Vector3 tDir = mousePos - transform.position;
        float angle = Mathf.Atan2(tDir.y, tDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotationSpeed * Time.deltaTime));

    }

    public void rotatePlayer(bool left, bool right) {
        if(left == right)  //If both ot zero keys are pressed, do nothing
            return;
        Vector3 tDir = left ? transform.up : -transform.up;
        float angle = Mathf.Atan2(tDir.y, tDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotationSpeed * Time.deltaTime));
    }

    public void movePlayer( float forward, float sideways) {
        //Move the Ship
        if(forward != 0f) {
            transform.position += transform.right * Time.deltaTime * forward * movementSpeedForward;
        }
        if(sideways != 0f) {
            transform.position += transform.up * Time.deltaTime * -sideways * movementSpeedSideways;
        }
    }
}
