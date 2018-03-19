using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum StatusEffect {
    Blinded, Burning, Wet
}

public class StatusEffectsBar : MonoBehaviour {

    [System.Serializable]
    public struct Icons {
        public StatusEffect statusEffect;
        public Sprite icon;
    }
    public Icons[] icons;

    private Dictionary<StatusEffect, Sprite> iconsDictionary = new Dictionary<StatusEffect, Sprite>();
    private Character player;

    private void Awake() {
        for (int i = 0; i < icons.Length; i++) {
            iconsDictionary.Add(icons[i].statusEffect, icons[i].icon);
        }
    }

    public void UpdateBar(Dictionary<StatusEffect, string> statusEffects)  {
        for (int i = 0; i < transform.childCount; i++) {
            Transform barNode = transform.GetChild(i);
            if (i < statusEffects.Count) {
                barNode.gameObject.SetActive(true);
                barNode.gameObject.GetComponent<Image>().sprite = iconsDictionary[statusEffects.Keys.ToList()[i]];
                barNode.GetComponentInChildren<Text>().text = statusEffects.Values.ToList()[i];
            } else { 
                barNode.GetComponent<Image>().sprite = null;
                barNode.GetComponentInChildren<Text>().text = null;
                barNode.gameObject.SetActive(false);
            }
        }
    }
}
