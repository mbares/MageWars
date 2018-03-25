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

    private FloatingTextController floatingTextController;
    private int health;
    private int mana;
    private Dictionary<StatusEffect, string> statusEffects = new Dictionary<StatusEffect, string>();
    private bool isBlinded;
    private int blindedTurns = 0;
    private bool isBurning;
    private int burningTurns = 0;
    private int burningDamage = 0;
    private bool isWet;
    private int wetTurns = 0;
    private bool isStunned;
    private bool isConfused;
    private int confusedTurns = 0;
    private bool hasElementalShield;
    private SpellType elementalShieldType;
    private int elementalShieldTurns = 0;
    private int elementalShieldDamage = 0;
    private int empoweredDamage = 0;
    private List<SpellType> temporaryResistances = new List<SpellType>();

    private void Awake() {
        floatingTextController = FindObjectOfType<FloatingTextController>();
        if (gameObject.GetComponent<Player>() != null) {
            maxHealth = PlayerPrefsManager.GetPlayerMaxHealth();
            maxMana = PlayerPrefsManager.GetPlayerMaxMana();
        } 
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
        floatingTextController.CreateFloatingText("+" + value, transform, Color.green);
    }

    public void DecreaseHealth(int value) {
        GetComponent<Animator>().SetTrigger("damaged");
        floatingTextController.CreateFloatingText("-" + value, transform, Color.red);
        health -= value;
    }

    public void DecreaseHealthBySpellDamage(int value, SpellType spellType) {
        if (resistances.Contains(spellType) || temporaryResistances.Contains(spellType)) {
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
                Debug.Log("PLAYER GETS BURNED BY SHIELD FOR " + elementalShieldDamage);
                FindObjectOfType<Player>().GetComponent<Character>().DecreaseHealthBySpellDamage(elementalShieldDamage, elementalShieldType);
            }
        }
        DecreaseHealth(value);
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
            blindedTurns = turns;
            floatingTextController.CreateFloatingText("Blinded", transform, Color.yellow);
            if (!statusEffects.ContainsKey(StatusEffect.Blinded)) {
                statusEffects.Add(StatusEffect.Blinded, turns.ToString());
            } else {
                statusEffects[StatusEffect.Blinded] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            isBlinded = false;
            blindedTurns = turns;
            statusEffects.Remove(StatusEffect.Blinded);
            statusEffectsBar.UpdateBar(statusEffects);
        }
    }

    public bool IsBurning() {
        return isBurning;
    }

    public void SetIsBurning(int turns, int damage) {
        if (turns > 0 && turns > burningTurns) {
            isBurning = true;
            burningTurns = turns;
            burningDamage = damage;
            floatingTextController.CreateFloatingText("Burning", transform, Color.yellow);
            if (!statusEffects.ContainsKey(StatusEffect.Burning)) {
                statusEffects.Add(StatusEffect.Burning, turns.ToString());
            } else {
                statusEffects[StatusEffect.Burning] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            isBurning = false;
            burningTurns = turns;
            burningDamage = damage;
            statusEffects.Remove(StatusEffect.Burning);
            statusEffectsBar.UpdateBar(statusEffects);
        }
    }

    public bool IsWet() {
        return isWet;
    }

    public void SetIsWet(int turns) {
        if (turns > 0 && turns > wetTurns) {
            isWet = true;
            wetTurns = turns;
            floatingTextController.CreateFloatingText("Wet", transform, Color.yellow);
            if (!statusEffects.ContainsKey(StatusEffect.Wet)) {
                statusEffects.Add(StatusEffect.Wet, turns.ToString());
            } else {
                statusEffects[StatusEffect.Wet] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            isWet = false;
            wetTurns = turns;
            statusEffects.Remove(StatusEffect.Wet);
            statusEffectsBar.UpdateBar(statusEffects);
        }
    }

    public bool IsConfused() {
        return isConfused;
    }

    public void SetIsConfused(int turns) {
        if (turns > 0 && turns > confusedTurns) {
            isConfused = true;
            confusedTurns = turns;
            floatingTextController.CreateFloatingText("Confused", transform, Color.yellow);
            if (!statusEffects.ContainsKey(StatusEffect.Confused)) {
                statusEffects.Add(StatusEffect.Confused, turns.ToString());
            } else {
                statusEffects[StatusEffect.Confused] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            confusedTurns = turns;
            isConfused = false;
            statusEffects.Remove(StatusEffect.Confused);
            statusEffectsBar.UpdateBar(statusEffects);
        }
    }

    public bool IsStunned() {
        return isStunned;
    }

    public void SetIsStunned(bool isStunned) {
        floatingTextController.CreateFloatingText("Stunned", transform, Color.yellow);
        this.isStunned = isStunned;
    }

    public bool HasElementalShield() {
        return hasElementalShield;
    }

    public void SetHasElementalShield(SpellType type, int turns, int damage) {
        Debug.Log("Setting shield, turns: " + turns);
        if (turns > 0 && turns > elementalShieldTurns) {
            hasElementalShield = true;
            floatingTextController.CreateFloatingText("Shielded", transform, Color.yellow);
            if (!resistances.Contains(type)) {
                temporaryResistances.Add(type);
            }
            elementalShieldTurns = turns;
            elementalShieldType = type;
            elementalShieldDamage = damage;
            if (!statusEffects.ContainsKey(StatusEffect.ElementalShield)) {
                statusEffects.Add(StatusEffect.ElementalShield, turns.ToString());
            } else {
                statusEffects[StatusEffect.ElementalShield] = turns.ToString();
            }
            statusEffectsBar.UpdateBar(statusEffects);
        } else if (turns == 0) {
            elementalShieldTurns = turns;
            temporaryResistances.Remove(type);
            hasElementalShield = false;
            statusEffects.Remove(StatusEffect.ElementalShield);
            statusEffectsBar.UpdateBar(statusEffects);
        }
    }

    public int GetEmpoweredDamage() {
        return empoweredDamage;
    }

    public void SetEmpowered(int empoweredDamage) {
        this.empoweredDamage = empoweredDamage;
        if (empoweredDamage > 0) {
            if (!statusEffects.ContainsKey(StatusEffect.Empowered)) {
                statusEffects.Add(StatusEffect.Empowered, "");
                floatingTextController.CreateFloatingText("Empowered", transform, Color.yellow);
                statusEffectsBar.UpdateBar(statusEffects);
            } 
        } else {
            statusEffects.Remove(StatusEffect.Empowered);
            statusEffectsBar.UpdateBar(statusEffects);
        }
    }

    public void CalculateEndTurnStats() {
        if (confusedTurns > 0) {
            confusedTurns--;
        }
        if (blindedTurns > 0) {
            blindedTurns--;
        }
        if (wetTurns > 0) {
            wetTurns--;
        }

        if (confusedTurns == 0) {
            isConfused = false;
            statusEffects.Remove(StatusEffect.Confused);
        } else if (confusedTurns > 0) {
            statusEffects[StatusEffect.Confused] = confusedTurns.ToString();
        }
        if (blindedTurns == 0) {
            isBlinded = false;
            statusEffects.Remove(StatusEffect.Blinded);
        } else if (blindedTurns > 0) {
            statusEffects[StatusEffect.Blinded] = blindedTurns.ToString();
        }
        if (wetTurns == 0) {
            isWet = false;
            statusEffects.Remove(StatusEffect.Wet);
        } else if (wetTurns > 0) {
            statusEffects[StatusEffect.Wet] = wetTurns.ToString();
        }
        statusEffectsBar.UpdateBar(statusEffects);
    }

    public bool CalculateNewTurnStats() {
        if (elementalShieldTurns > 0) {
            elementalShieldTurns--;
        }
        if (burningTurns > 0) {
            DecreaseHealth(burningDamage);
            Debug.Log(name + " suffers burning damage.");
            burningTurns--;
        }
        
        if (elementalShieldTurns == 0) {
            hasElementalShield = false;
            temporaryResistances.Remove(elementalShieldType);
            statusEffects.Remove(StatusEffect.ElementalShield);
        } else if (elementalShieldTurns > 0) {
            statusEffects[StatusEffect.ElementalShield] = elementalShieldTurns.ToString();
        }
        if (burningTurns == 0) {
            isBurning = false;
            burningDamage = 0;
            statusEffects.Remove(StatusEffect.Burning);
        } else if (burningTurns > 0) {
            statusEffects[StatusEffect.Burning] = burningTurns.ToString();
        }
        statusEffectsBar.UpdateBar(statusEffects);
        if (isStunned) {
            isStunned = false;
            return false;
        }
        return true;
    }
}
