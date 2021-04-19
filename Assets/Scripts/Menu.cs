using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadInstuctions()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void loadSettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }

    public void exitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
    public void LoadLevelEndless()
    {
        Debug.Log("Entered Endless Mode");
        SceneManager.LoadScene("Level_4");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level_3");
    }
}
