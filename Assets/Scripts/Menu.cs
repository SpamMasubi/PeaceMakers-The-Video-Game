using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioSource audioS;
    private bool selectionMade = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            PlaySound();
            selectionMade = true;
            anim.SetBool("Selection", selectionMade);
            Invoke("LoadScene", 1f);
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            PlaySound();
            Application.Quit();
        }
    }

    void LoadScene()
    {
        selectionMade = false;
        anim.SetBool("Selection", selectionMade);
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
