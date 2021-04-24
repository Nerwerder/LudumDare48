using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int hullPoints = 1;
    public int shieldPoints = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
