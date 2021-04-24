using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public ParticleSystem[] defaultBackwardsEngines = new ParticleSystem[3];
    public ParticleSystem[] chargeBackwardsEngines = new ParticleSystem[3];
    public enum EngineState
    {
        undefined,
        on,
        off,
        charge
    }
    private EngineState engineState = EngineState.undefined;

    void Start()
    {
        setBackWardsEngineState(EngineState.off);
    }

    void Update()
    {
        //Nothing
    }

    private void setStateForAll(ParticleSystem[] system, bool e) {
        foreach (var s in system) {
            var emission = s.emission;
            emission.enabled = e;
        }
    }

    public void setBackWardsEngineState(EngineState nS) {
        if(nS != engineState) {
            switch (nS) {
                case EngineState.on:
                    setStateForAll(defaultBackwardsEngines, true);
                    setStateForAll(chargeBackwardsEngines, false);
                    break;
                case EngineState.off:
                    setStateForAll(defaultBackwardsEngines, false);
                    setStateForAll(chargeBackwardsEngines, false);
                    break;
                case EngineState.charge:
                    setStateForAll(defaultBackwardsEngines, false);
                    setStateForAll(chargeBackwardsEngines, true);
                    break;
            }
            engineState = nS;
        }
    }
}
