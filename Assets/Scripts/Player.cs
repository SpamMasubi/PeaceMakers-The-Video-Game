using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxSpeed = 4;
    public float jumpForce = 400;
    public float minHeight, maxHeight;
    public int maxHealth = 10;
    public string playerName;
    public Sprite playerImage;
    public AudioClip collisionSound, playerShoot, playerComboed, playerDead, playerRespawnSound, jumpSound, attackYell, healthItem;
    public Weapon weapon;
    public SpecialWeapon specialWeapon;
    public GameObject playerProjectile;
    public Transform launchPoint;

    private bool comboDamaged = false;
    private int comboCount = 0;
    private int currentHealth;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private Transform groundCheck;
    private bool onGround;
    private bool running;
    public static bool isDead = false;
    private bool facingRight = true;
    private bool jump = false;
    private AudioSource audioS;
    private bool holdingWeapon = false;
    private bool holdingSpecialWeapon = false;
    private MusicController music;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        currentHealth = maxHealth;
        audioS = GetComponent<AudioSource>();
        music = FindObjectOfType<MusicController>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        anim.SetBool("OnGround", onGround);
        anim.SetBool("IsDead", isDead);
        anim.SetBool("SentFlying", comboDamaged);
        anim.SetBool("Weapon", holdingWeapon);
        anim.SetBool("SpecialWeapon", holdingSpecialWeapon);

        if (!isDead && !Boss.winLevel && !comboDamaged)
        {
            if (Input.GetButtonDown("Jump") && onGround)
            {
                jump = true;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                
                anim.SetTrigger("Attack");
                if (!holdingSpecialWeapon)
                {
                    PlaySong(attackYell);
                }
            }
        }

        if (Boss.winLevel || Stage2Boss.winLevel)
        {
            anim.SetBool("WinLevel", true);
        }
        else
        {
            anim.SetBool("WinLevel", false);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && !Boss.winLevel && !comboDamaged)
        {
            float h = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (!onGround)
            {
                y = 0;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                running = true;
            }

            if (running) {
                rb.velocity = new Vector3(h * (currentSpeed * 1.5f), rb.velocity.y, y * (currentSpeed * 1.5f));
                anim.SetBool("Run", running);
                running = false;
            }
            else
            {
                rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, y * currentSpeed);
                anim.SetBool("Run", running);
            }

            if (onGround)
            {
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
            }

            if(h>0 && !facingRight)
            {
                Flip();
            }else if( h<0 && facingRight)
            {
                Flip();
            }

            if (jump && !comboDamaged)
            {
                jump = false;
                rb.AddForce(Vector3.up * jumpForce);
                PlaySong(jumpSound);
            }

            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0,0,10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1), rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }

    public void TookDamage(int damage)
    {
        if (!isDead && !comboDamaged)
        {
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");
            FindObjectOfType<UIManager>().healthUpdate(currentHealth);
            PlaySong(collisionSound);
            if (comboCount == 4 && currentHealth > 0 || !onGround && currentHealth > 0)
            {
                PlaySong(playerComboed);
                comboDamaged = true;
                rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
            }
            if (currentHealth <= 0)
            {
                PlaySong(playerDead);
                isDead = true;
                FindObjectOfType<GameManager>().lives--;
                
                if (facingRight)
                {
                    rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                }
            }
        }
    }

    public void resetPlayerFlying()
    {
        comboCount = 0;
        comboDamaged = false;
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Health Item"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Destroy(other.gameObject);
                anim.SetTrigger("Catching");
                PlaySong(healthItem);
                currentHealth = maxHealth;
                FindObjectOfType<UIManager>().healthUpdate(currentHealth);
            }
        }

        if (other.CompareTag("Weapon"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetTrigger("Catching");
                holdingWeapon = true;
                WeaponItem weaponItem = other.GetComponent<PickableWeapon>().weapon;
                weapon.ActivateWeapon(weaponItem.sprite, weaponItem.color, weaponItem.durability, weaponItem.damage);
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Special Weapon"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetTrigger("Catching");
                holdingSpecialWeapon = true;
                if (FindObjectOfType<GameManager>().characterIndex == 1)
                {
                    ShootableWeapons shootableWeapon = other.GetComponent<PickableSpecialWeapon>().shootableWeapon[0];
                    specialWeapon.ActivateWeapon(shootableWeapon.sprite, shootableWeapon.color, shootableWeapon.durability);
                }
                else if (FindObjectOfType<GameManager>().characterIndex == 2)
                {
                    ShootableWeapons shootableWeapon = other.GetComponent<PickableSpecialWeapon>().shootableWeapon[1];
                    specialWeapon.ActivateWeapon(shootableWeapon.sprite, shootableWeapon.color, shootableWeapon.durability);
                }
                else if (FindObjectOfType<GameManager>().characterIndex == 3)
                {
                    ShootableWeapons shootableWeapon = other.GetComponent<PickableSpecialWeapon>().shootableWeapon[2];
                    specialWeapon.ActivateWeapon(shootableWeapon.sprite, shootableWeapon.color, shootableWeapon.durability);
                }
                else if (FindObjectOfType<GameManager>().characterIndex == 4)
                {
                    ShootableWeapons shootableWeapon = other.GetComponent<PickableSpecialWeapon>().shootableWeapon[3];
                    specialWeapon.ActivateWeapon(shootableWeapon.sprite, shootableWeapon.color, shootableWeapon.durability);
                }
                Destroy(other.gameObject);
            }
        }
    }

    void PlayerRespawn()
    {
        if (FindObjectOfType<GameManager>().lives > 0)
        {
            PlaySong(playerRespawnSound);
            isDead = false;
            FindObjectOfType<UIManager>().UpdateLives();
            currentHealth = maxHealth;
            FindObjectOfType<UIManager>().healthUpdate(currentHealth);
            anim.Rebind();
            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            transform.position = new Vector3(minWidth, 10, -4);
        }
        else
        {
            music.PlaySong(music.gameOverSong);
            FindObjectOfType<UIManager>().UpdateDisplayMessage("Game Over");
            Destroy(FindObjectOfType<GameManager>().gameObject);
            Invoke("LoadScene", 7.5f);
        }

    }

    void PlayerShoot()
    {
        PlaySong(playerShoot);
        GameObject tempAxe = Instantiate(playerProjectile, launchPoint.position, launchPoint.rotation);
        playerProjectile.SetActive(true);
        if (facingRight)
        {
            tempAxe.GetComponent<Projectiles>().direction = 1;
        }
        else
        {
            tempAxe.GetComponent<Projectiles>().direction = -1;
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(2);
        isDead = false;
    }

    public void SetHoldingWeaponToFalse()
    {
        holdingWeapon = false;
    }

    public void SetHoldingSpecialWeaponToFalse()
    {
        holdingSpecialWeapon = false;
    }
}
