using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public GameObject spawnCar;
    private GameObject car;
    private MoveManager carManager;
    private RaceUI raceUI;
    [SerializeField] private int respectReward = 0;
    [SerializeField] private int moneyReward = 0;
    [SerializeField] private int stageID = 0;
    [SerializeField] private List<TimeMultiplayer> rewardMultiplayers;
    [SerializeField] private int startTime;
    private float time = 0;
    private bool counting = false;
    void Start()
    {
        if(RaceSettings.car != null)
            spawnCar = RaceSettings.car;
        car = Instantiate(spawnCar, transform.position, Quaternion.identity);
        raceUI = GetComponent<RaceUI>();
        carManager = car.GetComponent<MoveManager>();
        StartCoroutine(StartRace());
    }

    private void FixedUpdate() {
        if (counting) {
            time += Time.fixedDeltaTime;
            raceUI.SetTimerText(time);
        }
    }

    public IEnumerator StartRace() {
        carManager.SetIsLoked(true);
        StartCoroutine(raceUI.StartCountdown(startTime));
        yield return new WaitForSeconds(startTime*3);
        carManager.SetIsLoked(false);
        counting = true;
    }
    
    public void StopRace() {
        counting = false;
    }
    public void FinishRace() {
        StopRace();
        float currentMultiplayer = 1;
        foreach(TimeMultiplayer multiplayer in rewardMultiplayers) {
            if(time <= multiplayer.time) {
                PlayerStats playerStats = PlayerStats.GetStats();
                currentMultiplayer = multiplayer.multiplayer;
                if(playerStats != null) {
                    playerStats.AddValues(moneyReward, respectReward, multiplayer.multiplayer);
                    playerStats.SaveStats();
                }
                break;
            }
        }
        carManager.SetIsLoked(true);
        string rewardsText = $"Награды:\nДеньги: {moneyReward * currentMultiplayer}\nРепутация: {respectReward * currentMultiplayer}";
        raceUI.OpenFinishMenu(RaceSettings.raceName, rewardsText);
    }
    public void OpenGarage() {
        SceneManager.LoadScene(stageID);
    }

    [System.Serializable]
    public class TimeMultiplayer {
        public float time;
        public float multiplayer;
    }
}
