using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Button restartButton;
    public Button exitButton;
    private SpaceStationGui sGui;
    public GameObject helpPanel;

    void Start()
    {
        restartButton.onClick.AddListener(restartGame);
        exitButton.onClick.AddListener(exitGame);
        sGui = FindObjectOfType<SpaceStationGui>();
    }

    void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void exitGame() {
        Application.Quit();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!sGui.stationMenu.activeSelf) {
                Application.Quit();
            }
        }
        if(Input.GetKeyDown(KeyCode.H)) {
            helpPanel.SetActive(!helpPanel.activeSelf);
        }
    }
}
