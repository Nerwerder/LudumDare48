using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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


    /// <summary>
    /// Will be called in PlayerController update
    /// Rotate the Ship to the mouse, move towards it or rotate around it
    /// </summary>
    public void movePlayer(Vector3 mousePos, float forward, float sideways) {
        //Rotate the Ship
        Vector3 tDir = mousePos - transform.position;
        float angle = Mathf.Atan2(tDir.y, tDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotationSpeed * Time.deltaTime));

        //Move the Ship
        if(forward != 0f) {
            transform.position += transform.right * Time.deltaTime * forward * movementSpeedForward;
        }
        if(sideways != 0f) {
            transform.position += transform.up * Time.deltaTime * sideways * movementSpeedSideways;
        }
    }
}
