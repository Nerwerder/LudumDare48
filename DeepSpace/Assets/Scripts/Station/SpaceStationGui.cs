using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SpaceStationGui : MonoBehaviour
{
    Transform player;
    public GameObject stationMenu = null;
    public float disableGuiDistance = 50f;

    void Start() {
        var pc = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(pc);
        player = pc.transform;
    }

    private void Update() {
        if(stationMenu.activeSelf && (Vector3.Distance(player.position, transform.position) > disableGuiDistance)) {
            stationMenu.SetActive(false);
        }
    }

    public void showGui() {
        stationMenu.SetActive(!stationMenu.activeSelf);
    }
}
