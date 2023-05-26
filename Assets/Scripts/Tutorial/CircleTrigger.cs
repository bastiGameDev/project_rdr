using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTrigger : MonoBehaviour
{
    [SerializeField] private TutorialController tutorialController;
    private GameObject playerIn = null;
    private int progeress = 0;

    private void OnTriggerEnter(Collider other) {
        MoveManager moveManager;
        if (other.TryGetComponent<MoveManager>(out moveManager) && other.gameObject != playerIn) {
            tutorialController.ProgressCurrentTask();
            playerIn = other.gameObject;
            progeress++;
            if (progeress > 2) gameObject.SetActive(false);
                else StartCoroutine(DropPlayerIn());
        }
    }
    private IEnumerator DropPlayerIn() {
        yield return new WaitForSeconds(5);
        playerIn = null;
    }
}
