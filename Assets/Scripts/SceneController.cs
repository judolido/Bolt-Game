using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LoadWorld()
    {
        SceneManager.LoadSceneAsync("World");
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    // Function to quit the game
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");  // This will show in the editor log for testing
        Application.Quit();  // This will close the game when built (but won't work in the editor)
    }
}
