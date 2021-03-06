﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

    public GameObject pausePanel;

    private bool paused = false;

    void Update() {
        if (paused && !AudioListener.pause) {
            AudioListener.pause = true;
        }
    }

    public void PauseGame() {
        paused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        AudioListener.pause = true;
        AudioListener.volume = 0.0f;
    }

    public void ResumeGame() {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        AudioListener.volume = 1.0f; 
    }

    public void ReturnToMenu() {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        AudioListener.volume = 1.0f;
        FindObjectOfType<LevelManager>().LoadLevel("00a_Start_Menu");
    }
}
