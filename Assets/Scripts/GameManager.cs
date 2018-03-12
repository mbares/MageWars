using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject spellBar;
    public GameObject manaBar;
    public GameObject drawArea;
    public GameObject castButton;

    private Player player;
    private Enemy enemy;
    private List<Spell> spellsToCast = new List<Spell>();
    private Spell spellToCast;
    private int spellsToCastIndex = 0;
    private bool castPhase = false;

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

    private void EndCastPhase()
    {
        castPhase = false;
        drawArea.SetActive(false);
        spellsToCast.Clear();
        spellsToCastIndex = 0;
        castButton.SetActive(true);
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
            EndCastPhase();
        }
    }

    public bool IsCastPhase()
    {
        return castPhase;
    }

    public void AddSpellToCast(Spell spell)
    {
        spellsToCast.Add(spell);
    }
}
