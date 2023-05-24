using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeelsUpdater;

public class WeelsUpdater : MonoBehaviour
{
    [SerializeField] private List<GameObject> wheels = new List<GameObject>();
    [SerializeField] List<WheelCollider> weelsColliders = new List<WheelCollider>();


    private void Update() {
        for(int i = 0; i < wheels.Count; i++) {
            Vector3 wheelTransform;
            Quaternion wheelRotation;
            weelsColliders[i].GetWorldPose(out wheelTransform, out wheelRotation);
            wheels[i].transform.position = wheelTransform;
            wheels[i].transform.rotation = wheelRotation;
        }
    }
    
}
