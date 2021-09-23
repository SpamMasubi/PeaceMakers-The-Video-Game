using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxSpeed = 4;
    public float jumpForce = 400;
    public float minHeight, maxHeight;
    public string playerName;
    public Sprite playerImage;
    public AudioClip collisionSound, playerShoot, playerComboed, playerDead, playerRespawnSound, jumpSound, crowdBreaker, attackYell, healthItem, get1Up;
    public Weapon weapon;
    public SpecialWeapon specialWeapon;
    public GameObject playerProjectile;
    public Transform launchPoint;

    public float flashLength;
    private bool flashActive;
    private float flashCounter;
    private SpriteRenderer playerSprite;
    private bool isCrowdBreaker = false;
    private bool isAttack = false;
    private bool takenDamaged = false;
    private bool playerInAir = false;

    private bool comboDamaged = false;
    public static int comboCount = 0;
    public static int currentHealth;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private Transform groundCheck;
    private bool onGround;
    private bool running;
    public static bool isDead = false;
    public static bool hasRespawned = false;
    private bool facingRight = true;
    private bool jump = false;
    private AudioSource audioS;
    private bool holdingWeapon = false;
    private bool holdingSpecialWeapon = false;
    public static bool notMaxHealth = false;
    private MusicController music;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        if (!notMaxHealth)
        {
            currentHealth = GameManager.maxHealth;
        }
        else
        {
            currentHealth = EnemySpawn.playerCurrentHealth;
        }
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

        if (!isDead && !comboDamaged && !takenDamaged && !Boss.bossDefeated && !Stage2Boss.bossDefeated && !Stage3Boss.bossDefeated && !Stage4Boss.bossDefeated && !FinalBoss.bossDefeated)
        {
            if (Input.GetButtonDown("Jump") && onGround && !isCrowdBreaker && !isAttack)
            {
                jump = true;
                playerInAir = true;
            }

            if (Input.GetButtonDown("Fire1") && Input.GetButton("Fire2") && !isCrowdBreaker && !playerInAir && !holdingSpecialWeapon && !holdingWeapon)
            {
                anim.SetTrigger("CrowdBreaker");
                PlaySong(crowdBreaker);
                isCrowdBreaker = true;
            }
            else if (Input.GetButtonDown("Fire1") && !isCrowdBreaker)
            {
                anim.SetTrigger("Attack");
                isAttack = true;
                if (!holdingSpecialWeapon)
                {
                    PlaySong(attackYell);
                }
            }
            
        }

        if (flashActive)
        {
            if (flashCounter > flashLength * 4.95f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 4.62f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 4.29f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 3.96f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 3.63f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 3.30f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 2.97f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 2.64f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 2.31f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.98f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 1.65f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.32f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.99f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 0.66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > 0.0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }

        if (Boss.bossDefeated || Stage2Boss.bossDefeated || Stage3Boss.bossDefeated || Stage4Boss.bossDefeated || FinalBoss.bossDefeated)
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
        if (!isDead && !comboDamaged && !Boss.bossDefeated && !Stage2Boss.bossDefeated && !Stage3Boss.bossDefeated && !Stage4Boss.bossDefeated && !FinalBoss.bossDefeated)
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
                rb.velocity = new Vector3(h * (currentSpeed * 2.0f), rb.velocity.y, y * (currentSpeed * 2.0f));
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

            if (h>0 && !facingRight)
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
        CrowdBreaker.crowdBreaker = false;
        isCrowdBreaker = false;
        isAttack = false;
        takenDamaged = false;
        playerInAir = false;
    }

    public void TookDamage(int damage)
    {
        if (!isDead && !comboDamaged && !flashActive)
        {
            takenDamaged = true;
            currentHealth -= damage;
            notMaxHealth = true;
            EnemySpawn.playerCurrentHealth = currentHealth;
            comboCount += 1;
            anim.SetTrigger("HitDamage");
            FindObjectOfType<UIManager>().healthUpdate(currentHealth);
            PlaySong(collisionSound);
            if (comboCount == 4 && currentHealth > 0 || !onGround && currentHealth > 0)
            {
                PlaySong(playerComboed);
                comboDamaged = true;
                if (facingRight)
                {
                    rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                }
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
                currentHealth = GameManager.maxHealth;
                notMaxHealth = false;
                FindObjectOfType<UIManager>().healthUpdate(currentHealth);
            }
        }

        if (other.CompareTag("1-Up Item"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Destroy(other.gameObject);
                anim.SetTrigger("Catching");
                PlaySong(get1Up);
                FindObjectOfType<GameManager>().lives++;
                FindObjectOfType<UIManager>().UpdateLives();
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

        if (other.CompareTag("NPC"))
        {
            RinMessage.startDialogue = true;
        }
    }

    void PlayerRespawn()
    {
        if (FindObjectOfType<GameManager>().lives > 0)
        {
            PlaySong(playerRespawnSound);
            flashActive = true;
            flashCounter = flashLength + 1.6f;
            isDead = false;
            hasRespawned = true;
            notMaxHealth = false;
            FindObjectOfType<UIManager>().UpdateLives();
            currentHealth = GameManager.maxHealth;
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
        GameObject projectile = Instantiate(playerProjectile, launchPoint.position, launchPoint.rotation);
        playerProjectile.SetActive(true);
        if (facingRight)
        {
            projectile.GetComponent<Projectiles>().direction = 1;
        }
        else
        {
            projectile.GetComponent<Projectiles>().direction = -1;
        }
    }

    void LoadScene()
    {
        Destroy(FindObjectOfType<UIManager>().gameObject);
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
