using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private AudioSource audioS;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(FindObjectOfType<GameManager>().gameObject);
        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlaySound();
            Invoke("LoadScene", 1f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void PlaySound()
    {
        if (!audioS.isPlaying)
        {
            audioS.Play();
        }
    }
}
