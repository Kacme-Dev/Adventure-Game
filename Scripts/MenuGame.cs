using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    private bool isGamePaused = false;
    public Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        ESCPressed();
    }

    public void RestartGame()
    {
        ScoreManager.instance.ResetAllScores();
        ScoreManager.instance.SetRestartFlag(true);

        SceneManager.LoadScene("Level_01");
    }

    public void StartNewGame()
    {
        ScoreManager.instance.ResetAllScores();
        Player.instance.ResetPlayer();
        SceneManager.LoadScene("Level_01");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ESCPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                PausedGame();
            }

            else
            {
                ResumeGame();
            }
        }
    }

    private void PausedGame()
    {
        Time.timeScale = 0.0f;
        isGamePaused = true;
        player.controlsEnabled = false;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isGamePaused = false;
        player.controlsEnabled = true;
    }



}
