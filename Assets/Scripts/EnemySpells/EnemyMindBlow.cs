using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMindBlow : IEnemySpell {

    public int minMindBlowDamage;
    public int maxMindBlowDamage;
    public SpellType spellType = SpellType.Dark;
    public int ManaCost { get; set; }

    public EnemyMindBlow(int manaCost, int minMindBlowDamage, int maxMindBlowDamage) {
        ManaCost = manaCost;
        this.minMindBlowDamage = minMindBlowDamage;
        this.maxMindBlowDamage = maxMindBlowDamage;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        enemy.Attack(minMindBlowDamage, maxMindBlowDamage, spellType);
        target.SetIsConfused(Random.Range(1, 4));
        Debug.Log(enemy.GetComponent<Character>().name + " hits and confuses " + target.name + " with Mind Blow.");
    }
}
