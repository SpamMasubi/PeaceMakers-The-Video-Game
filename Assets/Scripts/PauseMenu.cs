using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public AudioClip buttonHover, buttonClick;

    private AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

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

    public void ClickContinue()
    {
        PlaySong(buttonClick);
        ContinueGame();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        Resume();
    }

    public void LoadMainMenu()
    {
        PlaySong(buttonClick);
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OnMouseOver()
    {
        PlaySong(buttonHover);
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }
}
