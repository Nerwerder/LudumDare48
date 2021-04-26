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

    private Text getText(string path) {
        var e = GameObject.Find(path);
        Assert.IsNotNull(e);
        var t = e.GetComponent<Text>();
        Assert.IsNotNull(t);
        return t;
    }

    void Start()
    {
        state = GetComponent<PlayerState>();
        Assert.IsNotNull(state);
        hullText = getText("Canvas/PlayerStatus/HullPoints");
        shieldText = getText("Canvas/PlayerStatus/ShieldPoints");
        fuelText = getText("Canvas/PlayerStatus/Fuel");
        metalText = getText("Canvas/PlayerStatus/Metal");
        updateText();
    }

    public void updateText() {
        hullText.text =   string.Format("Hull:   {0,03}|{1,03}", state.maxHullPoints, state.hullPoints);
        shieldText.text = string.Format("Shield: {0,03}|{1,03}", state.maxShieldPoints, state.shieldPoints);
        fuelText.text =   string.Format("Fuel:   {0,03}|{1,03}", state.maxFuel, state.fuel);
        metalText.text =  string.Format("Metal:  {0,03}|{1,03}", state.maxMetal, state.metal);
    }
}
