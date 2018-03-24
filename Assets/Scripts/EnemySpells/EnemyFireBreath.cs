using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBreath : IEnemySpell {

    public int minFireBreathDamage;
    public int maxFireBreathDamage;
    public int burningDamage;
    public SpellType spellType = SpellType.Fire;
    public int ManaCost { get; set; }

    public EnemyFireBreath(int manaCost, int minFireBreathDamage, int maxFireBreathDamage, int burningDamage) {
        ManaCost = manaCost;
        this.minFireBreathDamage = minFireBreathDamage;
        this.maxFireBreathDamage = maxFireBreathDamage;
        this.burningDamage = burningDamage;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        enemy.Attack(minFireBreathDamage, maxFireBreathDamage, spellType);
        target.SetIsBurning(Random.Range(1, 4), burningDamage);
        Debug.Log(enemy.GetComponent<Character>().name + " hits " + target.name + " with Fire Breath");
    }
}
