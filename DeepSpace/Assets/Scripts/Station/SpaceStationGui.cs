using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SpaceStationGui : MonoBehaviour
{
    SpaceStation sStation;
    Text metalText = null;
    Transform player;
    public GameObject stationMenu = null;
    public float disableGuiDistance = 50f;

    void Start() {
        sStation = GetComponent<SpaceStation>();
        Assert.IsNotNull(sStation);
        Assert.IsNotNull(stationMenu);

        var pc = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(pc);
        player = pc.transform;

        //Text Gui
        var metal = GameObject.Find("Canvas/StationStatus/Metal");
        Assert.IsNotNull(metal);
        metalText = metal.GetComponent<Text>();
        Assert.IsNotNull(metalText);
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
