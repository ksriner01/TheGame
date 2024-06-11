using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public static bool GameIsWon = false;

    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    public Health playerHealth;
    public GameObject player;
    public Scoring score;
    public GameObject tutorial;

    public void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
        if (playerHealth.currentHealth == 0.0f) { 
            GameOver();
        }
        if (score.score == score.maxScore)
        {
            GameWon();
        }
    }

    public void GameOver()
    {
        tutorial.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true;
    }

    public void GameWon()
    {
        tutorial.SetActive(false);
        victoryUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsWon = true;
    }

    public void Resume()
    {
        tutorial.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        tutorial.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1f;
        transform.position = player.transform.position;
        playerHealth.Respawn();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
