using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    public Image masakiImage, dyanaImage, kammyImage, alecImage;
    public Animator masakiAnim, dyanaAnim, kammyAnim, alecAnim;

    private Color defaultColor;
    private int characterIndex;
    private AudioSource audioS;
    public AudioClip masakiStart, dyanaStart, kammyStart, alecStart;
    private bool charSelect = false;

    // Start is called before the first frame update
    void Start()
    {
        characterIndex = 1;
        audioS = GetComponent<AudioSource>();
        defaultColor = dyanaImage.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (!charSelect)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && characterIndex > 1)
            {
                characterIndex -= 1;
                PlaySound();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && characterIndex < 4)
            {
                characterIndex += 1;
                PlaySound();
            }

            if (characterIndex == 1)
            {
                masakiImage.color = Color.red;
                masakiAnim.SetBool("Attack", true);
                dyanaImage.color = defaultColor;
                dyanaAnim.SetBool("Attack", false);
                kammyImage.color = defaultColor;
                kammyAnim.SetBool("Attack", false);
                alecImage.color = defaultColor;
                alecAnim.SetBool("Attack", false);
            }
            else if (characterIndex == 2)
            {
                masakiImage.color = defaultColor;
                masakiAnim.SetBool("Attack", false);
                dyanaImage.color = Color.green;
                dyanaAnim.SetBool("Attack", true);
                kammyImage.color = defaultColor;
                kammyAnim.SetBool("Attack", false);
                alecImage.color = defaultColor;
                alecAnim.SetBool("Attack", false);
            }
            else if (characterIndex == 3)
            {
                masakiImage.color = defaultColor;
                masakiAnim.SetBool("Attack", false);
                dyanaImage.color = defaultColor;
                dyanaAnim.SetBool("Attack", false);
                kammyImage.color = Color.blue;
                kammyAnim.SetBool("Attack", true);
                alecImage.color = defaultColor;
                alecAnim.SetBool("Attack", false);
            }
            else if (characterIndex == 4)
            {
                masakiImage.color = defaultColor;
                masakiAnim.SetBool("Attack", false);
                dyanaImage.color = defaultColor;
                dyanaAnim.SetBool("Attack", false);
                kammyImage.color = defaultColor;
                kammyAnim.SetBool("Attack", false);
                alecImage.color = Color.yellow;
                alecAnim.SetBool("Attack", true);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                charSelect = true;
                FindObjectOfType<GameManager>().characterIndex = characterIndex;
                if (characterIndex == 1)
                {
                    PlayCharacterStart(masakiStart);
                    masakiAnim.SetBool("Attack", false);
                }
                else if (characterIndex == 2)
                {
                    PlayCharacterStart(dyanaStart);
                    dyanaAnim.SetBool("Attack", false);
                }
                else if (characterIndex == 3)
                {
                    PlayCharacterStart(kammyStart);
                    kammyAnim.SetBool("Attack", false);
                }
                else if(characterIndex == 4)
                {
                    PlayCharacterStart(alecStart);
                    alecAnim.SetBool("Attack", false);
                }
                Invoke("LoadScene", 2f);
            }

            if (Input.GetButtonDown("Fire2")){
                SceneManager.LoadScene(1);
            }
        }
    }
    void PlaySound()
    {
        if (!audioS.isPlaying)
        {
            audioS.Play();
        }
    }

    public void PlayCharacterStart(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        charSelect = false;
    }
}
