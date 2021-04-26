using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SpaceStationGui : MonoBehaviour
{
    SpaceStation sStation;
    Transform player;
    public GameObject stationMenu = null;
    public float disableGuiDistance = 50f;

    const string backgroundPath = "Canvas/StationMenu/Backgound/";
    const string upgradePath = backgroundPath + "ShipUpgrades/";
    Text metalText;
    Text fuelUpgradeButtonText;
    Text shieldUpgradeButtonText;
    Text cargoUpgradeButtonText;
    Text thrusterUpgradeButtonText;

    private Text getText(string path) {
        var e = GameObject.Find(path);
        Assert.IsNotNull(e);
        var t = e.GetComponent<Text>();
        Assert.IsNotNull(t);
        return t;
    }

    void Start() {
        sStation = GetComponent<SpaceStation>();
        var pc = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(pc);
        player = pc.transform;
        Assert.IsNotNull(sStation);
        Assert.IsNotNull(stationMenu);

        //Text Gui
        metalText = getText(backgroundPath + "StationStatus/Metal");
        fuelUpgradeButtonText = getText(upgradePath + "FuelCapacity/Text");
        shieldUpgradeButtonText = getText(upgradePath + "Shield/Text");
        cargoUpgradeButtonText = getText(upgradePath + "CargoCapacity/Text");
        thrusterUpgradeButtonText = getText(upgradePath + "Thrusters/Text");

        updateText();
    }

    private void Update() {
        if(stationMenu.activeSelf && (Vector3.Distance(player.position, transform.position) > disableGuiDistance)) {
            stationMenu.SetActive(false);
        }
    }

    public void updateText() {
        metalText.text = string.Format("Metal:  {0,3}", sStation.metal);
    }

    public void showGui() {
        stationMenu.SetActive(!stationMenu.activeSelf);
    }
}
