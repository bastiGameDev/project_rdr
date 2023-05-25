using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public Vector3 GetCheckPointSpawn() {
        Vector3 spawnPoint = transform.position;
        spawnPoint += offset;
        return spawnPoint;
    }

    private void OnTriggerEnter(Collider other) {
        CheckPointManager manager = null;
        other.TryGetComponent<CheckPointManager>(out manager);
        if (manager != null) {
            manager.UpdateCheckPoint(this);
        }
    }
}
