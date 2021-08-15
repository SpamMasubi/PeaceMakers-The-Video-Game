using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdBreaker : MonoBehaviour
{
    public int damage;
    public static bool crowdBreaker = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        BreakableObject breakObj = other.GetComponent<BreakableObject>();

        if (enemy != null)
        {
            crowdBreaker = true;
            enemy.TookDamage(damage);
        }

        if (breakObj != null)
        {
            breakObj.ObjectBreak(damage);
        }
    }
}
