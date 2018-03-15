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

    private Enemy enemy;
    private Player player;
    private List<Spell> spellsToCast = new List<Spell>();
    private Spell spellToCast;
    private int spellsToCastIndex = 0;
    private int spellsToCastBarIndex = 0;
    private bool isPreparePhase = true;
    private bool isCastingPhase = false;
    private bool isEnemyPhase = false;

    void Start()
    {
        drawArea.SetActive(false);
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        for (int i = 0; i < player.spellsPrepared.Count; i++)
        {
            spellBar.transform.GetChild(i).gameObject.SetActive(true);
            Instantiate(player.spellsPrepared[i], spellBar.transform.GetChild(i));
        }
        for (int i = 19; i >= player.GetComponent<Character>().maxMana; i--)
        {
            manaBar.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isCastingPhase)
        {
            spellToCast = spellsToCast[spellsToCastIndex];
        }
    }

    public bool IsEnemyPhase()
    {
        return isEnemyPhase;
    }


    public bool IsCastingPhase()
    {
        return isCastingPhase;
    }

    public void StartPreparePhase()
    {
        isPreparePhase = true;
        castButton.SetActive(true);
        spellsToCastBar.SetActive(true);
        player.GetComponent<Character>().SetMana(player.GetComponent<Character>().maxMana);
        RefillMana();
        ClearSpellsToCastBar();
    }

    public void StartCastPhase()
    {
        if (spellsToCast.Count > 0)
        {
            isCastingPhase = true;
            drawArea.SetActive(true);
            castButton.SetActive(false);
            spellsToCastBar.SetActive(false);
            gesturePreview.GetComponent<GesturePreview>().ShowGesturePreview(spellsToCast[spellsToCastIndex].gestureId);
        }
    }

    private void StartEnemyPhase()
    {
        enemy.SetIsFinishedAttacking(false);
        spellsToCastBarIndex = 0;
        ClearSpellsToCastBar();
        isEnemyPhase = true;
        isCastingPhase = false;
        drawArea.SetActive(false);
        spellsToCast.Clear();
        spellsToCastIndex = 0;
    }

    public string GetSpellToCastGestureId()
    {
        return spellToCast.gestureId;
    }

    public void IncrementSpellsToCastIndex()
    {
        spellsToCastIndex++;
        if (spellsToCastIndex >= spellsToCast.Count)
        {
            StartEnemyPhase();
            return;
        }
        gesturePreview.GetComponent<GesturePreview>().ShowGesturePreview(spellsToCast[spellsToCastIndex].gestureId);
    }

    public void AddSpellToCast(Spell spell)
    {
        if (spell.manaCost <= player.GetComponent<Character>().GetMana())
        {
            spellsToCast.Add(spell);
            GameObject spellToCastBarSlot = spellsToCastBar.transform.GetChild(spellsToCastBarIndex).gameObject;
            spellToCastBarSlot.SetActive(true);
            spellToCastBarSlot.GetComponent<PreparedSpell>().spell = spell;
            spellToCastBarSlot.GetComponent<Image>().sprite = spell.gameObject.GetComponent<Image>().sprite;
            spellsToCastBarIndex++;
            player.GetComponent<Character>().SetMana(player.GetComponent<Character>().GetMana() - spell.manaCost);
            for (int i = player.GetComponent<Character>().maxMana; i >= player.GetComponent<Character>().GetMana(); i--)
            {
                Color tmp = manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color;
                tmp.a = 0.5f;
                manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = tmp;
            }
        }
    }

    public void RemoveSpellToCast(Spell spell)
    {
        int index = spellsToCast.LastIndexOf(spell);
        spellsToCast.Remove(spell);
        GameObject spellToCastBarSlot = spellsToCastBar.transform.GetChild(index).gameObject;
        for (int i = index; i < spellsToCastBarIndex; i++)
        {
            if (spellsToCastBar.transform.GetChild(i + 1).gameObject.activeInHierarchy)
            {
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<PreparedSpell>().spell = spellsToCastBar.transform.GetChild(i + 1).gameObject.GetComponent<PreparedSpell>().spell;
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = spellsToCastBar.transform.GetChild(i + 1).gameObject.GetComponent<Image>().sprite;
            }
            else
            {
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<PreparedSpell>().spell = null;
                spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
                spellsToCastBar.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
        spellsToCastBarIndex--;
        player.GetComponent<Character>().SetMana(player.GetComponent<Character>().GetMana() + spell.manaCost);
        RefillMana();
    }

    private void RefillMana()
    {
        for (int i = 0; i < player.GetComponent<Character>().GetMana(); i++)
        {
            Color tmp = manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color;
            tmp.a = 1f;
            manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = tmp;
        }
    }

    private void ClearSpellsToCastBar()
    {
        for (int i = 0; i < spellsToCastBar.transform.childCount; i++)
        {
            spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<PreparedSpell>().spell = null;
            spellsToCastBar.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            spellsToCastBar.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
