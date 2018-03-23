using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject spellBar;
    public GameObject manaBar;
    public GameObject drawArea;
    public GameObject castButton;
    public GameObject spellsToCastBar;
    public GameObject gesturePreview;

    private LevelManager levelManager;
    private Enemy enemy;
    private Player player;
    private List<ISpell> spellsToCast = new List<ISpell>();
    private ISpell spellToCast;
    private int spellsToCastIndex = 0;
    private int spellsToCastBarIndex = 0;
    private bool isPreparePhase = true;
    private bool isCastingPhase = false;
    private bool isEnemyPhase = false;

    void Start() {
        drawArea.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        for (int i = 0; i < Player.spellsPrepared.Count; i++) {
            spellBar.transform.GetChild(i).gameObject.SetActive(true);
            Instantiate(Player.spellsPrepared[i], spellBar.transform.GetChild(i));
        }
        for (int i = 19; i >= player.GetComponent<Character>().maxMana; i--) {
            manaBar.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (isCastingPhase) {
            spellToCast = spellsToCast[spellsToCastIndex];
        }
    }

    public void HandleCharacterDead(Character character) {
        if (character.gameObject.GetComponent<Player>() != null) {
            levelManager.LoadLevel("02b_Lose");
        } else {
            levelManager.NextLevel();
        }
    }

    public bool IsEnemyPhase() {
        return isEnemyPhase;
    }


    public bool IsCastingPhase() {
        return isCastingPhase;
    }

    public void StartPreparePhase() {
        isPreparePhase = true;
        castButton.SetActive(true);
        spellsToCastBar.SetActive(true);
        player.GetComponent<Character>().SetMana(player.GetComponent<Character>().maxMana);
        RefillMana();
        ClearSpellsToCastBar();
        if (!player.GetComponent<Character>().CalculateNewTurnStats()) {
            Debug.Log(player.GetComponent<Character>().name + " is stunned and is skipping turn");
            StartEnemyPhase();
        }
    }

    public void StartCastPhase() {
        if (spellsToCast.Count > 0) {
            isCastingPhase = true;
            drawArea.SetActive(true);
            castButton.SetActive(false);
            spellsToCastBar.SetActive(false);
            gesturePreview.GetComponent<GesturePreview>().ShowGesturePreview(spellsToCast[spellsToCastIndex].GestureId);
        }
    }

    private void StartEnemyPhase() {
        enemy.SetIsFinishedAttacking(false);
        spellsToCastBarIndex = 0;
        ClearSpellsToCastBar();
        isEnemyPhase = true;
        isCastingPhase = false;
        drawArea.SetActive(false);
        spellsToCast.Clear();
        spellsToCastIndex = 0;
        if (!enemy.GetComponent<Character>().CalculateNewTurnStats()) {
            Debug.Log(enemy.GetComponent<Character>().name + " is stunned and is skipping turn");
            StartPreparePhase();
        }
    }

    public ISpell GetSpellToCast() {
        return spellToCast;
    }

    public void IncrementSpellsToCastIndex() {
        spellsToCastIndex++;
        if (spellsToCastIndex >= spellsToCast.Count) {
            StartEnemyPhase();
            return;
        }
        gesturePreview.GetComponent<GesturePreview>().ShowGesturePreview(spellsToCast[spellsToCastIndex].GestureId);
    }

    public void AddSpellToCast(ISpell spell) {
        if (spell.ManaCost <= player.GetComponent<Character>().GetMana()) {
            spellsToCast.Add(spell);
            GameObject spellToCastBarSlot = spellsToCastBar.transform.GetChild(spellsToCastBarIndex).gameObject;
            spellToCastBarSlot.SetActive(true);
            spellToCastBarSlot.GetComponent<PreparedSpell>().spell = spell;
            spellToCastBarSlot.GetComponent<Image>().sprite = ((MonoBehaviour)spell).gameObject.GetComponent<Image>().sprite;
            spellsToCastBarIndex++;
            player.GetComponent<Character>().DecreaseMana(spell.ManaCost);
            for (int i = player.GetComponent<Character>().maxMana; i >= player.GetComponent<Character>().GetMana(); i--) {
                Color tmp = manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color;
                tmp.a = 0.5f;
                manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = tmp;
            }
        }
    }

    public void RemoveSpellToCast(ISpell spell) {
        int index = spellsToCast.LastIndexOf(spell);
        spellsToCast.Remove(spell);
        GameObject spellToCastBarSlot = spellsToCastBar.transform.GetChild(index).gameObject;
        for (int i = index; i < spellsToCastBarIndex; i++) {
            if (spellsToCastBar.transform.GetChild(i + 1).gameObject.activeInHierarchy) {
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<PreparedSpell>().spell = spellsToCastBar.transform.GetChild(i + 1).gameObject.GetComponent<PreparedSpell>().spell;
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = spellsToCastBar.transform.GetChild(i + 1).gameObject.GetComponent<Image>().sprite;
            } else {
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<PreparedSpell>().spell = null;
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
                spellsToCastBar.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
        spellsToCastBarIndex--;
        player.GetComponent<Character>().IncreaseMana(spell.ManaCost);
        RefillMana();
    }

    private void RefillMana()
    {
        for (int i = 0; i < player.GetComponent<Character>().GetMana(); i++) {
            Color tmp = manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color;
            tmp.a = 1f;
            manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = tmp;
        }
    }

    private void ClearSpellsToCastBar() {
        for (int i = 0; i < spellsToCastBar.transform.childCount; i++) {
            spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<PreparedSpell>().spell = null;
            spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            spellsToCastBar.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
