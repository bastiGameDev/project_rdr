using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialParking : MonoBehaviour
{
    private bool inParkingZone = false;
    private bool isComplited = false;
    private GameObject player;
    [SerializeField] private TutorialController tutorial;
    private void OnTriggerEnter(Collider other) {
        MoveManager moveManager;
        if (other.TryGetComponent<MoveManager>(out moveManager)) {
            inParkingZone = true;
            player = other.gameObject;
            StartCoroutine(CheckForStop());
        }
    }
    private void OnTriggerExit(Collider other) {
        inParkingZone = false;
    }

    private IEnumerator CheckForStop() {
        yield return new WaitForSeconds(1);
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if(playerRigidbody.velocity.magnitude <=0.01f && isComplited == false) {
            tutorial.ProgressCurrentTask();
            Debug.Log("Check");
            isComplited = true;
        }
        if (inParkingZone) StartCoroutine(CheckForStop());
    }
}
