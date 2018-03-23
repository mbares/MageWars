using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;

public class Empower : MonoBehaviour, ISpell {

    public int empowerValue;
    public int scoreModifier;

    [SerializeField]
    private string gestureId;
    public string GestureId { get { return gestureId; } set { gestureId = value; } }
    [SerializeField]
    private int manaCost;
    public int ManaCost { get { return manaCost; } set { manaCost = value; } }
    [SerializeField]
    private SpellType spellType;
    public SpellType SpellType { get { return spellType; } }
    [SerializeField]
    private string name;
    public string Name { get { return name; } set { name = value; } }
    [SerializeField]
    [TextArea]
    private string description;
    public string Description { get { return description; } set { description = value; } }

    private GameManager gameManager;
    private Character playerCharacter;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        playerCharacter = FindObjectOfType<Player>().GetComponent<Character>();
    }

    public void AddSpellToCast() {
        gameManager.AddSpellToCast(this);
    }

    public void DoSpellEffect(RecognitionResult result) {
        int extraDamage = empowerValue;
        if (result.score.score > 0.95f) {
            extraDamage *= 2;
        } else if (result.score.score > 0.85f) {
            extraDamage += scoreModifier;
        }
        playerCharacter.GetComponent<Character>().SetExtraDamage(extraDamage);
        Debug.Log(playerCharacter.name + " empowers his next damaging spell.");
    }
}
