using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : MonoBehaviour {

    public int minFireBreathDamage = 2;
    public int maxFireBreathDamage = 4;
    public int burningDamage = 2;
    public int fireShieldDamage = 1;
    public int fireShieldDuration = 3;
    public int smokeScreenDuration = 2;

    private Character playerCharacter;
    private Enemy enemy;
    private Character enemyCharacter;
    private GameManager gameManager;
    private List<IEnemySpell> spells;
    private int[] spellPriority;

    private void Start() {
        spells = new List<IEnemySpell> {
            new EnemyFireBreath(3, minFireBreathDamage, maxFireBreathDamage, burningDamage),
            new EnemyElementalShield(2, fireShieldDuration, fireShieldDamage, SpellType.Fire),
            new EnemySmokeScreen(2, smokeScreenDuration)
        };
        spellPriority = new int[spells.Count];
        playerCharacter = FindObjectOfType<Player>().GetComponent<Character>();
        enemy = GetComponent<Enemy>();
        enemyCharacter = GetComponent<Character>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update() {
        if (gameManager.IsEnemyPhase() && !enemy.IsFinishedAttacking()) {
            for (int i = 0; i < spells.Count; i++) {
                spellPriority[i] = Random.Range(0, 100);
            }
            if (enemyCharacter.HasElementalShield()) {
                spellPriority[1] -= 30;
            }
            if (playerCharacter.IsBlinded()) {
                spellPriority[2] -= 30;
            }
            enemy.SetIsFinishedAttacking(true);
            enemy.EndTurn();
        }
    }
}
