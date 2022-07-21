using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    public Image masakiImage, dyanaImage, kammyImage, alecImage;
    public Animator masakiAnim, dyanaAnim, kammyAnim, alecAnim;

    bool isMoving;

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
            float selectionAxis = Input.GetAxis("Horizontal");

            if (selectionAxis < 0)
            {
                MoveSelection("left");
            }
            else if (selectionAxis > 0)
            {
                MoveSelection("right");
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

            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Jump"))
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

            if (Input.GetButtonDown("Fire1")){
                SceneManager.LoadScene(1);
                Destroy(FindObjectOfType<GameManager>().gameObject);
            }
        }
    }

    void MoveSelection(string direction)
    {
        if (!isMoving)
        {
            isMoving = true;
            PlaySound();
            if (direction == "right")
            {
                characterIndex += 1;
                if (characterIndex > 4)
                {
                    characterIndex = 1;
                }
            }
            else if (direction == "left")
            {
                characterIndex -= 1;
                if (characterIndex < 1)
                {
                    characterIndex = 4;
                }
            }

            Invoke("ResetMove", 0.3f);
        }
    }

    void ResetMove()
    {
        isMoving = false;
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
