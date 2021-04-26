using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SpaceStation : MonoBehaviour
{
    SpaceStationGui sGui;
    PlayerState pState;
    PlayerMovement pMovement;

    public bool enableFreeUpgrades = false;

    public Button repairButton;
    public Button reFuelButton;
    public Button storeMetalButton;

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

    private void updateButtonText(Button b, List<UpgradeLevel> l, int i, string name) {
        var t = b.GetComponentInChildren<Text>();
        Assert.IsNotNull(t);
        if ((i+1) < l.Count)
            t.text = string.Format("lvl {0} - {1} - {2}", l[i].mLevel, name, l[i+1].mCost);
        else
            t.text = string.Format("lvl {0} - {1} - max", l[i].mLevel, name);
    }

    //UPGRADE FUEL
    [Range(0,3)]
    public int startFuelLevel = 1;
    public Button fuelUpgradeButton;
    int mCurFuelLevel;
    int curFuelLevel {
        set {
            mCurFuelLevel = value;
            pState.maxFuel = fuelLevels[mCurFuelLevel].mValue;
            pState.fuel = pState.maxFuel;
            updateButtonText(fuelUpgradeButton, fuelLevels, mCurFuelLevel, "Fuel Capacity");
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
    public Button shieldUpgradeButton;
    int mCurShieldLevel;
    int curShieldLevel {
        set {
            mCurShieldLevel = value;
            pState.maxShieldPoints = shieldLevels[mCurShieldLevel].mValue;
            updateButtonText(shieldUpgradeButton, shieldLevels, mCurShieldLevel, "Shield");
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
    public Button cargoUpgradeButton;
    int mCurCargoLevel;
    int curCargoLevel {
        set {
            mCurCargoLevel = value;
            pState.maxMetal = cargoLevels[mCurCargoLevel].mValue;
            updateButtonText(cargoUpgradeButton, cargoLevels, mCurCargoLevel, "Cargo Capacity");
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
    public Button thrusterUpgradeButton;
    int mCurThrusterLevel = 0;
    int curThrusterLevel {
        set {
            mCurThrusterLevel = value;
            pMovement.movementForceForward = thrusterLevel[mCurThrusterLevel].mValue;
            updateButtonText(thrusterUpgradeButton, thrusterLevel, mCurThrusterLevel, "Thrusters");
        }
        get { return mCurThrusterLevel; }
    }
    List<UpgradeLevel> thrusterLevel = new List<UpgradeLevel> {
        new UpgradeLevel(0, 0,  0),
        new UpgradeLevel(1, 0, 800),
        new UpgradeLevel(2, 120, 1200),
        new UpgradeLevel(3, 250, 1500)};

    //RESOURCES
    public int startMetal = 0;
    int mMetal;
    public Text metalText;
    public int metal {
        set {
            mMetal = value;
            metalText.text = string.Format("Metal:  {0,3}", mMetal);
        }
        get { return mMetal; }
    }

    private void Start() {
        sGui = GetComponent<SpaceStationGui>();
        pState = FindObjectOfType<PlayerState>();
        pMovement = FindObjectOfType<PlayerMovement>();

        //Assign Buttons
        repairButton.onClick.AddListener(repairShip);
        reFuelButton.onClick.AddListener(refuelShip);
        storeMetalButton.onClick.AddListener(storeMetal);
        fuelUpgradeButton.onClick.AddListener(upgradeFuelCapacity);
        shieldUpgradeButton.onClick.AddListener(upgradeShield);
        cargoUpgradeButton.onClick.AddListener(upgradeCargoCapacity);
        thrusterUpgradeButton.onClick.AddListener(upgradeThrusters);

        //Init
        curFuelLevel = startFuelLevel;
        curShieldLevel = startShieldLevel;
        curCargoLevel = startCargoLevel;
        curThrusterLevel = startThrusterLevel;

        metal = startMetal;
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
            pay(fuelLevels[curFuelLevel+1].mCost);
            curFuelLevel += 1;
        }
    }

    public void upgradeShield() {
        if(checkCostAndLevel(shieldLevels, curShieldLevel)) {
            pay(shieldLevels[curShieldLevel+1].mCost);
            curShieldLevel += 1;
        }
    }

    public void upgradeCargoCapacity() {
        if(checkCostAndLevel(cargoLevels, curCargoLevel)) {
            pay(cargoLevels[curCargoLevel+1].mCost);
            curCargoLevel += 1;
        }
    }

    public void upgradeThrusters() {
        if(checkCostAndLevel(thrusterLevel, curThrusterLevel)) {
            pay(thrusterLevel[curThrusterLevel+1].mCost);
            curThrusterLevel += 1;
        }
    }
}
