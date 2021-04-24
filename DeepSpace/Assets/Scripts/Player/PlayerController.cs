using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerActions actions;
    private PlayerState state;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        Assert.IsNotNull(movement);
        state = GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        actions = GetComponent<PlayerActions>();
        Assert.IsNotNull(actions);
    }

    Vector3 getMousePos() {
        //Get the Mouse (look) Position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Interactable")) {
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            Assert.IsNotNull(interactable);
            interactable.interact(gameObject, collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Interactable")) {
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            Assert.IsNotNull(interactable);
            interactable.interact(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        //Nothing
    }

    private void OnCollisionExit2D(Collision2D collision) {
        //Nothing
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
        bool collectLoot = Input.GetKeyDown(KeyCode.C);

        movement.move(mousePos, rotL, rotR, vInput, hInput, spaceDown, spaceUp);
        if (collectLoot)
            actions.collectLoot();
    }
}
