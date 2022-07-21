using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChapterIntro : MonoBehaviour
{
    private string levelToLoad;
    [SerializeField]
    private Text chapterTitle;
    [SerializeField]
    private Text chapterName;

    public GameObject startMessage;
    private bool canStart = false;
    public float startDelay = 5.0f;
    public static bool level3 = false;

    // Start is called before the first frame update
    void Start()
    {
        canStart = false;
        Player.notMaxHealth = false;
        Stages(); //Functions full of stages
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(activeStart());
        if (Input.GetButtonDown("Submit") && canStart)
        {
            Invoke("LoadScene", 1f);
            startMessage.SetActive(false);
        }
    }

    void Stages()
    {
        if (Boss.winLevel)
        {
            levelToLoad = "Stage 2-1";
            chapterTitle.text = "Chapter 2";
            chapterName.text = "三魂ギャング\n" + "3 Souls Gang";
        }
        else if (Stage2Boss.winLevel)
        {
            levelToLoad = "Stage 3-1";
            chapterTitle.text = "Chapter 3";
            chapterName.text = "アレス軍\n" + "Ares Army";
        }
        else if (Stage3Boss.winLevel)
        {
            levelToLoad = "Stage 4-1";
            chapterTitle.text = "Chapter 4";
            chapterName.text = "ヴァンプのブランド\n" + "Blood of Vamp";
        }
        else if (Stage4Boss.winLevel)
        {
            levelToLoad = "Stage 5-1";
            chapterTitle.text = "Final Chapter";
            chapterName.text = "キングヴァンプ、ヴラキュラ\n" + "King Vamp, Vladcula";
        }
        else
        {
            levelToLoad = "Stage 1";
            chapterTitle.text = "Chapter 1";
            chapterName.text = "ザクギャング\n" + "Zaku Gang";
        }
    }

    public IEnumerator activeStart()
    {
        yield return new WaitForSeconds(startDelay);
        startMessage.SetActive(true);
        canStart = true;
    }

    void LoadScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
