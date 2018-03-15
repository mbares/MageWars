using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GesturePreview : MonoBehaviour {

    [System.Serializable]
    public struct GesturePreviews
    {
        public string gestureId;
        public Sprite image;
    }
    public GesturePreviews[] gesturePreviews;

    private Dictionary<string, Sprite> gesturePreviewsDictionary = new Dictionary<string, Sprite>();

    private void Awake()
    {
        for (int i = 0; i < gesturePreviews.Length; i++)
        {
            gesturePreviewsDictionary.Add(gesturePreviews[i].gestureId, gesturePreviews[i].image);
        }
    }

    public void ShowGesturePreview(string gestureId)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowGesture(gestureId));
    }


    IEnumerator ShowGesture(string gestureId)
    {
        GetComponent<Image>().sprite = gesturePreviewsDictionary[gestureId];
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
