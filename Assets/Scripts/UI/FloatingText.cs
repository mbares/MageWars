using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Animator animator;

    private void OnEnable() {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetText(string text, Color color) {
        Text damageText = animator.GetComponent<Text>();
        damageText.color = color;
        damageText.text = text;
    }
}
