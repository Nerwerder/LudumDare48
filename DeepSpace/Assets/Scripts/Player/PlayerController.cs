using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    PlayerMovement movement;
    PlayerActions actions;
    PlayerState state;
    FollowerCamera fCamera;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        Assert.IsNotNull(movement);
        state = GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        actions = GetComponent<PlayerActions>();
        Assert.IsNotNull(actions);
        fCamera = FindObjectOfType<FollowerCamera>();
        Assert.IsNotNull(fCamera);
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
        //Axis
        float vertical = Input.GetAxis("Vertical"); //q e
        float horizontal = Input.GetAxis("Horizontal");//w s
        float zoom = Input.GetAxis("Mouse ScrollWheel");//mouse 3
        //Buttons
        bool rotL = Input.GetKey(KeyCode.A);
        bool rotR = Input.GetKey(KeyCode.D);
        bool chargePrep = Input.GetKeyDown(KeyCode.Space);
        bool charge = Input.GetKeyUp(KeyCode.Space);
        bool travel = Input.GetKey(KeyCode.LeftShift);
        bool collectLoot = Input.GetKeyDown(KeyCode.C);
        bool dropLoot = Input.GetKeyDown(KeyCode.V);
        bool interact = Input.GetKeyDown(KeyCode.F);

        movement.move(mousePos, rotL, rotR, vertical, horizontal, chargePrep, charge, travel);
        actions.action(collectLoot, dropLoot, interact);
        if (zoom != 0)
            fCamera.zoom(zoom);
    }
}
