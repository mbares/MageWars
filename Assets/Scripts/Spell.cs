using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour {

    public string gestureId;
    public int manaCost;
    public int damage;
    public int damageLevelModifier;
    public string name;
    public string description;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Cast()
    {
        gameManager.AddSpellToCast(this);
    }

}
