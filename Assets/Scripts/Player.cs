using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static List<GameObject> spellsPrepared = new List<GameObject>();

    private Character playerCharacter;
    private Character enemyTarget;
    private List<GameObject> spellsKnown;
    private int level;

    private void Start() {
        level = PlayerPrefsManager.GetPlayerLevel();
        playerCharacter = GetComponent<Character>();
        enemyTarget = FindObjectOfType<Enemy>().GetComponent<Character>();
    }

    public void Attack(int damage, SpellType spellType = SpellType.None, bool lifesteal = false) {
        damage += playerCharacter.GetExtraDamage();
        if (playerCharacter.IsBlinded() && Random.Range(0, 4) == 0) {
            Debug.Log(playerCharacter.name + " misses the attack because of blind effect.");
            return;
        }
        if (playerCharacter.IsConfused() && Random.Range(0, 5) == 0) {
            playerCharacter.DecreaseHealthBySpellDamage(damage, spellType);
            Debug.Log("In it's confusion " + playerCharacter.name + "'s attack backfires.");
        } else {
            enemyTarget.DecreaseHealthBySpellDamage(damage, spellType);
        }
        if (lifesteal) {
            playerCharacter.IncreaseHealth(damage / 2);
        }
        playerCharacter.SetExtraDamage(0);
    }
}
