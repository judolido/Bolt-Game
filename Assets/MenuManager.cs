using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject Starting_screen;  // Reference to the Main Menu Panel
    public GameObject Settings_panel;  // Reference to the Settings Panel

    // Function to open the Settings Panel and hide the Main Menu Panel
    public void OpenSettingsPanel()
    {
        Starting_screen.SetActive(true);  // Hide Main Menu
        Settings_panel.SetActive(true);   // Show Settings Panel
    }

    // Function to return to the Main Menu from the Settings Panel
    public void BackToMainMenu()
    {
        Settings_panel.SetActive(false);  // Hide Settings Panel
        Starting_screen.SetActive(true);   // Show Main Menu
    }

    public void StartGame()
    {
     
  //      SceneManager.LoadScene("InWorld");
    }

    // Function to quit the game
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");  // This will show in the editor log for testing
        Application.Quit();  // This will close the game when built (but won't work in the editor)
    }
}
