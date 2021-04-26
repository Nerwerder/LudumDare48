using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public List<ParticleSystem> defaultBackwardsEngines = new List<ParticleSystem>();
    public List<ParticleSystem> chargeBackwardsEngines = new List<ParticleSystem>();
    public List<ParticleSystem> travelBackwardsEngines = new List<ParticleSystem>();
    public List<ParticleSystem> forwardsEngines = new List<ParticleSystem>();
    public List<GameObject> fuelGaugeSprites = new List<GameObject>();
    public GameObject mainShield;
    public GameObject invulnerableShield;

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

    public void updateInvulnerability(bool i) {
        if(i) {
            invulnerableShield.SetActive(true);
        } else {
            invulnerableShield.SetActive(false);
        }
    }

    public void updateShieldState(int sl) {
        if(sl > 0) {
            mainShield.SetActive(true);
        } else {
            mainShield.SetActive(false);
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

    private void setEngineState(bool d, bool t, bool c, bool f) {
        setStateForAll(defaultBackwardsEngines, d);
        setStateForAll(chargeBackwardsEngines, c);
        setStateForAll(travelBackwardsEngines, t);
        setStateForAll(forwardsEngines, f);
    }

    public void updateEngines(PlayerState.MovementState state) {
        switch(state) {
            case PlayerState.MovementState.idle:
            case PlayerState.MovementState.chargePrep:
            case PlayerState.MovementState.dead:
                setEngineState(false, false, false, false);
                break;
            case PlayerState.MovementState.moving_forwards:
                setEngineState(true, false, false, false);
                break;
            case PlayerState.MovementState.travel:
                setEngineState(false, true, false, false);
                break;
            case PlayerState.MovementState.charge:
                setEngineState(false, false, true, false);
                break;
            case PlayerState.MovementState.moving_backwards:
                setEngineState(false, false, false, true);
                break;
            default:
                throw new System.NotImplementedException();
        }
    }
}
