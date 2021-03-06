using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SpaceStationGui : MonoBehaviour
{
    Transform player;
    PlayerState state;
    public GameObject stationMenu = null;
    public float disableGuiDistance = 50f;

    void Start() {
        var pc = FindObjectOfType<PlayerController>();
        state = FindObjectOfType<PlayerState>();
        Assert.IsNotNull(pc);
        player = pc.transform;
    }

    private void Update() {
        if(stationMenu.activeSelf && (Vector3.Distance(player.position, transform.position) > disableGuiDistance)) {
            stationMenu.SetActive(false);
            state.invulnerable = false;
        }
    }

    public void showGui() {
        if(stationMenu.activeSelf) {
            stationMenu.SetActive(false);
            state.invulnerable = false;
        } else {
            stationMenu.SetActive(true);
            state.invulnerable = true;
        }
    }
}
