using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public List<GameObject> playerGroup = null;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (scene.name == "00a_Start_Menu")
        {
            playerGroup.Clear();
            if (musicPlayer.GetComponent<AudioSource>().clip != musicPlayer.clips[0])
            {
                musicPlayer.SetClip(0);
            }
        }
        else if (scene.name == "01a_Level01")
        {
            musicPlayer.SetClip(1);
        }
        else if (scene.name == "01b_Level02")
        {
            musicPlayer.SetClip(2);
        }
        else if (scene.name == "01c_Level03")
        {
            musicPlayer.SetClip(3);
        }
        else if (scene.name == "01d_Level04")
        {
            musicPlayer.SetClip(4);
        }
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
