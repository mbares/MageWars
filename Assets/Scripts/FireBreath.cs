using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;

public class FireBreath : MonoBehaviour, ISpell {

    public int damage;
    public int burningDamage;

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
    private string description;
    public string Description { get { return description; } set { description = value; } }

    private GameManager gameManager;
    private Character enemyTarget;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        enemyTarget = FindObjectOfType<Enemy>().GetComponent<Character>();
    }

    public void AddSpellToCast() {
        gameManager.AddSpellToCast(this);
    }

    public void DoSpellEffect(RecognitionResult result) {
        if (enemyTarget.IsWet()) {
            enemyTarget.SetIsWet(0);
        }
        if (result.score.score > 0.95f) {
            enemyTarget.SetIsBurning(3, burningDamage);
        } else if (result.score.score > 0.85f) {
            enemyTarget.SetIsBurning(2, burningDamage);
        } else {
            enemyTarget.SetIsBurning(1, burningDamage);
        }
        enemyTarget.DecreaseHealthBySpellDamage(damage, spellType);
        Debug.Log("Fire breath hits and burns " + enemyTarget.name);
    }
}
