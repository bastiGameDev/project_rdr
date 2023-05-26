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
        foreach(TimeMultiplayer multiplayer in rewardMultiplayers) {
            if(time <= multiplayer.time) {
                PlayerStats playerStats = PlayerStats.GetStats();
                playerStats.AddValues(moneyReward, respectReward, multiplayer.multiplayer);
                playerStats.SaveStats();
            }
        }
        SceneManager.LoadScene(stageID);
    }

    [System.Serializable]
    public class TimeMultiplayer {
        public float time;
        public float multiplayer;
    }
}
