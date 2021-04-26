using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class PlayerMovement : MonoBehaviour
{
    public bool rotateTowardsMouse = true;
    public float rotationSpeed = 5f;
    float mMovementForceForward = 0f;    //Set by SpaceStation (upgradable)
    public float movementForceForward {
        set { mMovementForceForward = value; }
        get { return mMovementForceForward; }
    }

    public float movementForceSideways = 300f;

    public float chargePrepTime = 0.1f;
    public float chargeForceMultiplier = 7f;
    public float chargeDuration = 0.2f;
    public float chargeCoolDown = 0.5f;
    public float chargeRotationDivider = 4f;
    public int chargeFuelCost = 7;

    public float travelModeForceMultiplier = 2f;
    public int travelModeSecondsPerFuel = 4;
    public float travelRotationDivider = 2f;
    float travelTimer = 0f;

    private float chargePrepTimer = 0f;
    private float chargeTimer = 0f;
    private float chargeCoolDownTimer = 0f;
    private PlayerState state;
    private PlayerAnimation anim;
    private Rigidbody2D mRb;
    public Rigidbody2D rb {
        set { mRb = value; }
        get { return mRb; }
    }

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
            case PlayerState.MovementState.travel:
                travelTimer += Time.deltaTime;
                break;
            default:
                chargeCoolDownTimer += Time.deltaTime;
                break;
        }
    }

    public void move(Vector3 mousePos, bool left, bool right, float forward, float sideways, bool chStart, bool chEnd, bool travel) {
        if (rotateTowardsMouse)
            rotate(mousePos);
        else
            rotate(left, right);
        checkChargeState(chStart, chEnd);
        checkTravelState(travel);
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
        float rotSpeed = rotationSpeed;
        switch(state.movementState) {
            case PlayerState.MovementState.charge:
                rotSpeed = rotationSpeed / chargeRotationDivider;
                break;
            case PlayerState.MovementState.travel:
                rotSpeed = rotationSpeed / travelRotationDivider;
                break;
            case PlayerState.MovementState.dead:
                rotSpeed = 0f;
                break;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (rotSpeed * Time.deltaTime));
    }    

    public void checkChargeState(bool start, bool end) {
        switch(state.movementState) {
            case PlayerState.MovementState.idle:
            case PlayerState.MovementState.moving_forwards:
            case PlayerState.MovementState.moving_backwards:
            case PlayerState.MovementState.travel:
                if (start) {
                    if (chargeCoolDownTimer >= chargeCoolDown) {
                        state.movementState = PlayerState.MovementState.chargePrep;
                        chargeCoolDownTimer = 0;
                    }
                }
                break;
            case PlayerState.MovementState.chargePrep:
                if(end) {
                    if((chargePrepTimer >= chargePrepTime) && (state.fuel > chargeFuelCost)) {
                        state.movementState = PlayerState.MovementState.charge;
                        state.invulnerable = true;
                        state.fuel -= chargeFuelCost;
                    } else {
                        state.movementState = PlayerState.MovementState.idle;
                    }
                }
                break;
        } 
    }

    public void checkTravelState(bool travel) {
        switch(state.movementState) {
            case PlayerState.MovementState.idle:
            case PlayerState.MovementState.moving_forwards:
            case PlayerState.MovementState.moving_backwards:
                if(travel && state.fuel > 0)
                    state.movementState = PlayerState.MovementState.travel;
                break;
            case PlayerState.MovementState.travel:
                if(!travel)
                    state.movementState = PlayerState.MovementState.idle;
                break;
        }
    }

    public void move(float forward, float sideways) {
        switch(state.movementState) {
            case PlayerState.MovementState.charge:
                if (chargeTimer >= chargeDuration) {
                    state.movementState = PlayerState.MovementState.idle;
                    state.invulnerable = false;
                    chargeTimer = 0;
                }
                rb.AddForce((Vector2)transform.right * Time.deltaTime * movementForceForward * chargeForceMultiplier);
                break;
            case PlayerState.MovementState.travel:
                if (state.fuel > 0) {
                    if (travelTimer > travelModeSecondsPerFuel) {
                        state.fuel -= 1;
                        travelTimer = 0f;
                    }
                } else {
                    state.movementState = PlayerState.MovementState.idle;
                }
                rb.AddForce((Vector2)transform.right * Time.deltaTime * movementForceForward * travelModeForceMultiplier); ;
                break;
            case PlayerState.MovementState.idle:
            case PlayerState.MovementState.moving_forwards:
            case PlayerState.MovementState.moving_backwards:
                if (forward > 0)
                    state.movementState = PlayerState.MovementState.moving_forwards;
                else if (forward < 0)
                    state.movementState = PlayerState.MovementState.moving_backwards;
                else
                    state.movementState = PlayerState.MovementState.idle;
                if (forward != 0f) {
                    rb.AddForce((Vector2)transform.right * Time.deltaTime * forward * movementForceForward);
                }
                if (sideways != 0f) {
                    rb.AddForce((Vector2)transform.up * Time.deltaTime * -sideways * movementForceSideways);
                }
                break;
            case PlayerState.MovementState.chargePrep:
            case PlayerState.MovementState.dead:
                //Nothing
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    public void addExternalForce(Vector2 force) {
        rb.AddForce(force);
    }
}
