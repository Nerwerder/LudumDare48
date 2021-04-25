using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerGui : MonoBehaviour
{
    Text hullText = null;
    Text shieldText = null;
    Text fuelText = null;
    Text metalText = null;

    PlayerState state = null;

    void Start()
    {
        state = GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        var hull = GameObject.Find("Canvas/HullPoints");
        var shield = GameObject.Find("Canvas/ShieldPoints");
        var fuel = GameObject.Find("Canvas/PlayerFuel");
        var metal = GameObject.Find("Canvas/PlayerMetal");
        Assert.IsNotNull(hull);
        Assert.IsNotNull(shield);
        Assert.IsNotNull(fuel);
        Assert.IsNotNull(metal);
        hullText = hull.GetComponent<Text>();
        shieldText = shield.GetComponent<Text>();
        fuelText = fuel.GetComponent<Text>();
        metalText = metal.GetComponent<Text>();
        Assert.IsNotNull(hullText);
        Assert.IsNotNull(shieldText);
        Assert.IsNotNull(fuelText);
        Assert.IsNotNull(metalText);
        updateText();
    }

    public void updateText() {
        hullText.text = string.Format("Hull:   {0,3}|{1,3}", state.maxHullPoints, state.hullPoints);
        shieldText.text = string.Format("Shield: {0,3}|{1,3}", state.maxShieldPoints, state.shieldPoints);
        fuelText.text = string.Format("Fuel:   {0,3}|{1,3}", state.maxFuel, state.fuel);
        metalText.text = string.Format("Metal:  {0,3}|{1,3}", state.maxMetal, state.metal);
    }
}
