using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
    public Text volumeValue;

    private LevelManager levelManager;
	private MusicPlayer musicPlayer;
	// Use this for initialization
	void Start () 
	{
        levelManager = FindObjectOfType<LevelManager>();
		musicPlayer = FindObjectOfType<MusicPlayer>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
	}

    // Update is called once per frame
    void Update()
    {
        musicPlayer.SetVolume(volumeSlider.value);
        volumeValue.text = (volumeSlider.value * 100).ToString("F2") + "%";
    }

    public void SaveAndExit()
	{
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		levelManager.LoadLevel("00a_Start_Menu");
	}
	
	public void SetDefaults()
	{
		volumeSlider.value = 0.8f;
	}
}
