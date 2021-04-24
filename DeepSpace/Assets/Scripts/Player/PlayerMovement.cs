using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class PlayerMovement : MonoBehaviour
{
    public bool rotateTowardsMouse = true;
    public float rotationSpeed = 5f;
    public float movementSpeedForward = 3f;
    public float movementSpeedSideways = 2f;

    public float chargePrepTime = 0.1f;
    public float chargeSpeed = 15f;
    public float chargeDuration = 0.2f;
    public float chargeCoolDown = 0.5f;

    enum MovementState
    {
        moving,
        chargePrep,
        charge
    }
    private MovementState movementState = MovementState.moving;
    private float chargePrepTimer = 0f;
    private float chargeTimer = 0f;
    private float chargeCoolDownTimer = 0f;
    void Start()
    {
        //Nothing
    }

    void Update() { 
        switch(movementState) {
            case MovementState.chargePrep:
                chargePrepTimer += Time.deltaTime;
                break;
            case MovementState.charge:
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
        float angle = Mathf.Atan2(tDir.y, tDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotationSpeed * Time.deltaTime));
    }

    public void rotate(bool left, bool right) {
        if(left == right)  //If both ot zero keys are pressed, do nothing
            return;
        Vector3 tDir = left ? transform.up : -transform.up;
        float angle = Mathf.Atan2(tDir.y, tDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotationSpeed * Time.deltaTime));
    }
    public void checkChargeState(bool start, bool end) {
        if (!start && !end)
            return;
        if (start) {
            if ((movementState == MovementState.moving) && (chargeCoolDownTimer >= chargeCoolDown)) {
                movementState = MovementState.chargePrep;
                chargeCoolDownTimer = 0;
            }
        } else if (end) {
            if ((movementState == MovementState.chargePrep) && (chargePrepTimer >= chargePrepTime)) {
                movementState = MovementState.charge;
                chargePrepTimer = 0;
            }
        }
    }

    public void move(float forward, float sideways) {
        switch (movementState) {
            case MovementState.charge:
                if(chargeTimer >= chargeDuration) {
                    movementState = MovementState.moving;
                    chargeTimer = 0;
                }
                transform.position += transform.right * Time.deltaTime * chargeSpeed;
                break;
            case MovementState.moving:
                if (forward != 0f) {
                    transform.position += transform.right * Time.deltaTime * forward * movementSpeedForward;
                }
                if (sideways != 0f) {
                    transform.position += transform.up * Time.deltaTime * -sideways * movementSpeedSideways;
                }
                break;
        }
    }
}
