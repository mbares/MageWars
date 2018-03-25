using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Character target;
    private Character character;
    private GameManager gameManager;
    private bool isFinishedAttacking = false;

    private void Start() {
        target = FindObjectOfType<Player>().GetComponent<Character>();
        gameManager = FindObjectOfType<GameManager>();
        character = GetComponent<Character>();
    }

    public void Attack(int minDmg, int maxDmg, SpellType spellType = SpellType.None, bool lifesteal = false) {
        int damage = Random.Range(minDmg, maxDmg + 1) + character.GetEmpoweredDamage();
        if (character.IsBlinded() && Random.Range(0, 4) == 0) {
            Debug.Log(character.name + " misses the attack because of blind effect.");
            return;
        }
        if (character.IsConfused() && Random.Range(0, 5) == 0) {
            character.DecreaseHealthBySpellDamage(damage, spellType);
            Debug.Log("In it's confusion " + character.name + "'s attack backfires.");
        } else {
            target.DecreaseHealthBySpellDamage(damage, spellType);
            if (lifesteal) {
                character.IncreaseHealth(damage / 2);
            }
        }
        character.SetEmpowered(0);
    }

    public bool IsFinishedAttacking() {
        return isFinishedAttacking;
    }

    public void SetIsFinishedAttacking(bool isFinishedAttacking) {
        this.isFinishedAttacking = isFinishedAttacking;
    }

    public void EndTurn() {
        character.IncreaseMana(character.maxMana);
        Invoke("EndEnemyTurn", 1.5f);
    }

    private void EndEnemyTurn() {
        gameManager.StartPreparePhase();
    }
}
