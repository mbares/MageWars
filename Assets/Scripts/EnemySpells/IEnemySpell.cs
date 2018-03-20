using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpell {

    int ManaCost { get; set; }

    void DoSpellEffect(Enemy enemy, Character target);
}
