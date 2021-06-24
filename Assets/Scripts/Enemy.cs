using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxSpeed;
    public float minHeight, maxHeight;
    public float damageTime = 0.5f;
    public int maxHealth;
    public float attackRate = 1f;
    public string enemyName;
    public Sprite enemyImage;
    public AudioClip collisionSound, enemyComboed, enemyAttack, deathSound;
    [Header("For Enemy with Weapons with projectiles")]
    public GameObject enemyProjectile;
    public Transform launchPoint;

    public float flashLength;
    private bool flashActive;
    private float flashCounter;
    private SpriteRenderer enemySprite;

    private bool comboDamaged = false;
    protected int comboCount = 0;
    private int currentHealth;
    private float currentSpeed;
    private Rigidbody rb;
    protected Animator anim;
    private Transform groundCheck;
    private bool onGround;
    protected bool facingRight = false;
    private Transform target;
    protected bool isDead = false;
    private float zForce;
    private float walkTimer;
    private bool damaged = false;
    private float damageTimer;
    private float nextAttack;
    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        target = FindObjectOfType<Player>().transform;
        currentHealth = maxHealth;
        audioS = GetComponent<AudioSource>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        anim.SetBool("Grounded", onGround);
        anim.SetBool("Dead", isDead);
        anim.SetBool("SentFlying", comboDamaged);

        if (!isDead && !comboDamaged)
        {
            facingRight = (target.position.x < transform.position.x) ? false : true;
            if (facingRight)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (damaged && !isDead)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageTime)
            {
                damaged = false;
                damageTimer = 0;
            }
        }

        walkTimer += Time.deltaTime;

        if (flashActive)
        {
            if (flashCounter > flashLength * 3.30f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 2.97)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 2.64f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 2.31)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.98f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 1.65f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.32f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 0.66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > 0.0f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    {
        if (!isDead && !comboDamaged)
        {
            Vector3 targetDistance = target.position - transform.position;
            float hForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            if (walkTimer >= Random.Range(1f, 2f))
            {
                zForce = Random.Range(-1, 2);
                walkTimer = 0;
            }

            if (Mathf.Abs(targetDistance.x) < 1.5f)
            {
                hForce = 0;
            }

            if (!damaged && comboCount == 0)
            {
                rb.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);

                anim.SetFloat("Speed", Mathf.Abs(currentSpeed));

                if (Mathf.Abs(targetDistance.x) < 2f && Mathf.Abs(targetDistance.z) < 2f && Time.time > nextAttack && !Player.isDead)
                {
                    anim.SetTrigger("Attack");
                    PlaySong(enemyAttack);
                    currentSpeed = 0;
                    nextAttack = Time.time + attackRate;
                }
            }
        }

        if (Player.isDead)
        {
            currentSpeed = 0;
            anim.SetFloat("Speed", Mathf.Abs(currentSpeed));
        }

        rb.position = new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));

    }

    public void TookDamage(int damage)
    {
        if (!isDead && !comboDamaged && !flashActive)
        {
            damaged = true;
            currentHealth -= damage;
            comboCount += 1;
            anim.SetTrigger("HitDamage");
            PlaySong(collisionSound);
            FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);
            if (comboCount == 4 && currentHealth > 0)
            {
                PlaySong(enemyComboed);
                comboDamaged = true;
                rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
            }
            if (currentHealth <= 0)
            {
                isDead = true;
                rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                PlaySong(deathSound);
            }
        }
    }

    void EnemyShoot()
    {
        GameObject projectile = Instantiate(enemyProjectile, launchPoint.position, launchPoint.rotation);
        enemyProjectile.SetActive(true);
        if (facingRight)
        {
            projectile.GetComponent<Projectiles>().direction = 1;
        }
        else
        {
            projectile.GetComponent<Projectiles>().direction = -1;
        }
    }

    public void resetEnemyFlying()
    {
        comboCount = 0;
        comboDamaged = false;
        damaged = false;
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    public void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

    public void invincibleFlash()
    {
        flashActive = true;
        //flashCounter = flashLength + 1.5f;
        flashCounter = flashLength + 1f;
    }
}
