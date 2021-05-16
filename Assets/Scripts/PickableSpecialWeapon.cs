using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableSpecialWeapon : MonoBehaviour
{
    public ShootableWeapons[] shootableWeapon;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<GameManager>().characterIndex == 1)
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = shootableWeapon[0].sprite;
            sprite.color = shootableWeapon[0].color;
        }
        else if (FindObjectOfType<GameManager>().characterIndex == 2)
        {
            sprite = GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(1f, 1f, 1f);
            sprite.sprite = shootableWeapon[1].sprite;
            sprite.color = shootableWeapon[1].color;
        }
        else if (FindObjectOfType<GameManager>().characterIndex == 3)
        {
            sprite = GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(1f, 1f, 1f);
            sprite.sprite = shootableWeapon[2].sprite;
            sprite.color = shootableWeapon[2].color;
        }
        else if (FindObjectOfType<GameManager>().characterIndex == 4)
        {
            sprite = GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            sprite.sprite = shootableWeapon[3].sprite;
            sprite.color = shootableWeapon[3].color;
        }
    }
}
