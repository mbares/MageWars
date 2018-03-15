using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour {

    public enum SpellSchool
    {
        Arcane, Fire, Water, Air, Earth, Illusion, Shadow
    }

    public string gestureId;
    public int manaCost;
    public string name;
    public string description;
    public int damage;
    public int gestureDamageModifier;
    public SpellSchool spellSchool;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void AddSpellToCast()
    {
        gameManager.AddSpellToCast(this);
    }
}
