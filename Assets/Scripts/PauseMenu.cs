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
    public float transitionTime = 1f;

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
        if (SceneManager.GetActiveScene().buildIndex >= 4)
        {
            StartCoroutine(Transit(0));
        }
        else
        {
            StartCoroutine(Transit(SceneManager.GetActiveScene().buildIndex));
        }
    }

    IEnumerator Transit(int levelIndex)
    {
        int x = levelIndex;
        Debug.Log("Donezo");
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Cutscene_"+x);
    }
}
