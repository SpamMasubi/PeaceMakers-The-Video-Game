using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMidBoss : Enemy
{
    public float minSpecialTime, maxSpecialTime;
    public AudioClip bossStart, specialAttackSound;
    private MusicController music;
    private AudioSource audioS;
    public static bool winLevel;
    public static bool bossDefeated = false;
    public static bool midBoss = false;

    void Awake()
    {
        midBoss = true;
        winLevel = false;
        music = FindObjectOfType<MusicController>();
        audioS = GetComponent<AudioSource>();
        Invoke("SpecialAttack", Random.Range(minSpecialTime, maxSpecialTime));
        music.PlaySong(music.bossSong);
        PlayBoss(bossStart);
    }

    void SpecialAttack()
    {
        if (!isDead)
        {
            anim.SetTrigger("Special");
            PlayBoss(specialAttackSound);
            currentSpeed = 0;
            Invoke("SpecialAttack", Random.Range(minSpecialTime, maxSpecialTime));
        }
    }

    public void PlayBoss(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }
}
