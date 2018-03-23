using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour { 
    public static LevelManager instance = null;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (scene.name == "00a_Start_Menu") {
            if (musicPlayer.GetComponent<AudioSource>().clip != musicPlayer.clips[0]) {
                musicPlayer.SetClip(0);
            }
            PlayerPrefsManager.SetInitialValues();
        } else if (scene.name == "00b_Spell_Select") {
            Player.spellsPrepared.Clear();
        } else if (scene.name == "01a_Dungeon01") {
            musicPlayer.SetClip(1);
        }

    }

    public void LoadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
