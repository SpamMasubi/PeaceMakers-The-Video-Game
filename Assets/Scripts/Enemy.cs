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

    [Header("Invincibility Flash")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public static bool isInvincible;
    private SpriteRenderer enemySprite;

    private bool comboDamaged = false;
    protected int comboCount = 0;
    private int currentHealth;
    protected float currentSpeed;
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

            if (Player.isDead)
            {
                currentSpeed = 0;
                anim.SetFloat("Speed", Mathf.Abs(currentSpeed));
            }
            else if (Player.hasRespawned)
            {
                currentSpeed = maxSpeed;
                anim.SetFloat("Speed", Mathf.Abs(currentSpeed));
                Player.hasRespawned = false;
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
        rb.position = new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));
    }

    public void StartInvincibility()
    {
        StartCoroutine(InvincibilityFlash());
    }
    private IEnumerator InvincibilityFlash()
    {
        int temp = 0;
        isInvincible = true;
        while (temp < numberOfFlashes)
        {
            enemySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            enemySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        isInvincible = false;
    }

    public void TookDamage(int damage)
    {
        if (!isDead && !comboDamaged && !isInvincible)
        {
            damaged = true;
            currentHealth -= damage;
            comboCount += 1;
            anim.SetTrigger("HitDamage");
            PlaySong(collisionSound);
            FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);
            if (comboCount == 4 && currentHealth > 0 || CrowdBreaker.crowdBreaker)
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
            projectile.GetComponent<Projectiles>().speed = Mathf.Abs(projectile.GetComponent<Projectiles>().speed);
        }
        else
        {
            projectile.GetComponent<Projectiles>().speed = -(projectile.GetComponent<Projectiles>().speed);
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
        if (FinalMidBoss.midBoss)
        {
            FindObjectOfType<UIManager>().enemyUI.SetActive(false);
            FinalMidBoss.midBoss = false;
            Destroy(FindObjectOfType<MusicController>().gameObject);
        }
        Destroy(gameObject);
    }

    public void ResetSpeed()
    {
        currentSpeed = maxSpeed;
        CrowdBreaker.crowdBreaker = false;
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }
}
