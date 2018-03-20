    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : MonoBehaviour {

    public int minFireBreathDamage = 2;
    public int maxFireBreathDamage = 4;
    public int fireShieldDamage = 1;

    private Character playerCharacter;
    private Enemy enemy;
    private Character enemyCharacter;
    private GameManager gameManager;

    private void Start()
    {
        playerCharacter = FindObjectOfType<Player>().GetComponent<Character>();
        enemy = GetComponent<Enemy>();
        enemyCharacter = GetComponent<Character>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update()
    {
        if (gameManager.IsEnemyPhase() && !enemy.IsFinishedAttacking())
        {
            enemy.SetIsFinishedAttacking(true);
            enemy.EndTurn();
        }
    }

    private void FireBreath()
    {
        enemyCharacter.DecreaseMana(3);
        enemy.Attack(minFireBreathDamage, maxFireBreathDamage);
        playerCharacter.SetIsBurning(Random.Range(1, 4), 2);
    }

    private void FireShield()
    {
        enemyCharacter.DecreaseMana(2);
        enemyCharacter.SetHasElementalShield(true, SpellType.Fire, 3, fireShieldDamage);
    }

    private void SmokeScreen() {
        enemyCharacter.DecreaseMana(2);
        playerCharacter.SetIsBlinded(2);
    }
}
