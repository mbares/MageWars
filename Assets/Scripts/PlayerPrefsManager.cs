using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour 
{

	const string MASTER_VOLUME_KEY = "master_volume";

    private void Start()
    {
        SetMasterVolume(80f);
    }

    public static void SetMasterVolume(float volume)
	{
		if(volume >= 0f && volume <= 1f)
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		else 
			Debug.LogError("Master volume out of range");
	}
	public static float GetMasterVolume()
	{
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
}
