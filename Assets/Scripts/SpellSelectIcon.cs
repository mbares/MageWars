using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSelectIcon : MonoBehaviour {

    public GameObject spell;
    public GameObject spellBar;
    public Sprite spellSign;

    private SpellSelectController spellSelectController;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(delegate () { SpellInfo(); });
        spellSelectController = FindObjectOfType<SpellSelectController>();
    }

    public void AddSpellToLoadout() {
        if (!SpellIsPrepared() && Player.spellsPrepared.Count < 4) {
            Player.spellsPrepared.Add(spell);
            GameObject clone = Instantiate(gameObject, spellBar.transform.GetChild(GetIndexOfFreeSpace())) as GameObject;
            Button cloneButton = clone.GetComponent<Button>();
            cloneButton.onClick.RemoveAllListeners();
            cloneButton.onClick.AddListener(delegate () { RemoveSpellFromLoadout(); });
            clone.transform.localPosition = new Vector3(0, 0, 0);
            spellSelectController.UpdateAddSpellButton(this);
            Debug.Log(Player.spellsPrepared.Count);
            Debug.Log(Player.spellsPrepared[0]);
        } 
    }

    public void RemoveSpellFromLoadout() {
        foreach (Transform child in spellBar.transform) {
            if (child.childCount > 0 && child.GetChild(0).CompareTag(spell.tag)) {
                Destroy(child.GetChild(0).gameObject);
            }
        }
        foreach (GameObject spell in Player.spellsPrepared) {
            if (spell.CompareTag(this.spell.tag)) {
                Player.spellsPrepared.Remove(spell);
                break;
            }
        }
        spellSelectController.UpdateAddSpellButton(this);
    }

    public void SpellInfo() {
        spellSelectController.SpellInfo(this);
    }

    private bool SpellIsPrepared() {
        foreach (GameObject spell in Player.spellsPrepared) {
            if (this.spell.CompareTag(spell.tag)) {
                return true;
            }
        }
        return false;
    }

    private int GetIndexOfFreeSpace() {
        foreach (Transform child in spellBar.transform) {
            if (child.childCount == 0) {
                return child.GetSiblingIndex();
            }
        }
        return 0;
    }
}
