using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public AudioClip objectHit, objectBreak;
    public int breakCountDown = 4;
    public bool drops;
    public GameObject[] itemsDrop;
    public Transform dropPoint;

    private Transform groundCheck;
    private bool onGround;
    private AudioSource audioS;
    private Animator anim;
    private Rigidbody rb;
    private bool isBreak;
    private bool isRight;
    private bool playerRight;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.Find("GroundCheck");
        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        anim.SetBool("Grounded", onGround);

        playerRight = (player.position.x < transform.position.x) ? false : true;
        if (playerRight)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
    }

    public void ObjectBreak(int damage)
    {
        breakCountDown -= damage;
        
        if(breakCountDown <= 0 && !isBreak)
        {
            isBreak = true;
            anim.SetBool("Break", true);
            GetComponent<BoxCollider>().enabled = false;
            if (!isRight)
            {
                rb.AddRelativeForce(new Vector3(300, 500, 0), ForceMode.Impulse);
            }
            else
            {
                rb.AddRelativeForce(new Vector3(-300, 500, 0), ForceMode.Impulse);
            }
            PlaySong(objectBreak);
            if (drops)
            {
                int itemRandom = Random.Range(0, itemsDrop.Length);
                Instantiate(itemsDrop[itemRandom], dropPoint.position, dropPoint.rotation);
            }
        }
        else
        {
            if (!isBreak)
            {
                PlaySong(objectHit);
            }
        }
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

    public void ResetBreak()
    {
        isBreak = false;
        gameObject.SetActive(false);
        anim.SetBool("Break", false);
    }
}
