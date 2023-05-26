using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    [SerializeField] private RaceManager manager;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other) {
        MoveManager moveManager;
        if(other.TryGetComponent(out moveManager) && !isTriggered) {
            isTriggered = true;
            manager.FinishRace();
        }
    }
}
