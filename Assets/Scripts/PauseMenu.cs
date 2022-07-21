using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject resumeButton;

    public GameObject pauseMenuUI;
    public AudioClip buttonHover, buttonClick;

    private AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!Boss.bossDefeated || !Stage2Boss.bossDefeated || !Stage3Boss.bossDefeated || !Stage4Boss.bossDefeated || !FinalBoss.bossDefeated)
        {
            if (Input.GetButtonDown("Submit"))
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
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void ClickContinue()
    {
        PlaySong(buttonClick);
        ContinueGame();
    }

    public void ContinueGame()
    {
        Resume();
    }

    public void LoadMainMenu()
    {
        PlaySong(buttonClick);
        Destroy(FindObjectOfType<GameManager>().gameObject);
        Destroy(FindObjectOfType<UIManager>().gameObject);
        Destroy(FindObjectOfType<MusicController>().gameObject);
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
