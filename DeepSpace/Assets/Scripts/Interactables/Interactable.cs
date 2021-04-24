using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    void Start()
    {
        //Nothing
    }

    void Update()
    {
        //Nothing
    }

    /// <summary>
    /// Interact with a collider (complex and will have impact on the rigidBody)
    /// </summary>
    /// <param name="other"></param>
    /// <param name="collision"></param>
    abstract public void interact(GameObject other, Collision2D collision);

    /// <summary>
    /// Interact with a Trigger (simple)
    /// </summary>
    /// <param name="other"></param>
    abstract public void interact(GameObject other);
}
