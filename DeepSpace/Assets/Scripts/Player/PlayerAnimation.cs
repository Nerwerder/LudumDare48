using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public ParticleSystem[] backwardsEngines = new ParticleSystem[3];
    private bool backwardsEnginesEnabled = true;

    void Start()
    {
        setBackWardsEngineState(false);
    }

    void Update()
    {
        //Nothing
    }

    public void setBackWardsEngineState(bool enabled) {
        if(enabled !=  backwardsEnginesEnabled) {
            foreach (var engine in backwardsEngines) {
                var emission = engine.emission;
                emission.enabled = enabled;
            }
            backwardsEnginesEnabled = enabled;
        }
    }
}
