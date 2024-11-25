using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadWorld()
    {
        SceneManager.LoadScene("InWorld");
        
    }

    // Function to quit the game
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");  // This will show in the editor log for testing
        Application.Quit();  // This will close the game when built (but won't work in the editor)
    }
}
