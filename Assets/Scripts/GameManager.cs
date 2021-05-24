using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives;
    public static int maxHealth = 100;
    public int characterIndex;
    private GameManager gm;

    // Start is called before the first frame update
    void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
