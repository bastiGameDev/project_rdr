using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    [SerializeField] private int respect;
    [SerializeField] private int money;

    public void AddValues(int money, int respect, float multiplayer) {
        this.money += Mathf.RoundToInt(money * multiplayer);
        this.respect += Mathf.RoundToInt(respect * multiplayer);
    }

    public bool TryBuy(int cost) {
        if (cost <= money) {
            money -= cost;
            return true;
        }
        return false;
    }

    public static PlayerStats GetStats() {
        string stats = PlayerPrefs.GetString("PlayerStats");
        if (string.IsNullOrEmpty(stats)) return null;
        PlayerStats playerStats = JsonUtility.FromJson<PlayerStats>(stats);
        return playerStats;
    }
    public bool SaveStats() {
        try {
            string json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("PlayerStats", json);
            return true;
        } catch {
            return false;
        }
    }
}
