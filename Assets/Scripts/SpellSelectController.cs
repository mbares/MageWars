using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSelectController : MonoBehaviour {

    public Text levelText;
    public Text hpText;
    public Text mpText;
    public Text spellName;
    public Text spellType;
    public Text spellManaCost;
    public Text spellDescription;
    public Image spellSign;
    public Button addSpellButton;
    public Button backButton;
    public Button startButton;

	void Start () {
        levelText.text = PlayerPrefsManager.GetPlayerLevel().ToString();
        hpText.text = PlayerPrefsManager.GetPlayerMaxHealth().ToString();
        mpText.text = PlayerPrefsManager.GetPlayerMaxMana().ToString();
        backButton.onClick.AddListener(delegate { FindObjectOfType<LevelManager>().LoadLevel("00a_Start_Menu"); });
        spellName.text = null;
        spellType.text = null;
        spellManaCost.text = null;
        spellDescription.text = null;
        spellSign.gameObject.SetActive(false);
        startButton.gameObject.GetComponent<Text>().color = Color.grey;
    }
	
    public void SpellInfo(SpellSelectIcon spellSelectIcon) {
        if (!spellSign.gameObject.activeInHierarchy) {
            spellSign.gameObject.SetActive(true);
        }
        ISpell spell = spellSelectIcon.spell.GetComponent<ISpell>();
        spellName.text = spell.Name;
        spellType.text = spell.SpellType.ToString();
        spellManaCost.text = spell.ManaCost.ToString();
        spellDescription.text = spell.Description;
        spellSign.sprite = spellSelectIcon.spellSign;
        UpdateAddSpellButton(spellSelectIcon);
    }

    public void UpdateAddSpellButton(SpellSelectIcon spellSelectIcon) {
        addSpellButton.onClick.RemoveAllListeners();
        if (SpellIsPrepared(spellSelectIcon.spell)) {
            if (Player.spellsPrepared.Count == 4) {
                startButton.onClick.AddListener(delegate { FindObjectOfType<LevelManager>().NextLevel(); });
                startButton.gameObject.GetComponent<Text>().color = Color.cyan;
            }
            addSpellButton.gameObject.GetComponent<Text>().text = "REMOVE SPELL";
            addSpellButton.onClick.AddListener(delegate () { spellSelectIcon.RemoveSpellFromLoadout(); });
        } else {
            startButton.onClick.RemoveAllListeners();
            startButton.gameObject.GetComponent<Text>().color = Color.grey;
            addSpellButton.gameObject.GetComponent<Text>().text = "ADD SPELL";
            addSpellButton.onClick.AddListener(delegate () { spellSelectIcon.AddSpellToLoadout(); });
        }
    }

    private bool SpellIsPrepared(GameObject spell) {
        foreach (GameObject preparedSpell in Player.spellsPrepared) {
            if (spell.CompareTag(preparedSpell.tag)) {
                return true;
            }
        }
        return false;
    }
}
