using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class barScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image content;
    [SerializeField]
    private Text statusText;

    
	void Start()
    {
        Invoke("Link", 0.1f);
    }
	
	
	void Update () 
    {
        if (player == null)
            Link();
        if (player!=null)
        {
            HealthBar();
        }
	}

    void Link()
    {
        if (this.gameObject.tag == "hb1")
            player = GameObject.FindWithTag("player1");
        else if (this.gameObject.tag == "hb2")
            player = GameObject.FindWithTag("player2");
        else if (this.gameObject.tag == "bossHB")
            player = GameObject.FindWithTag("boss");
        
    }
	
	void HealthBar()
    {
		if (player.tag == "player1" || player.tag == "player2")
            {
                statusText.text = player.GetComponent<Player>().HP.ToString();
                fillAmount = player.GetComponent<Player>().HP / 100;
                content.fillAmount = fillAmount;
                if(player.GetComponent<Player>().isDead)
                {
                    statusText.text = "DEAD";
                }
            }
            else
            {
                statusText.text = player.GetComponent<Boss>().HP.ToString();
                fillAmount = player.GetComponent<Boss>().HP / 100;
                content.fillAmount = fillAmount;
                if (player.GetComponent<Boss>().isDead)
                {
                    statusText.text = "DEAD";
                }
            }
	}
   
 }
