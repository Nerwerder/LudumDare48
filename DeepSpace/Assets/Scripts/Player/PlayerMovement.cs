using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class PlayerMovement : MonoBehaviour
{
    public bool rotateTowardsMouse = true;
    public float rotationSpeed = 5f;
    public float movementForceForward = 500f;
    public float movementForceSideways = 300f;

    public float chargePrepTime = 0.1f;
    public float chargeForce = 3000f;
    public float chargeDuration = 0.2f;
    public float chargeCoolDown = 0.5f;

    private float chargePrepTimer = 0f;
    private float chargeTimer = 0f;
    private float chargeCoolDownTimer = 0f;
    private PlayerState state;
    private PlayerAnimation anim;
    private Rigidbody2D rb;

    void Start()
    {
        state = GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb);
        anim = GetComponent<PlayerAnimation>();
        Assert.IsNotNull(anim);
    }

    void Update() { 
        switch(state.movementState) {
            case PlayerState.MovementState.chargePrep:
                chargePrepTimer += Time.deltaTime;
                break;
            case PlayerState.MovementState.charge:
                chargeTimer += Time.deltaTime;
                break;
            default:
                chargeCoolDownTimer += Time.deltaTime;
                break;
        }
    }

    public void move(Vector3 mousePos, bool left, bool right, float forward, float sideways, bool chStart, bool chEnd) {
        if (rotateTowardsMouse)
            rotate(mousePos);
        else
            rotate(left, right);
        checkChargeState(chStart, chEnd);
        move(forward, sideways);
    }

    public void rotate(Vector3 mousePos) {
        Vector3 tDir = mousePos - transform.position;
        rotateTo(tDir);
    }

    public void rotate(bool left, bool right) {
        if(left == right)  //If both ot zero keys are pressed, do nothing
            return;
        Vector3 tDir = left ? transform.up : -transform.up;
        rotateTo(tDir);
    }

    private void rotateTo(Vector3 tDir) {
        float angle = Mathf.Atan2(tDir.y, tDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotationSpeed * Time.deltaTime));
    }    

    public void checkChargeState(bool start, bool end) {
        if (!start && !end)
            return;
        if (start) {
            if ((state.movementState == PlayerState.MovementState.moving) && (chargeCoolDownTimer >= chargeCoolDown)) {
                state.movementState = PlayerState.MovementState.chargePrep;
                chargeCoolDownTimer = 0;
            }
        } else if (end) {
            if ((state.movementState == PlayerState.MovementState.chargePrep) && (chargePrepTimer >= chargePrepTime)) {
                state.movementState = PlayerState.MovementState.charge;
                chargePrepTimer = 0;
            }
        }
    }

    public void move(float forward, float sideways) {
        switch (state.movementState) {
            case PlayerState.MovementState.charge:
                if (chargeTimer >= chargeDuration) {
                    state.movementState = PlayerState.MovementState.moving;
                    chargeTimer = 0;
                    
                }
                rb.AddForce((Vector2)transform.right * Time.deltaTime * chargeForce);
                break;
            case PlayerState.MovementState.moving:
                if (forward != 0f) {
                    rb.AddForce((Vector2)transform.right * Time.deltaTime * forward * movementForceForward);
                    if(forward > 0) 
                        anim.setBackWardsEngineState(true);
                    else
                        anim.setBackWardsEngineState(false);
                } else {
                    anim.setBackWardsEngineState(false);
                }
                if (sideways != 0f) {
                    rb.AddForce((Vector2)transform.up * Time.deltaTime * -sideways * movementForceSideways);
                }
                break;
        }
    }
}
