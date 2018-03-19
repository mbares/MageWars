using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GesturePreview : MonoBehaviour {

    public float gestureShowTime;

    [System.Serializable]
    public struct GesturePreviews {
        public string gestureId;
        public Sprite image;
    }
    public GesturePreviews[] gesturePreviews;

    private Dictionary<string, Sprite> gesturePreviewsDictionary = new Dictionary<string, Sprite>();
    private Character player;

    private void Awake() {
        player = FindObjectOfType<Player>().GetComponent<Character>();
        for (int i = 0; i < gesturePreviews.Length; i++) {
            gesturePreviewsDictionary.Add(gesturePreviews[i].gestureId, gesturePreviews[i].image);
        }
    }

    public void ShowGesturePreview(string gestureId) {
        gameObject.SetActive(true);
        float showTime;
        if (player.IsBlinded()) {
            showTime = 0.1f;
        } else {
            showTime = gestureShowTime;
        }
        StartCoroutine(ShowGesture(gestureId, showTime));
    }


    IEnumerator ShowGesture(string gestureId, float gestureShowTime) {
        GetComponent<Image>().sprite = gesturePreviewsDictionary[gestureId];
        yield return new WaitForSeconds(gestureShowTime);
        gameObject.SetActive(false);
    }
}
