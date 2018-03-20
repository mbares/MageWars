using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmokeScreen : MonoBehaviour, IEnemySpell {

    public int manaCost;
    public int smokeScreenDuration;

    public EnemySmokeScreen(int manaCost, int smokeScreenDuration) {
        this.manaCost = manaCost;
        this.smokeScreenDuration = smokeScreenDuration;
    }

    public void DoSpellEffect(Enemy enemy, Character target) {
        target.SetIsBlinded(smokeScreenDuration);
    }
}
