using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireMage : MonoBehaviour {

    public int fireBreathManaCost = 3;
    public int minFireBreathDamage = 2;
    public int maxFireBreathDamage = 4;
    public int burningDamage = 2;
    public int fireShieldManaCost = 2;
    public int fireShieldDamage = 1;
    public int fireShieldDuration = 3;
    public int smokeScreenManaCost = 2;
    public int smokeScreenDuration = 2;

    private Character playerCharacter;
    private Enemy enemy;
    private Character enemyCharacter;
    private GameManager gameManager;
    private List<IEnemySpell> spells;
    private int[] spellPriority;

    private void Start() {
        spells = new List<IEnemySpell> {
            new EnemyFireBreath(fireBreathManaCost, minFireBreathDamage, maxFireBreathDamage, burningDamage),
            new EnemyElementalShield(fireShieldManaCost, fireShieldDuration, fireShieldDamage, SpellType.Fire),
            new EnemySmokeScreen(smokeScreenManaCost, smokeScreenDuration)
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
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack() {
        int fireShieldCastedCount = 0;
        int smokeScreenCastedCount = 0;
        while (enemyCharacter.GetMana() >= Mathf.Min(fireBreathManaCost, fireShieldManaCost, smokeScreenManaCost)) {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < spells.Count; i++) {
                spellPriority[i] = Random.Range(0, 100);
            }
            if (enemyCharacter.HasElementalShield()) {
                spellPriority[1] -= (50 + (-50 * fireShieldCastedCount));
            }
            if (playerCharacter.IsBlinded()) {
                spellPriority[2] -= (50 + (-50 * smokeScreenCastedCount));
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
            if (spellToCast is EnemyElementalShield) {
                fireShieldCastedCount++;
            } else if (spellToCast is EnemySmokeScreen) {
                smokeScreenCastedCount++;
            }
        }
        enemy.EndTurn();
    }
}
