using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeSteal : IEnemySpell {

    public int minLifeStealDamage;
    public int maxLifeStealDamage;
    public SpellType spellType = SpellType.Dark;
    public int ManaCost { get; set; }

    public EnemyLifeSteal(int manaCost, int minLifeStealDamage, int maxLifeStealDamage) {
        ManaCost = manaCost;
        this.minLifeStealDamage = minLifeStealDamage;
        this.maxLifeStealDamage = maxLifeStealDamage;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        enemy.Attack(minLifeStealDamage, maxLifeStealDamage, spellType, true);
        Debug.Log(enemy.GetComponent<Character>().name + " hits " + target.name + " with Life Steal");
    }
}
