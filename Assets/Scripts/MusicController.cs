using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioClip levelSong, bossSong, levelCompleteSong, gameOverSong;

    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        PlaySong(levelSong);

    }

    private void Update()
    {
        if(PauseMenu.GameIsPaused)
        {
            audioS.Pause();
        }
        else
        {
            audioS.UnPause();
        }
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play(); 
    }
}
