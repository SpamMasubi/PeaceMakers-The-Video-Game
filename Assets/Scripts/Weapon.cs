using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRend;


    private Color color;
    private int durability;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void ActivateWeapon(Sprite sprite, Color color, int durabilityValue, int damage)
    {
        spriteRend.sprite = sprite;
        spriteRend.color = color;
        durability = durabilityValue;
        GetComponent<Attack>().damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            durability--;
            if(durability <= 0)
            {
                spriteRend.sprite = null;
                GetComponentInParent<Player>().SetHoldingWeaponToFalse();
            }
        }
    }
}
