using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmokeScreen : IEnemySpell {

    public int smokeScreenDuration;
    public int ManaCost { get; set; }

    public EnemySmokeScreen(int manaCost, int smokeScreenDuration) {
        ManaCost = manaCost;
        this.smokeScreenDuration = smokeScreenDuration;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        target.SetIsBlinded(smokeScreenDuration);
        Debug.Log(enemy.GetComponent<Character>().name + " conjures dense smoke in " + target.name + "'s face with Smoke Screen.");
    }
}
