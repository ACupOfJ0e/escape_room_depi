using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
