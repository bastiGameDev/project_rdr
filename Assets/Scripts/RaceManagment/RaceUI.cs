using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceUI : MonoBehaviour {
    [Header("Main Layout")]
    [SerializeField] private GameObject mainLayout;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text countdownText;
    [Header("Finish Layout")]
    [SerializeField] private GameObject finishLayout;
    [SerializeField] private TMP_Text raceNameText;
    [SerializeField] private TMP_Text finishTimeText;
    [SerializeField] private TMP_Text earningsText;

    public IEnumerator StartCountdown(int time) {
        float startTextSize = countdownText.fontSize;
        for(int i = time; i > 0; i--) {
            countdownText.fontSize = startTextSize;
            countdownText.text = i.ToString();
            for(float j = startTextSize; j<startTextSize*2; j++) {
                countdownText.fontSize = j;
                yield return new WaitForSeconds(0.25f / (startTextSize * 2));
            }
        }
        countdownText.fontSize = startTextSize;
        countdownText.text = "";
    }
    public void SetTimerText(float time) {
        float seconds = Mathf.Round(time * 100) / 100;
        timeText.text = seconds.ToString();
    }
    public void OpenFinishMenu(string name, string rewards ) {
        mainLayout.SetActive(false);
        finishLayout.SetActive(true);
        raceNameText.text = name;
        finishTimeText.text = timeText.text;
        earningsText.text = rewards;
    }
}
