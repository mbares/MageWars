using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedSpell : MonoBehaviour {

    public ISpell spell;

    private GameManager gameManager;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void RemoveSpellToCast() {
        gameManager.RemoveSpellToCast(spell);
    }
}
