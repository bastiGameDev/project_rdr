using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRaceController : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private int loadScene;
    [SerializeField] private GameObject choosenMachine;

    public void MakeTransitionTo(int index) {
        cameraAnimator.SetInteger("transitionPoint", index);
    }

    public void SetLoadScene(int id) {
        loadScene = id;
    }
    public void SetMachine(GameObject machine) { 
        choosenMachine = machine;
    }
}
