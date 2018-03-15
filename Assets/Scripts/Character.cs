using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public int maxHealth;
    public int maxMana;

    private int health;
    private int mana;
    private bool isBurning = false;
    private bool isStunned = false;
    private bool isConfused = false;

    private void Start()
    {
        health = maxHealth;
        mana = maxMana;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetMana(int mana)
    {
        this.mana = mana;
    }

    public int GetMana()
    {
        return mana;
    }
}
