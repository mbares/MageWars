using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public string name;
    public int maxHealth;
    public int maxMana;
    public List<SpellType> resistances;
    public List<SpellType> weaknesses;
    public StatusEffectsBar statusEffectsBar;

    private int health;
    private int mana;
    private Dictionary<StatusEffect, string> statusEffects = new Dictionary<StatusEffect, string>();
    private bool isBlinded;
    private int blindedTurns;
    private bool isBurning;
    private int burningTurns;
    private int burningDamage = 0;
    private bool isWet;
    private int wetTurns;
    private bool isStunned;
    private bool isConfused;
    private int confusedTurns;
    private bool hasElementalShield;
    private SpellType elementalShieldType;
    private int elementalShieldTurns;
    private int elementalShieldDamage = 0;

    private void Start() {
        health = maxHealth;
        mana = maxMana;
    }

    public void SetHealth(int health) {
        this.health = health;
    }

    public int GetHealth() {
        return health;
    }

    public void SetMana(int mana) {
        this.mana = mana;
    }

    public int GetMana() {
        return mana;
    }

    public void IncreaseHealth(int value) {
        if ((health + value) > maxHealth) {
            health = maxHealth;
        } else {
            health += value;
        }
    }

    public void DecreaseHealth(int value) {
        health -= value;
    }

    public void DecreaseHealthBySpellDamage(int value, SpellType spellType) {
        if (resistances.Contains(spellType)) {
            value /= 2;
            Debug.Log(name + " is resistant to " + spellType + " damage, damage is halved");
        } else if (weaknesses.Contains(spellType)) {
            value *= 2;
            Debug.Log(name + " is weak to " + spellType + " damage, damage is doubled");
        }
        if (HasElementalShield()) {
            if (GetComponent<Player>() != null) {
                FindObjectOfType<Enemy>().GetComponent<Character>().DecreaseHealthBySpellDamage(elementalShieldDamage, elementalShieldType);
            } else if (GetComponent<Enemy>() != null) {
                FindObjectOfType<Player>().GetComponent<Character>().DecreaseHealthBySpellDamage(elementalShieldDamage, elementalShieldType);
            }
        }
        health -= value;
    }

    public void IncreaseMana(int value) {
        mana += value;
    }

    public void DecreaseMana(int value) {
        mana -= value;
    }

    public bool IsBlinded() {
        return isBlinded;
    }

    public void SetIsBlinded(int turns) {
        if (turns > 0 && turns > blindedTurns) {
            isBlinded = true;
            if (!statusEffects.ContainsKey(StatusEffect.Blinded)) {
                statusEffects.Add(StatusEffect.Blinded, turns.ToString());
            } else {
                statusEffects[StatusEffect.Blinded] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            isBlinded = false;
            statusEffects.Remove(StatusEffect.Blinded);
            statusEffectsBar.UpdateBar(statusEffects);
        }
        blindedTurns = turns;
    }

    public bool IsBurning() {
        return isBurning;
    }

    public void SetIsBurning(int turns, int damage) {
        if (turns > 0 && turns > burningTurns) {
            isBurning = true;
            if (!statusEffects.ContainsKey(StatusEffect.Burning)) {
                statusEffects.Add(StatusEffect.Burning, turns.ToString());
            } else {
                statusEffects[StatusEffect.Burning] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            isBurning = false;
            statusEffects.Remove(StatusEffect.Burning);
            statusEffectsBar.UpdateBar(statusEffects);
        }
        burningTurns = turns;
        burningDamage = damage;
    }

    public bool IsWet() {
        return isWet;
    }

    public void SetIsWet(int turns) {
        if (turns > 0 && turns > wetTurns) {
            isWet = true;
            if (!statusEffects.ContainsKey(StatusEffect.Wet)) {
                statusEffects.Add(StatusEffect.Wet, turns.ToString());
            } else {
                statusEffects[StatusEffect.Wet] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            isWet = false;
            statusEffects.Remove(StatusEffect.Wet);
            statusEffectsBar.UpdateBar(statusEffects);
        }
        wetTurns = turns;
    }

    public bool IsConfused() {
        return isConfused;
    }

    public void SetIsConfused(int turns) {
        if (turns > 0 && turns > confusedTurns) {
            isBurning = true;
            if (!statusEffects.ContainsKey(StatusEffect.Confused)) {
                statusEffects.Add(StatusEffect.Confused, turns.ToString());
            } else {
                statusEffects[StatusEffect.Confused] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            isBurning = false;
            statusEffects.Remove(StatusEffect.Confused);
            statusEffectsBar.UpdateBar(statusEffects);
        }
        confusedTurns = turns;
    }

    public bool IsStunned() {
        return isStunned;
    }

    public void SetIsStunned(bool isStunned) {
        this.isStunned = isStunned;
    }

    public bool HasElementalShield() {
        return hasElementalShield;
    }

    public void SetHasElementalShield(bool hasElementalShield, SpellType type, int turns, int damage) {
        if (turns > 0 && turns > elementalShieldTurns) {
            hasElementalShield = true;
            elementalShieldType = type;
            elementalShieldDamage = damage;
            if (!statusEffects.ContainsKey(StatusEffect.ElementalShield)) {
                statusEffects.Add(StatusEffect.ElementalShield, turns.ToString());
            } else {
                statusEffects[StatusEffect.ElementalShield] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            hasElementalShield = false;
            statusEffects.Remove(StatusEffect.ElementalShield);
            statusEffectsBar.UpdateBar(statusEffects);
        }
    }

    public bool CalculateNewTurnStats() {
        if (blindedTurns > 0) {
            blindedTurns--;
        }
        if (burningTurns > 0) {
            DecreaseHealth(burningDamage);
            Debug.Log(name + " suffers burning damage.");
            burningTurns--;
        }
        if (wetTurns > 0) {
            wetTurns--;
        }
        if (blindedTurns == 0) {
            isBlinded = false;
            statusEffects.Remove(StatusEffect.Blinded);
        } else if (blindedTurns > 0) {
            statusEffects[StatusEffect.Blinded] = blindedTurns.ToString();
        }
        if (burningTurns == 0) {
            isBurning = false;
            burningDamage = 0;
            statusEffects.Remove(StatusEffect.Burning);
        } else if (burningTurns > 0) {
            statusEffects[StatusEffect.Burning] = burningTurns.ToString();
        }
        if (wetTurns == 0) {
            isWet = false;
            statusEffects.Remove(StatusEffect.Wet);
        } else if (wetTurns > 0) {
            statusEffects[StatusEffect.Wet] = wetTurns.ToString();
        }
        statusEffectsBar.UpdateBar(statusEffects);
        if (isStunned) {
            isStunned = false;
            return false;
        }
        return true;
    }
}
