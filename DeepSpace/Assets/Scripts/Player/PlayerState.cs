using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
    public int hullPoints = 5;
    public int shieldPoints = 0;

    void Start()
    {    
    }

    void Update()
    {
    }  
    
    public void takeDamage(int dmg) {
        hullPoints -= dmg;
    }
       
}
