using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        Assert.IsNotNull(movement);
    }
    Vector3 getMousePos() {
        //Get the Mouse (look) Position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void Update()
    {
        Vector3 mousePos = getMousePos();
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        bool q = Input.GetKey(KeyCode.Q);
        bool e = Input.GetKey(KeyCode.E);

        //Movement
        if (movement.useMouseRotation())
            movement.rotatePlayer(mousePos);
        else
            movement.rotatePlayer(q, e);
        movement.movePlayer(vInput, hInput);
    }
}
