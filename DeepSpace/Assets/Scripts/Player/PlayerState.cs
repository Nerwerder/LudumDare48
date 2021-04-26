using UnityEngine;


public class PlayerState : MonoBehaviour
{
    PlayerAnimation pAnimation;
    PlayerGui pGui;
    public GameObject GameOverScreen;

    //HULL
    public int mHullPoints = 10;
    public int hullPoints {
        set {
            mHullPoints = Mathf.Max(value, 0);
            pGui.updateText();
        }
        get { return mHullPoints; }
    }
    public int maxHullPoints = 10;

    //SHIELD
    public float shieldRecoveryTime = 0.5f;
    float shieldRecoveryTimer = 0;
    int mShieldPoints = 0;
    public int shieldPoints {
        set {
            mShieldPoints = Mathf.Max(value, 0);
            pGui.updateText();
            pAnimation.updateShieldState(mShieldPoints);
        }
        get { return mShieldPoints; }
    }
    int mMaxShieldPoints;   //set by SpaceStation (upgradable)
    public int maxShieldPoints {
        set { 
            mMaxShieldPoints = value;
            pGui.updateText();
        }
        get { return mMaxShieldPoints; }

    }

    //FUEL
    int mFuel;
    public int fuel {
        set {
            mFuel = value;
            pGui.updateText();
            pAnimation.updateFuelGauge(mFuel, maxFuel);
        }
        get { return mFuel; }
    }
    int mMaxFuel;    //set by SpaceStation (upgradable)
    public int maxFuel {
        set {
            mMaxFuel = value;
            pGui.updateText();
            //NOTE: upgrading maxFuel will not call updateFuelGauge because it is assumed that a upgrade willa llways include a refill
        }
        get { return mMaxFuel; }
    }

    //CARGO
    int mMetal = 0;
    public int metal {
        set {
            mMetal = value;
            pGui.updateText();
        }
        get { return mMetal; }
    }
    int mMaxMetal;  //set by SpaceStation (upgradable)
    public int maxMetal {
        set {
            mMaxMetal = value;
            pGui.updateText();
        }
        get { return mMaxMetal; }
    }

    public bool addMetal(int v) {
        if((metal+v) <= maxMetal) {
            metal += v;
            return true;
        }
        return false;
    }

    //States
    public enum MovementState
    {
        undefined,
        idle,
        moving_forwards,
        moving_backwards,
        chargePrep,
        charge,
        travel,
        dead
    }
    private MovementState mMovementState = MovementState.undefined;
    public MovementState movementState {
        get { return mMovementState; }
        set {
            if (mMovementState != value) {
                mMovementState = value;
                pAnimation.updateEngines(mMovementState);
            }
        }
    }

    bool mInvulnerable;
    public bool invulnerable {
        set {
            mInvulnerable = value;
            pAnimation.updateInvulnerability(mInvulnerable);
        }
        get { return mInvulnerable; }
    }


    void Start()
    {
        pAnimation = GetComponent<PlayerAnimation>();
        pGui = GetComponent<PlayerGui>();
        movementState = PlayerState.MovementState.idle;
    }

    private void Update() {
        if((maxShieldPoints > 0) && (shieldPoints < maxShieldPoints)) {
            shieldRecoveryTimer += Time.deltaTime;
            if(shieldRecoveryTimer > shieldRecoveryTime) {
                shieldPoints += 1;
                shieldRecoveryTimer = 0f;
            }
        }
    }

    public bool takeDamage(int dmg) {
        switch(movementState) {
            case MovementState.charge:
                //No damage taken
                return false;
            default:
                if(!invulnerable) {
                    if (shieldPoints > 0)
                        shieldPoints -= dmg;
                    else
                        hullPoints -= dmg;
                }
                if (hullPoints <= 0) {
                    movementState = MovementState.dead;
                    GameOverScreen.SetActive(true);
                }
                return true;
        }
    }
}
