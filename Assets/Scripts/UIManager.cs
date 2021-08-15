using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private UIManager um;
    public Slider healthUI;
    public Image playerImage;
    public Text playerName;
    public Text livesText;
    public Text displayMessage;

    public GameObject enemyUI;
    public Slider enemySlider;
    public Text enemyName;
    public Image enemyImage;

    public float enemyUITime = 4f;

    private float enemyTimer;

    private Player player;

    void Awake()
    {
        if (um == null)
        {
            um = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerName.text = player.playerName;
        playerImage.sprite = player.playerImage;
        healthUI.maxValue = GameManager.maxHealth;
        healthUI.value = healthUI.maxValue;
        UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Boss.firstBoss && !Stage2Boss.secondBoss && !Stage3Boss.thirdBoss && !Stage4Boss.fourthBoss && !FinalMidBoss.midBoss && !FinalBoss.finalBoss)
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer >= enemyUITime)
            {
                enemyUI.SetActive(false);
                enemyTimer = 0;
            }
        }
    }

    public void healthUpdate(int amount)
    {
        healthUI.value = amount;
    }

    public void UpdateEnemyUI(int maxHealth, int currentHealth, string name, Sprite image)
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = currentHealth;
        enemyName.text = name;
        enemyImage.sprite = image;
        enemyTimer = 0;
        enemyUI.SetActive(true);
    }

    public void UpdateLives()
    {
        livesText.text = "x " + FindObjectOfType<GameManager>().lives.ToString();
    }

    public void UpdateDisplayMessage(string message)
    {
        displayMessage.text = message;
    }
}
