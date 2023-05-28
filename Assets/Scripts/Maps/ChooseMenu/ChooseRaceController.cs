using UnityEngine;
using UnityEngine.SceneManagement;

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

   public void StartRace() {
        RaceSettings.car = choosenMachine;
        SceneManager.LoadScene(loadScene);
   }
}
