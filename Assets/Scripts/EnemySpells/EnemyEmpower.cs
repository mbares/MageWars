using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmpower : MonoBehaviour, IEnemySpell {

    public int empowerValue;
    public SpellType spellType = SpellType.None;
    public int ManaCost { get; set; }

    public EnemyEmpower(int manaCost, int empowerValue) {
        ManaCost = manaCost;
        this.empowerValue = empowerValue;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        enemy.GetComponent<Character>().SetExtraDamage(empowerValue);
        Debug.Log(enemy.GetComponent<Character>().name + " empowers his next spell.");
    }
}
