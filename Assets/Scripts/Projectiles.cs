using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed = 3;
    public int direction = 1;
    public int damage;
    private Rigidbody rb;

    void Start()
    {
        rb= GetComponent<Rigidbody>();
        if (direction == -1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(speed * direction, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            SpecialWeapon.durability--;
            GetComponent<Attack>().damage = damage;
        }
        Destroy(gameObject);
    }

}
