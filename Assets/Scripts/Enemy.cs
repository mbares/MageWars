using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Character target;
    public string name;

    private GameManager gameManager;
    private bool isFinishedAttacking = false;

    private void Start()
    {
        target = FindObjectOfType<Player>().GetComponent<Character>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Attack(int minDmg, int maxDmg, int extraDamage = 0)
    {
        int damage = Random.Range(minDmg, maxDmg) + extraDamage;
        target.SetHealth(target.GetHealth() - damage);
    }

    public bool IsFinishedAttacking()
    {
        return isFinishedAttacking;
    }

    public void SetIsFinishedAttacking(bool isFinishedAttacking)
    {
        this.isFinishedAttacking = isFinishedAttacking;
    }

    public void EndTurn()
    {
        Invoke("EndEnemyTurn", 1.5f);
    }

    private void EndEnemyTurn()
    {
        gameManager.StartPreparePhase();
    }
}
