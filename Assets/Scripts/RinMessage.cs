using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinMessage : MonoBehaviour
{
    public AudioClip message;
    public bool drop;
    public GameObject itemsDrop;
    public Transform dropPoint;
    public GameObject dialogueBox;
    public static bool startDialogue = false;
   
    private AudioSource audioS;
    private Transform player;
    

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDialogue)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(dialolgueDisable());
        }
        else
        {
            dialogueBox.SetActive(false);
        }
    }

    public IEnumerator dialolgueDisable()
    {
        GetComponent<BoxCollider>().enabled = false;
        PlaySong(message);
        yield return new WaitForSeconds(3f);
        startDialogue = false;
        if (drop)
        {
            Instantiate(itemsDrop, dropPoint.position, dropPoint.rotation);
            drop = false;
        }
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

}
