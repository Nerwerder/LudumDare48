using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Button restartButton;
    public Button exitButton;

    void Start()
    {
        restartButton.onClick.AddListener(restartGame);
        exitButton.onClick.AddListener(exitGame);
    }

    void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void exitGame() {
        Application.Quit();
    }
}
