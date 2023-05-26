using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<TutorTask> tasks;
    [SerializeField] private int currentTask = 0;
    private TutorialUIController UIController;

    private void Start() {
        UIController = GetComponent<TutorialUIController>();
        UIController.ChangeTaskText(tasks[currentTask]);
    }

    public void ProgressCurrentTask() {
        tasks[currentTask].progress++;
        UIController.ChangeTaskText(tasks[currentTask]);
        if (tasks[currentTask].progress == tasks[currentTask].goal) { 
            currentTask++;
            if(currentTask < tasks.Count)
                StartCoroutine(UIController.SlowChangeText(tasks[currentTask]));
        }

    }
    private void FixedUpdate() {
        if(currentTask == 1 && Input.GetKey(KeyCode.R)) {
            ProgressCurrentTask();
        }
    }


    [System.Serializable]
    public class TutorTask {
        public enum TaskType {
            None,
            Collide,
            Leave
        }

        public string taskName;
        public string taskDescription;
        public int goal;
        public int progress;
        public TaskType type;

    }
}
