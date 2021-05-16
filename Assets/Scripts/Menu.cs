using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool startFirstLevel = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            startFirstLevel = true;
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
