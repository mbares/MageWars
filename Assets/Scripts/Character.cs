using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public int health;
    public int mana;
    public int maxHealth;
    public int maxMana;

    public void ReduceHealth(int damage)
    {
        health -= damage;
    }

    public void IncreaseHealth(int healValue)
    {
        health += healValue;
    }

    public void RefillFullMana()
    {
        mana = maxMana;
    }
}
