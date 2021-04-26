using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy
{
    void Update() {
        if (!isActive())
            return;
        turnToTaget();
        rb.AddForce(transform.right * Time.deltaTime * movementForce);
    }
}
