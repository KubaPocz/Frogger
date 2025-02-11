using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    public void ToHiScore()
    {
        SceneManager.LoadSceneAsync("Score");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
