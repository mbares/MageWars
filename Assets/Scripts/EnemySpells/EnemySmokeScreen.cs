using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmokeScreen : MonoBehaviour, IEnemySpell {

    public int smokeScreenDuration;

    private int manaCost;
    public int ManaCost { get { return manaCost; } set { manaCost = value; } }

    public EnemySmokeScreen(int manaCost, int smokeScreenDuration) {
        this.manaCost = manaCost;
        this.smokeScreenDuration = smokeScreenDuration;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        target.SetIsBlinded(smokeScreenDuration);
        Debug.Log(enemy.GetComponent<Character>().name + " conjures dense smoke in " + target.name + "'s face with Smoke Screen.");
    }
}
