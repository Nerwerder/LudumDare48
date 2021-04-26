using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public List<ParticleSystem> defaultBackwardsEngines = new List<ParticleSystem>();
    public List<ParticleSystem> chargeBackwardsEngines = new List<ParticleSystem>();
    public List<ParticleSystem> forwardsEngines = new List<ParticleSystem>();
    public List<GameObject> fuelGaugeSprites = new List<GameObject>();
    public GameObject shield;

    void Start()
    {
    }

    void Update()
    {
        //Nothing
    }

    private void setStateForAll(List<ParticleSystem> system, bool e) {
        foreach (var s in system) {
            var emission = s.emission;
            emission.enabled = e;
        }
    }

    public void updateShieldState(int sl) {
        if(sl > 0) {
            shield.SetActive(true);
        } else {
            shield.SetActive(false);
        }
    }

    public void updateFuelGauge(int fuel, int maxFuel) {
        for(int k = 0; k < fuelGaugeSprites.Count; ++k) {
            if(((maxFuel / fuelGaugeSprites.Count)*k) < fuel) {
                fuelGaugeSprites[k].SetActive(true);
            } else {
                fuelGaugeSprites[k].SetActive(false);
            }
        }
    }

    public void updateEngines(PlayerState.MovementState state) {
        switch(state) {
            case PlayerState.MovementState.idle:
            case PlayerState.MovementState.chargePrep:
                setStateForAll(defaultBackwardsEngines, false);
                setStateForAll(chargeBackwardsEngines, false);
                setStateForAll(forwardsEngines, false);
                break;
            case PlayerState.MovementState.moving_forwards:
                setStateForAll(defaultBackwardsEngines, true);
                setStateForAll(chargeBackwardsEngines, false);
                setStateForAll(forwardsEngines, false);
                break;
            case PlayerState.MovementState.moving_backwards:
                setStateForAll(defaultBackwardsEngines, false);
                setStateForAll(chargeBackwardsEngines, false);
                setStateForAll(forwardsEngines, true);
                break;
            case PlayerState.MovementState.charge:
                setStateForAll(defaultBackwardsEngines, false);
                setStateForAll(chargeBackwardsEngines, true);
                setStateForAll(forwardsEngines, false);
                break;
            default:
                throw new System.NotImplementedException();

        }
    }
}
