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
    public void exitGame()
    {
        Debug.Log("exit game");
        Application.Quit();
    }

}
