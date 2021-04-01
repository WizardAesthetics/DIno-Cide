using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
<<<<<<< HEAD
    public static bool complete = false;

    public GameObject pauseMenuUI;
    public GameObject completeOverlay;
=======

    public GameObject pauseMenuUI;
>>>>>>> origin/BlakesBranch

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

<<<<<<< HEAD
        if (complete) { Complete(); }
=======
>>>>>>> origin/BlakesBranch
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
<<<<<<< HEAD
        SceneManager.LoadScene("Menu");
=======
        //SceneManager.LoadScene("Menu");
>>>>>>> origin/BlakesBranch
    }

    public void QuitMenu()
    {
<<<<<<< HEAD
        Debug.Log("exit game");
        Application.Quit();
    }

    public void Complete()
    {
        completeOverlay.SetActive(true);
        Invoke("LoadMenu", 2f);
    }
=======
        Application.Quit();
    }
>>>>>>> origin/BlakesBranch
}
