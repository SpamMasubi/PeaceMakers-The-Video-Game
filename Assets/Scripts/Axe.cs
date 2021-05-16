using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public int direction = 1;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveAxe());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(6 * direction, 0, 2 * direction);
    }

    IEnumerator MoveAxe()
    {
        yield return new WaitForSeconds(2f);
        direction *= 1;
    }
}
