using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeelsUpdater;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private List<AxleInfo> axleInfos;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float brakesForce;
    [SerializeField] private float wheelsGroundCollision;

    private void FixedUpdate() {

        Move();

        if (Input.GetKey(KeyCode.Space)) {
            ApplyBrakes();
        } else {
            RemoveBrakes();
        }
    }
    private void Move() {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.isSteering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.isMotor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }
    private void ApplyBrakes() {
        foreach (AxleInfo axleInfo in axleInfos) {
            axleInfo.leftWheel.motorTorque = 0;
            axleInfo.rightWheel.motorTorque = 0;
            axleInfo.leftWheel.brakeTorque = brakesForce;
            axleInfo.rightWheel.brakeTorque = brakesForce;
        }
    }
    private void RemoveBrakes() {
        foreach (AxleInfo axleInfo in axleInfos) {
            axleInfo.leftWheel.brakeTorque = 0;
            axleInfo.rightWheel.brakeTorque = 0;
        }
    }

    [System.Serializable]
    public class AxleInfo {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool isMotor; // is this wheel attached to motor?
        public bool isSteering; // does this wheel apply steer angle?
    }
}
