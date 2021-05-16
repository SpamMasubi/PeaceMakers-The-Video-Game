using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{

    public GameObject axe;
    public float minAxeTime, maxAxeTime;
    public AudioClip bossStart, throwAxe;
    public AudioClip masakiVictory, dyanaVictory, kammyVictory, alecVictory;

    private MusicController music;
    private AudioSource audioS;
    public static bool winLevel;
    public static bool startLevel2;

    void Awake()
    {
        winLevel = false;
        Invoke("ThrowAxe", Random.Range(minAxeTime, maxAxeTime));
        music = FindObjectOfType<MusicController>();
        audioS = GetComponent<AudioSource>();
        music.PlaySong(music.bossSong);
        PlayBoss(bossStart);
    }

    void ThrowAxe()
    {

        if (!isDead && !damaged)
        {
            anim.SetTrigger("Axe");
            PlayBoss(throwAxe);
            GameObject tempAxe = Instantiate(axe, transform.position, transform.rotation);
            if (facingRight)
            {
                tempAxe.GetComponent<Axe>().direction = 1;
            }
            else
            {
                tempAxe.GetComponent<Axe>().direction = -1;
            }
            Invoke("ThrowAxe", Random.Range(minAxeTime, maxAxeTime));
        }

    }

    void BossDefeated()
    {
        music.PlaySong(music.levelCompleteSong);
        winLevel = true;
        if(FindObjectOfType<GameManager>().characterIndex == 1)
        {
            PlayBoss(masakiVictory);
        }
        else if(FindObjectOfType<GameManager>().characterIndex == 2)
        {
            PlayBoss(dyanaVictory);
        }
        else if(FindObjectOfType<GameManager>().characterIndex == 3)
        {
            PlayBoss(kammyVictory);
        }
        else if(FindObjectOfType<GameManager>().characterIndex == 4)
        {
            PlayBoss(alecVictory);
        }
        FindObjectOfType<UIManager>().UpdateDisplayMessage("Level Clear");
        Invoke("LoadScene", 8f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(3);
        startLevel2 = true;
        winLevel = false;
    }

    public void PlayBoss(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }
}
