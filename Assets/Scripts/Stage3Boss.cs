using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Boss : Enemy
{ 
    public GameObject specialAttackObj;
    public float minSpecialTime, maxSpecialTime;
    public AudioClip bossStart, specialAttackSound;
    public AudioClip masakiVictory, dyanaVictory, kammyVictory, alecVictory;

    private MusicController music;
    private AudioSource audioS;
    public static bool winLevel;
    public static bool bossDefeated = false;

    void Awake()
    {
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
            GameObject tempSpecial = Instantiate(specialAttackObj, transform.position, transform.rotation);
            if (facingRight)
            {
                tempSpecial.GetComponent<SpecialAttack>().direction = 1;
            }
            else
            {
                tempSpecial.GetComponent<SpecialAttack>().direction = -1;
            }
            Invoke("SpecialAttack", Random.Range(minSpecialTime, maxSpecialTime));
        }
    }

    void BossDefeated()
    {
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
        SceneManager.LoadScene(3);
        Stage2Boss.winLevel = false;
        bossDefeated = false;
        Destroy(FindObjectOfType<UIManager>().gameObject);
    }

    public void PlayBoss(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }
}
