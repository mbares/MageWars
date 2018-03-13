using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    public Character character;
    public Image content;
    public Text statusText;

    private float fillAmount;

    void Update () 
    {
        statusText.text = character.health.ToString();
        fillAmount = (float)character.health / character.maxHealth;
        content.fillAmount = fillAmount;
        if (character.health <= 0)
        {
            statusText.text = "DEAD";
        }
    }
 }
