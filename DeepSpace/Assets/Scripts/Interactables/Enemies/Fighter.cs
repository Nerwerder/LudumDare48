using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy
{
    void FixedUpdate()
    {
        turnToTaget();
        rb.AddForce(transform.right * Time.deltaTime * movementForce);
    }
}
