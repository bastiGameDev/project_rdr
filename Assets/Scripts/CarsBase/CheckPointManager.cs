using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private CheckPoint checkPoint = null;
    private bool canRestart = true;
    private void FixedUpdate() {
        if(Input.GetKey(KeyCode.R) && canRestart) {
            ResetPosition();
            StartCoroutine(RestartDelay());
        }
    }
    public void ResetPosition() {
        if(checkPoint != null) {
            transform.position = checkPoint.GetCheckPointSpawn();
            transform.rotation  = Quaternion.identity;
        }
    }

    private IEnumerator RestartDelay() {
        canRestart = false;
        yield return new WaitForSeconds(10);
        canRestart = true;
    }

    public void UpdateCheckPoint(CheckPoint checkPoint) {
        this.checkPoint = checkPoint;
    }
}
