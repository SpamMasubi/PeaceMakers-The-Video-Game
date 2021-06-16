﻿using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.startFirstLevel)
        {
            levelToLoad = "Stage 1";
            chapterTitle.text = "Chapter 1";
            chapterName.text = "ザクギャング\n" + "Zaku Gang";
            StartCoroutine(activeStart());
            if (Input.GetKeyDown("space") && canStart)
            {
                Invoke("LoadScene", 1f);
                startMessage.SetActive(false);
                Menu.startFirstLevel = false;
            }
        }
        else if (Boss.startLevel2)
        {
            levelToLoad = "Stage 2-1";
            chapterTitle.text = "Chapter 2";
            chapterName.text = "三魂ギャング\n" + "3 Souls Gang";
            StartCoroutine(activeStart());
            if (Input.GetKeyDown("space") && canStart)
            {
                Invoke("LoadScene", 1f);
                startMessage.SetActive(false);
                Boss.startLevel2 = false;
            }
        }
        else if (Stage2Boss.startLevel3)
        {
            levelToLoad = "Stage 3-1";
            chapterTitle.text = "Chapter 3";
            chapterName.text = "アレス軍\n" + "Ares Army";
            StartCoroutine(activeStart());
            if (Input.GetKeyDown("space") && canStart)
            {
                Invoke("LoadScene", 1f);
                startMessage.SetActive(false);
                Stage2Boss.startLevel3 = false;
            }

        }
        else if (Stage3Boss.startLevel4)
        {
            levelToLoad = "Stage 4-1";
            chapterTitle.text = "Chapter 4";
            chapterName.text = "ヴァンプのブランド\n" + "Blood of Vamp";
            StartCoroutine(activeStart());
            if (Input.GetKeyDown("space") && canStart)
            {
                Invoke("LoadScene", 1f);
                startMessage.SetActive(false);
                Stage3Boss.startLevel4 = false;
            }

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
