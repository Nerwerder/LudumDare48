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

    abstract public void interact(GameObject other, Collision2D collision);
}
