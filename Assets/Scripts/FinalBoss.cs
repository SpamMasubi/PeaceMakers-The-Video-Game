using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : Enemy
{
    public float minSpecialTime, maxSpecialTime;
    public AudioClip bossStart, specialAttackSound;
    public AudioClip masakiVictory, dyanaVictory, kammyVictory, alecVictory;

    private MusicController music;
    private AudioSource audioS;
    public static bool winLevel;
    public static bool bossDefeated = false;
    public static bool finalBoss = false;

    void Awake()
    {
        finalBoss = true;
        winLevel = false;
        music = FindObjectOfType<MusicController>();
        audioS = GetComponent<AudioSource>();
        Invoke("SpecialAttack", Random.Range(minSpecialTime, maxSpecialTime));
        music.PlaySong(music.bossSong);
        PlayBoss(bossStart);
    }

    void SpecialAttack()
    {
        if (!isDead && comboCount == 0)
        {
            anim.SetTrigger("Special");
            PlayBoss(specialAttackSound);
            if (facingRight)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(20, 0, 0), ForceMode.Impulse);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(-20, 0, 0), ForceMode.Impulse);
            }
            Invoke("SpecialAttack", Random.Range(minSpecialTime, maxSpecialTime));
        }
    }

    void BossDefeated()
    {
        FindObjectOfType<UIManager>().enemyUI.SetActive(false);
        finalBoss = false;
        music.PlaySong(music.levelCompleteSong);
        winLevel = true;
        bossDefeated = true;
        if (FindObjectOfType<GameManager>().characterIndex == 1)
        {
            PlayBoss(masakiVictory);
        }
        else if (FindObjectOfType<GameManager>().characterIndex == 2)
        {
            PlayBoss(dyanaVictory);
        }
        else if (FindObjectOfType<GameManager>().characterIndex == 3)
        {
            PlayBoss(kammyVictory);
        }
        else if (FindObjectOfType<GameManager>().characterIndex == 4)
        {
            PlayBoss(alecVictory);
        }
        FindObjectOfType<UIManager>().UpdateDisplayMessage("Level Clear");
        Invoke("LoadScene", 8f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Stage4Boss.winLevel = false;
        bossDefeated = false;
        winLevel = false;
        Destroy(FindObjectOfType<UIManager>().gameObject);
    }

    public void PlayBoss(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }
}
