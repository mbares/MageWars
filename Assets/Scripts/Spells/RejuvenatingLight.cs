using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;

public class RejuvenatingLight : MonoBehaviour, ISpell {

    public int healValue;
    public int healScoreModifier;

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
    private Character enemyTarget;
    private Character character;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        enemyTarget = FindObjectOfType<Enemy>().GetComponent<Character>();
        character = FindObjectOfType<Player>().GetComponent<Character>();
    }

    public void AddSpellToCast() {
        gameManager.AddSpellToCast(this);
    }

    public void DoSpellEffect(RecognitionResult result) {
        int heal = healValue;
        if (result.score.score > 0.95f) {
            heal *= 2;
        } else if (result.score.score > 0.85f) {
            heal += healScoreModifier;
        }
        if (character.IsConfused() && Random.Range(0, 5) == 0) {
            enemyTarget.IncreaseHealth(heal);
            Debug.Log("In it's confustion " + character.name + "heals the enemy.");
        } else {
            character.IncreaseHealth(heal);
        }
    }
}
