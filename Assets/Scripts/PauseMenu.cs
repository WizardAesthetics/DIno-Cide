using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool complete = false;

    public GameObject pauseMenuUI;
    public Animator transition;
    public float transitionTime = 4f;

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

        if (complete)
        {
            complete = false;
            Complete();
        }
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
        SceneManager.LoadScene("Menu");
    }

    public void QuitMenu()
    {
        Debug.Log("exit game");
        Application.Quit();
    }

    public void Complete()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings - 1)
        {
            StartCoroutine(Transit(0));
        }
        else
        {
<<<<<<< HEAD
            StartCoroutine(Transit(SceneManager.GetActiveScene().buildIndex + 1));
=======
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
>>>>>>> 3a7bf5586ad9d069937d63fbffc0de2987bc8406
        }
    }

    IEnumerator Transit(int levelIndex)
    {
        int x = levelIndex;
        Debug.Log("Donezo");
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(x);
    }
}
