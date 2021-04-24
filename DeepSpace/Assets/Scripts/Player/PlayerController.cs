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

    void resolveCollisions() {

    }

    private void FixedUpdate() {
        resolveCollisions();
    }

    void Update()
    {
        Vector3 mousePos = getMousePos();
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        bool rotL = Input.GetKey(KeyCode.A);
        bool rotR = Input.GetKey(KeyCode.D);
        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        bool spaceUp = Input.GetKeyUp(KeyCode.Space);


        movement.move(mousePos, rotL, rotR, vInput, hInput, spaceDown, spaceUp);
    }
}
