using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    public GameObject entity;
    public Image content;
    public Text statusText;

    private float fillAmount;
    private Character character;

    private void Start()
    {
        character = entity.GetComponent<Character>();
    }

    void Update () 
    {
        if (character != null)
        {
            HealthBar();
        }
	}

	
	void HealthBar()
    {
        statusText.text = character.health.ToString();
        fillAmount = character.health / character.maxHealth;
        content.fillAmount = fillAmount;
        if(character.health <= 0)
        {
            statusText.text = "DEAD";
        }
	}
 }
