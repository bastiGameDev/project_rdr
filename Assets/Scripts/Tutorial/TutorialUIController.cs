using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text taskTextElement;

    public void ChangeTaskText(TutorialController.TutorTask task) {
        taskTextElement.text = $"{task.taskDescription}\nПрогресс: {task.progress}/{task.goal}";
    }
    public IEnumerator SlowChangeText(TutorialController.TutorTask task) {
        string newText = $"{task.taskDescription}\nПрогресс: {task.progress}/{task.goal}";
        taskTextElement.color = new Color(0, 1, 0, 1);
        taskTextElement.CrossFadeAlpha(0, 3, true);
        yield return new WaitForSeconds(3);
        taskTextElement.color = Color.white;
        taskTextElement.text = newText;
        taskTextElement.CrossFadeAlpha(1, 3, true);
    }
}
