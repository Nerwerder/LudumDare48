using UnityEngine;
using UnityEngine.Assertions;

public class SpaceStation : MonoBehaviour
{
    SpaceStationGui sGui;
    PlayerState pState;

    int mMetal;
    public int metal {
        set {
            mMetal = value;
            sGui.updateText();
        }
        get { return mMetal; }
    }

    private void Start() {
        sGui = GetComponent<SpaceStationGui>();
        Assert.IsNotNull(sGui);
        pState = FindObjectOfType<PlayerState>();
        Assert.IsNotNull(pState);
    }

    public void interact() {
        sGui.showGui();
    }

    public void repairShip() {
        pState.hullPoints = pState.maxHullPoints;
    }

    public void refuelShip() {
        pState.fuel = pState.maxFuel;
    }

    public void storeMetal() {
        metal += pState.metal;
        pState.metal = 0;
    }
}
