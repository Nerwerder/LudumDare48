using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpaceStation : MonoBehaviour
{
    SpaceStationGui sGui;
    PlayerState pState;
    PlayerMovement pMovement;

    public bool enableFreeUpgrades = false;

    public struct UpgradeLevel
    {
        public int mLevel;
        public int mCost;
        public int mValue;
        public UpgradeLevel(int l, int c, int v) {
            mLevel = l;
            mCost = c;
            mValue = v;
        }
    }

    //UPGRADE FUEL
    [Range(0,3)]
    public int startFuelLevel = 1;
    int mCurFuelLevel;
    int curFuelLevel {
        set {
            mCurFuelLevel = value;
            pState.maxFuel = fuelLevels[mCurFuelLevel].mValue;
            pState.fuel = pState.maxFuel;
        }
        get { return mCurFuelLevel; }
    }
    List<UpgradeLevel> fuelLevels = new List<UpgradeLevel>() {
        new UpgradeLevel(0, 0, 0),
        new UpgradeLevel(1, 0, 100),
        new UpgradeLevel(2, 80, 200),
        new UpgradeLevel(3, 120, 300)};

    //UPGRADE SHIELD
    [Range(0, 3)]
    public int startShieldLevel = 0;
    int mCurShieldLevel;
    int curShieldLevel {
        set {
            mCurShieldLevel = value;
            pState.maxShieldPoints = shieldLevels[mCurShieldLevel].mValue;
        }
        get { return mCurShieldLevel; }
    }
    List<UpgradeLevel> shieldLevels = new List<UpgradeLevel> {
        new UpgradeLevel(0, 0,  0),
        new UpgradeLevel(1, 30, 1),
        new UpgradeLevel(2, 50, 3),
        new UpgradeLevel(3, 80, 5)};

    //UPGRADE CARGO
    [Range(0, 3)]
    public int startCargoLevel = 1;
    int mCurCargoLevel;
    int curCargoLevel {
        set {
            mCurCargoLevel = value;
            pState.maxMetal = cargoLevels[mCurCargoLevel].mValue;
        }
        get { return mCurCargoLevel; }
    }
    List<UpgradeLevel> cargoLevels = new List<UpgradeLevel> {
        new UpgradeLevel(0, 0,  0),
        new UpgradeLevel(1, 0, 50),
        new UpgradeLevel(2, 80, 100),
        new UpgradeLevel(3, 120, 250)};

    //UPGRADE THRUSTER
    [Range(0, 3)]
    public int startThrusterLevel = 1;
    int mCurThrusterLevel = 0;
    int curThrusterLevel {
        set {
            mCurThrusterLevel = value;
            pMovement.movementForceForward = thrusterLevel[mCurThrusterLevel].mValue;
        }
        get { return mCurThrusterLevel; }
    }
    List<UpgradeLevel> thrusterLevel = new List<UpgradeLevel> {
        new UpgradeLevel(0, 0,  0),
        new UpgradeLevel(1, 0, 800),
        new UpgradeLevel(2, 120, 1200),
        new UpgradeLevel(3, 250, 1500)};

    //RESOURCES
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
        pState = FindObjectOfType<PlayerState>();
        pMovement = FindObjectOfType<PlayerMovement>();

        //Init
        curFuelLevel = startFuelLevel;
        curShieldLevel = startShieldLevel;
        curCargoLevel = startCargoLevel;
        curThrusterLevel = startThrusterLevel;
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

    private bool checkCostAndLevel(List<UpgradeLevel> list, int i) {
        return ((metal > list[i].mCost) || (enableFreeUpgrades)) && ((i+1) < list.Count);
    }
    private void pay(int price) {
        metal -= enableFreeUpgrades ? 0 : price;
    }

    public void upgradeFuelCapacity() {
        if(checkCostAndLevel(fuelLevels, curFuelLevel)) {
            pay(fuelLevels[curFuelLevel].mCost);
            curFuelLevel += 1;
        }
    }

    public void upgradeShield() {
        if(checkCostAndLevel(shieldLevels, curShieldLevel)) {
            pay(shieldLevels[curShieldLevel].mCost);
            curShieldLevel += 1;
        }
    }

    public void upgradeCargoCapacity() {
        if(checkCostAndLevel(cargoLevels, curCargoLevel)) {
            pay(cargoLevels[curCargoLevel].mCost);
            curCargoLevel += 1;
        }
    }

    public void upgradeThrusters() {
        if(checkCostAndLevel(thrusterLevel, curThrusterLevel)) {
            pay(thrusterLevel[curThrusterLevel].mCost);
            curThrusterLevel += 1;
        }
    }
}
