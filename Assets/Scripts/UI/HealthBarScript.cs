using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public Character character;
    public Image content;
    public Text statusText;

    private float fillAmount;
    private GameManager gameManager;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update () {
        statusText.text = character.GetHealth().ToString();
        fillAmount = (float)character.GetHealth() / character.maxHealth;
        content.fillAmount = fillAmount;
        if (character.GetHealth() <= 0) {
            statusText.text = "DEAD";
            gameManager.HandleCharacterDead(character);
        }
    }
 }
