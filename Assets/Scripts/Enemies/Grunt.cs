using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour { 

    public int minDmg = 1;
    public int maxDmg = 1;

    private Enemy enemy;
    private GameManager gameManager;

	private void Start () {
        enemy = GetComponent<Enemy>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update () {
        if (gameManager.IsEnemyPhase() && !enemy.IsFinishedAttacking()) {
            enemy.SetIsFinishedAttacking(true);
            Debug.Log("ATTACKING");
            StartCoroutine(Attack());
            enemy.EndTurn();
        }
	}

    private IEnumerator Attack() {
        yield return new WaitForSeconds(1);
        enemy.Attack(minDmg, maxDmg);
    }
}
