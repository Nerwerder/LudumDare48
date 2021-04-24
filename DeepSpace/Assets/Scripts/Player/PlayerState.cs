using System.Collections;
using System.Collections.Generic;
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
    public int hullPoints = 10;
    public int maxHullPoints = 10;
    public int shieldPoints = 0;
    public int maxShieldPoints = 5;
    public int metal = 0;
    public int maxMetal = 100;

    Text hullText = null;
    Text shieldText = null;
    Text metalText = null;

    void Start()
    {
        var hull = GameObject.Find("Canvas/HullPoints");
        var shield = GameObject.Find("Canvas/ShieldPoints");
        var metal = GameObject.Find("Canvas/Metal");
        Assert.IsNotNull(hull);
        Assert.IsNotNull(shield);
        Assert.IsNotNull(metal);
        hullText = hull.GetComponent<Text>();
        shieldText = shield.GetComponent<Text>();
        metalText = metal.GetComponent<Text>();
        Assert.IsNotNull(hullText);
        Assert.IsNotNull(shieldText);
        Assert.IsNotNull(metalText);
        updateText();
    }

    private void updateText() {
        hullText.text =   string.Format("Hull:   {0,3} | {1,3}", maxHullPoints, hullPoints);
        shieldText.text = string.Format("Shield: {0,3} | {1,3}", maxShieldPoints, shieldPoints);
        metalText.text  = string.Format("Metal:  {0,3} | {1,3}", maxMetal, metal);
    }

    void Update()
    {
    }  
    
    public void takeDamage(int dmg) {
        hullPoints -= dmg;
        updateText();
    }

    public bool addMetal(int mtl) {
        metal += mtl;
        updateText();
        return true;
    }
       
}
