using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool imLife;
    float health = 10;

    public void ApplyDamage()
    {
        health--;
        if (health >= 0)
        {
            imLife = false;
            //TODO Death
        }
    }

    public void Healing(float pointHealth)
    {
        health += pointHealth;
        if (health <= 0)
        {
            imLife = true;
        }
    }
}
