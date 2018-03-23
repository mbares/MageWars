using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;
using UnityEngine.UI;

public class GestureHandler : MonoBehaviour
{

    public GameManager gameManager;
    public Text gestureScoreText;

    public void OnRecognize(RecognitionResult result)
    {
        StopAllCoroutines();
        if (result != RecognitionResult.Empty && result.gesture.id == gameManager.GetSpellToCast().GestureId) {
            GestureScore(result.score.score);
            gameManager.GetSpellToCast().DoSpellEffect(result);
            Debug.Log("Result: " + result.gesture.name + " " + result.score.score * 100 + "%");
        } else {
            GestureScore(0f);
            Debug.Log("Fail"); ;
        }
        gameManager.IncrementSpellsToCastIndex();
    }

    private void GestureScore(float score)
    {
        if (score > 0.95f) {
            gestureScoreText.color = Color.green;
            gestureScoreText.text = "Perfect!";
        } else if (score > 0.85f) {
            gestureScoreText.color = Color.yellow;
            gestureScoreText.text = "Great!";
        } else if (score > 0.75f) {
            gestureScoreText.color = Color.blue;
            gestureScoreText.text = "Good!";
        } else {
            gestureScoreText.color = Color.red;
            gestureScoreText.text = "Misspell!";
        }
        Invoke("ClearGestureScore", 0.5f);
    }

    private void ClearGestureScore()
    {
        gestureScoreText.text = " ";
    }
}
