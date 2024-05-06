using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCanvas,
        overPanel,
        winPanel;

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameWin()
    {
        Debug.Log("End Game Controller: GameWin");
        winPanel.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void GameLose()
    {
        mainCanvas.SetActive(false);
        overPanel.SetActive(true);
    }
}
