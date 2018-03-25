using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElementalShield : IEnemySpell {

    public int elementalShieldDuration;
    public int elementalShieldDamage;
    public SpellType spellType;
    public int ManaCost { get; set; }

    public EnemyElementalShield(int manaCost, int elementalShieldDuration, int elementalShieldDamage, SpellType spellType) {
        ManaCost = manaCost;
        this.elementalShieldDuration = elementalShieldDuration;
        this.elementalShieldDamage = elementalShieldDamage;
        this.spellType = spellType;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        enemy.GetComponent<Character>().SetHasElementalShield(spellType, elementalShieldDuration, elementalShieldDamage);
        Debug.Log(enemy.GetComponent<Character>().name + " surrounds himself with Elemental Shield");
    }
}
