using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeapon : MonoBehaviour
{
    private SpriteRenderer spriteRend;


    private Color color;
    public static int durability;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (durability <= 0)
        {
            spriteRend.sprite = null;
            GetComponentInParent<Player>().SetHoldingSpecialWeaponToFalse();
        }
    }

    public void ActivateWeapon(Sprite sprite, Color color, int durabilityValue)
    {
        spriteRend.sprite = sprite;
        spriteRend.color = color;
        durability = durabilityValue;
    }
}
