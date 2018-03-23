using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour { 

	const string MASTER_VOLUME_KEY = "master_volume";
    const string PLAYER_LEVEL_KEY = "player_level";
    const string PLAYER_MAX_HEALTH_KEY = "player_max_health";
    const string PLAYER_MAX_MANA_KEY = "player_max_mana";

    public static void SetInitialValues() {
        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY)) {
            SetMasterVolume(0.8f);
        }
        if (!PlayerPrefs.HasKey(PLAYER_LEVEL_KEY)) {
            SetPlayerLevel(1);
        }
        if (!PlayerPrefs.HasKey(PLAYER_MAX_HEALTH_KEY)) {
            SetPlayerMaxHealth(20);
        }
        if (!PlayerPrefs.HasKey(PLAYER_MAX_MANA_KEY)) {
            SetPlayerMaxMana(8);
        }
    }

    public static void SetMasterVolume(float volume) {
		if(volume >= 0f && volume <= 1f)
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		else 
			Debug.LogError("Master volume out of range");
	}

	public static float GetMasterVolume() {
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

    public static void SetPlayerLevel(int level) {
        if (level > 0)
            PlayerPrefs.SetInt(PLAYER_LEVEL_KEY, level);
        else
            Debug.LogError("Level out of range");
    }

    public static int GetPlayerLevel() {
        return PlayerPrefs.GetInt(PLAYER_LEVEL_KEY);
    }

    public static void SetPlayerMaxHealth(int maxHealth) {
        if (maxHealth > 0)
            PlayerPrefs.SetInt(PLAYER_MAX_HEALTH_KEY, maxHealth);
        else
            Debug.LogError("Max health out of range");
    }

    public static int GetPlayerMaxHealth() {
        return PlayerPrefs.GetInt(PLAYER_MAX_HEALTH_KEY);
    }

    public static void SetPlayerMaxMana(int maxMana) {
        if (maxMana > 0)
            PlayerPrefs.SetInt(PLAYER_MAX_MANA_KEY, maxMana);
        else
            Debug.LogError("Max health out of range");
    }

    public static int GetPlayerMaxMana() {
        return PlayerPrefs.GetInt(PLAYER_MAX_MANA_KEY);
    }
}
