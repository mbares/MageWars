using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    private LevelManager levelManager;

    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void LoadLevel(string level) {
        levelManager.LoadLevel(level);
    }

    public void Quit() {
        levelManager.Quit();
    }

    public void NextLevel() {
        levelManager.NextLevel();
    }
}
