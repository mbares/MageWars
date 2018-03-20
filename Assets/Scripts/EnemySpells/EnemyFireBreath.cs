using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBreath : MonoBehaviour, IEnemySpell {

    public int minFireBreathDamage;
    public int maxFireBreathDamage;
    public int burningDamage;
    public SpellType spellType = SpellType.Fire;

    private int manaCost;
    public int ManaCost { get { return manaCost; } set { manaCost = value; } }

    public EnemyFireBreath(int manaCost, int minFireBreathDamage, int maxFireBreathDamage, int burningDamage) {
        this.minFireBreathDamage = minFireBreathDamage;
        this.maxFireBreathDamage = maxFireBreathDamage;
        this.burningDamage = burningDamage;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        enemy.Attack(minFireBreathDamage, maxFireBreathDamage);
        target.SetIsBurning(Random.Range(1, 4), burningDamage);
        Debug.Log(enemy.GetComponent<Character>().name + " hits " + target.name + " with Fire Breath");
    }

}
