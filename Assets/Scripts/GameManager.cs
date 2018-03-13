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

    private Player player;
    private Enemy enemy;
    private List<Spell> spellsToCast = new List<Spell>();
    private Spell spellToCast;
    private int spellsToCastIndex = 0;
    private int spellsToCastBarIndex = 0;
    private bool castPhase = false;
    private bool preparePhase = true;

    // Use this for initialization
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
        for (int i = 0; i < player.GetComponent<Character>().maxMana; i++)
        {
            manaBar.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (castPhase)
        {
            spellToCast = spellsToCast[spellsToCastIndex];
        }
        else if (preparePhase)
        {

        }
        else 
        {

        }
    }

    public void StartCastPhase()
    {
        if (spellsToCast.Count > 0)
        {
            castPhase = true;
            drawArea.SetActive(true);
            castButton.SetActive(false);
        }
    }

    public void StartPreparePhase()
    {
        preparePhase = true;
        castButton.SetActive(true);
    }

    private void StartEnemyPhase()
    {
        castPhase = false;
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
        }
    }

    public bool IsCastPhase()
    {
        return castPhase;
    }

    public void AddSpellToCast(Spell spell)
    {
        spellsToCast.Add(spell);
        Transform spellToCastBarSlot = spellsToCastBar.transform.GetChild(spellsToCastBarIndex);
        Instantiate(spell, spellsToCastBar.transform.GetChild(spellsToCastBarIndex));
        spellsToCastBarIndex++;
    }
}
