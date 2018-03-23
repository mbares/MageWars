using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cultist : MonoBehaviour {

    public int lifeStealManaCost = 3;
    public int minLifeStealDamage = 2;
    public int maxLifeStealDamage = 4;
    public int mindBlowManaCost = 2;
    public int maxMindBlowDamage = 1;
    public int minMindBlowDamage = 3;
    public int empowerManaCost = 2;
    public int empowerValue = 2;

    private Character playerCharacter;
    private Enemy enemy;
    private Character enemyCharacter;
    private GameManager gameManager;
    private List<IEnemySpell> spells;
    private int[] spellPriority;

    private void Start() {
        spells = new List<IEnemySpell> {
            new EnemyLifeSteal(lifeStealManaCost, minLifeStealDamage, maxLifeStealDamage),
            new EnemyMindBlow(mindBlowManaCost, minMindBlowDamage, maxMindBlowDamage),
            new EnemyEmpower(empowerManaCost, empowerValue)
        };
        spellPriority = new int[spells.Count];
        playerCharacter = FindObjectOfType<Player>().GetComponent<Character>();
        enemy = GetComponent<Enemy>();
        enemyCharacter = GetComponent<Character>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update() {
        if (gameManager.IsEnemyPhase() && !enemy.IsFinishedAttacking()) {
            enemy.SetIsFinishedAttacking(true);
            Attack();
        }
    }

    private void Attack() {
        while (enemyCharacter.GetMana() >= Mathf.Min(lifeStealManaCost, mindBlowManaCost, empowerManaCost)) {
            for (int i = 0; i < spells.Count; i++) {
                spellPriority[i] = Random.Range(0, 100);
            }
            if (enemyCharacter.GetExtraDamage() > 0) {
                spellPriority[2] -= 100;
            }
            int spellToCastIndex = System.Array.IndexOf(spellPriority, spellPriority.Max());
            IEnemySpell spellToCast = spells[spellToCastIndex];
            while (spellToCast.ManaCost > enemyCharacter.GetMana()) {
                spellPriority[spellToCastIndex] = -100;
                spellToCastIndex = System.Array.IndexOf(spellPriority, spellPriority.Max());
                spellToCast = spells[spellToCastIndex];
            }
            spellToCast.DoSpellEffect(enemy, playerCharacter);
            enemyCharacter.DecreaseMana(spellToCast.ManaCost);
        }
        enemy.EndTurn();
    }
}
