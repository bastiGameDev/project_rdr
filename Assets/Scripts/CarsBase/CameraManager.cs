using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject rotationCenter;
    [SerializeField] private bool isLocked;
    [SerializeField] private float sansative;

    private float currentRotationX = 0;
    private float currentRotationY = 0;

    private bool canSwitch = true;

    private void FixedUpdate() {
        if (isLocked) {
            CalculateRotation();
        }

        if(Input.GetKey(KeyCode.F) && canSwitch) {
            LockUnlockCamera();
        }
    }

    private void CalculateRotation() {
        float rotationX = Input.GetAxis("Mouse X") * sansative;
        float rotationY = Input.GetAxis("Mouse Y") * sansative;
        currentRotationY += rotationX;
        currentRotationX -= rotationY;
        float maxXRotation = 25;
        currentRotationX = Mathf.Clamp(currentRotationX, -maxXRotation, maxXRotation);

        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        rotationCenter.transform.rotation = rotation;
    }

    private void LockUnlockCamera() {
        if(isLocked) {
            isLocked = false;
            Cursor.lockState = CursorLockMode.None;
        } else {
            isLocked = true;
            Cursor.lockState = CursorLockMode.Locked;

        }
        StartCoroutine(delaySwitch());
    }
    private IEnumerator delaySwitch() {
        canSwitch = false;
        yield return new WaitForSeconds(1);
        canSwitch = true;
    }
}
