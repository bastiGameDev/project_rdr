using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text countdownText;

    public IEnumerator StartCountdown(int time) {
        float startTextSize = countdownText.fontSize;
        for(int i = time; i > 0; i--) {
            countdownText.fontSize = startTextSize;
            countdownText.text = i.ToString();
            for(float j = startTextSize; j<startTextSize*2; j++) {
                countdownText.fontSize = j;
                yield return new WaitForSeconds(0.5f / (startTextSize * 2));
            }
        }
        countdownText.fontSize = startTextSize;
        countdownText.text = "";
    }
    public void SetTimerText(float time) {
        float seconds = Mathf.Round(time * 100) / 100;
        timeText.text = seconds.ToString();
    }
}
