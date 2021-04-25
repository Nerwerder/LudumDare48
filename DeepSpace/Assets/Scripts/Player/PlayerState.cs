using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public enum MovementState
    {
        moving,
        chargePrep,
        charge
    }
    private MovementState mMovementState = MovementState.moving;
    public MovementState movementState {
        get { return mMovementState;  }
        set { mMovementState = value; }
    }

    //Life
    public int mHullPoints = 10;
    public int hullPoints {
        set {
            mHullPoints = Mathf.Max(value, 0);
            updateText();
        }
        get { return mHullPoints; }
    }
    public int maxHullPoints = 10;

    public int mShieldPoints = 0;
    public int shieldPoints {
        set {
            mShieldPoints = Mathf.Max(value, 0);
            updateText();
        }
        get { return mShieldPoints; }
    }
    public int maxShieldPoints = 5;

    public int mFuel = 100;
    public int fuel {
        set {
            mFuel = value;
            updateText();
        }
        get { return mFuel; }
    }
    public int maxFuel = 100;

    public int mMetal = 0;
    public int metal {
        set {
            mMetal = value;
            updateText();
        }
        get { return mMetal; }
    }
    public int maxMetal = 100;

    Text hullText = null;
    Text shieldText = null;
    Text fuelText = null;
    Text metalText = null;

    void Start()
    {
        var hull = GameObject.Find("Canvas/HullPoints");
        var shield = GameObject.Find("Canvas/ShieldPoints");
        var fuel = GameObject.Find("Canvas/PlayerFuel");
        var metal = GameObject.Find("Canvas/PlayerMetal");
        Assert.IsNotNull(hull);
        Assert.IsNotNull(shield);
        Assert.IsNotNull(fuel);
        Assert.IsNotNull(metal);
        hullText = hull.GetComponent<Text>();
        shieldText = shield.GetComponent<Text>();
        fuelText = fuel.GetComponent<Text>();
        metalText = metal.GetComponent<Text>();
        Assert.IsNotNull(hullText);
        Assert.IsNotNull(shieldText);
        Assert.IsNotNull(fuelText);
        Assert.IsNotNull(metalText);
        updateText();
    }

    private void updateText() {
        hullText.text =   string.Format("Hull:   {0,3}|{1,3}", maxHullPoints, hullPoints);
        shieldText.text = string.Format("Shield: {0,3}|{1,3}", maxShieldPoints, shieldPoints);
        fuelText.text =   string.Format("Fuel:   {0,3}|{1,3}", maxFuel, fuel);
        metalText.text  = string.Format("Metal:  {0,3}|{1,3}", maxMetal, metal);
    }
    
    public bool takeDamage(int dmg) {
        switch(movementState) {
            case MovementState.chargePrep:
            case MovementState.moving:
                if (shieldPoints > 0)
                    shieldPoints -= dmg;
                else
                    hullPoints -= dmg;

                if(hullPoints <= 0) {
                    //TODO: GameOver
                }
                return true;
            default:
                //No damage taken
                return false;
        }
    }
}
