using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;

public class GestureHandler : MonoBehaviour {

    public GameManager gameManager;

    public void OnRecognize(RecognitionResult result)
    {
        StopAllCoroutines();
        if (result != RecognitionResult.Empty && result.gesture.id == gameManager.GetSpellToCastGestureId())
        {
            Debug.Log("Result: " + result.gesture.name + " " + result.score.score * 100 + "%");
        }
        else
        {
            Debug.Log("Fail"); ;
        }
        gameManager.IncrementSpellsToCastIndex();
    }
}
