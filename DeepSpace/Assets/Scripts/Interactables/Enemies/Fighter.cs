using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy
{
    void FixedUpdate()
    {
        if(isActive()) {
            turnToTaget();
            rb.AddForce(transform.right * Time.deltaTime * movementForce);
        }
    }
}
