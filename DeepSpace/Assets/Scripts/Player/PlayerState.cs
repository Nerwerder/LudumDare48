using UnityEngine;


public class PlayerState : MonoBehaviour
{
    PlayerAnimation pAnimation;
    PlayerGui pGui;

    //Ship Values
    public int mHullPoints = 10;
    public int hullPoints {
        set {
            mHullPoints = Mathf.Max(value, 0);
            pGui.updateText();
        }
        get { return mHullPoints; }
    }
    public int maxHullPoints = 10;

    public int mShieldPoints = 0;
    public int shieldPoints {
        set {
            mShieldPoints = Mathf.Max(value, 0);
            pGui.updateText();
        }
        get { return mShieldPoints; }
    }
    public int maxShieldPoints = 5;

    public int mFuel = 100;
    public int fuel {
        set {
            mFuel = value;
            pGui.updateText();
            pAnimation.updateFuelGauge(mFuel, maxFuel);
        }
        get { return mFuel; }
    }
    public int maxFuel = 100;

    public int mMetal = 0;
    public int metal {
        set {
            mMetal = value;
            pGui.updateText();
        }
        get { return mMetal; }
    }
    public int maxMetal = 100;

    //Ship State
    public enum MovementState
    {
        undefined,
        idle,
        moving_forwards,
        moving_backwards,
        chargePrep,
        charge
    }
    private MovementState mMovementState = MovementState.undefined;
    public MovementState movementState {
        get { return mMovementState; }
        set { 
            mMovementState = value;
            pAnimation.updateEngines(mMovementState);
        }
    }

    void Start()
    {
        pAnimation = GetComponent<PlayerAnimation>();
        pGui = GetComponent<PlayerGui>();
        movementState = PlayerState.MovementState.idle;
    }

    public bool takeDamage(int dmg) {
        switch(movementState) {
            case MovementState.charge:
                //No damage taken
                return false;
            default:
                if (shieldPoints > 0)
                    shieldPoints -= dmg;
                else
                    hullPoints -= dmg;

                if (hullPoints <= 0) {
                    //TODO: GameOver
                }
                return true;
        }
    }
}
