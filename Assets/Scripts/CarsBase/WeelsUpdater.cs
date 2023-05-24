using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeelsUpdater : MonoBehaviour
{
    [SerializeField] List<GameObject> wheels = new List<GameObject>();
    [SerializeField] List<WheelCollider> weelsColliders = new List<WheelCollider>();

    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle;

    private void Update() {
        for(int i = 0; i < wheels.Count; i++) {
            Vector3 wheelTransform;
            Quaternion wheelRotation;
            weelsColliders[i].GetWorldPose(out wheelTransform, out wheelRotation);
            wheels[i].transform.position = wheelTransform;
            wheels[i].transform.rotation = wheelRotation;
        }

        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            foreach (AxleInfo axleInfo in axleInfos) {
                axleInfo.leftWheel.motorTorque = 0;
                axleInfo.rightWheel.motorTorque = 0;
            }
        }

    }
    [System.Serializable]
    public class AxleInfo {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?
    }
}
