using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    public FloatingText floatingText;

    private GameObject canvas;

    public void Start() {
        canvas = FindObjectOfType<Canvas>().gameObject;
    }

	public void CreateFloatingText(string text, Transform location, Color textColor) {
        FloatingText instance = Instantiate(floatingText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-0.5f, 0.5f), location.position.y + 1.5f + Random.Range(-0.5f, 0.5f)));
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text, textColor);
    }
}
