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

    public void Attack(int minDmg, int maxDmg, int extraDamage = 0) {
        int damage = Random.Range(minDmg, maxDmg + 1) + extraDamage;
        if (character.IsBlinded() && Random.Range(0, 4) == 0) {
            damage = 0;
            Debug.Log(character.name + " misses the attack because of blind effect.");
        }
        target.DecreaseHealth(damage);
    }

    public bool IsFinishedAttacking() {
        return isFinishedAttacking;
    }

    public void SetIsFinishedAttacking(bool isFinishedAttacking) {
        this.isFinishedAttacking = isFinishedAttacking;
    }

    public void EndTurn() {
        Invoke("EndEnemyTurn", 1.5f);
    }

    private void EndEnemyTurn() {
        gameManager.StartPreparePhase();
    }
}
