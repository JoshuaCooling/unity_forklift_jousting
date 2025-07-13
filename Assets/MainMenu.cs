using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Replace with your game scene name
        SceneManager.LoadScene("game_scene");
    }

    public void SplitScreen()
    {
        SceneManager.LoadScene("split_screen_scene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
