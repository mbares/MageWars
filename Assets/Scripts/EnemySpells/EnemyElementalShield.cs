using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElementalShield : MonoBehaviour, IEnemySpell {

    public int elementalShieldDuration;
    public int elementalShieldDamage;
    public SpellType spellType;

    private int manaCost;
    public int ManaCost { get { return manaCost; } set { manaCost = value; } }

    public EnemyElementalShield(int manaCost, int elementalShieldDuration, int fireShieldDamage, SpellType spellType) {
        this.manaCost = manaCost;
        this.elementalShieldDuration = elementalShieldDuration;
        this.elementalShieldDamage = fireShieldDamage;
        this.spellType = spellType;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        enemy.GetComponent<Character>().SetHasElementalShield(true, spellType, elementalShieldDuration, elementalShieldDamage);
        Debug.Log(enemy.GetComponent<Character>().name + " surrounds himself with Elemental Shield");
    }
}
