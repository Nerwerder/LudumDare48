using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy
{
    void Update()
    {
        turnToTaget();
        rb.AddForce(transform.right * Time.deltaTime * movementForce);
    }
}
