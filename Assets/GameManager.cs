using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int energy = 5000;
    public int crystalShards = 0;
    public float MCRT = 0;

    // Upgrade values
    public int wingStrength = 0;
    public int cardio = 0;
    public float magicPower = 0f;
    public int cuteness = 0;

    // UI Elements
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI crystalShardsText;
    public TextMeshProUGUI MCRTText;

    // Upgrade buttons
    public Button wingStrengthButton;
    public Button cardioButton;
    public Button magicPowerButton;
    public Button cutenessButton;

    // Regen settings
    private int regenRate = 55;
    private int regenInterval = 900; // 15 minutes in seconds


    private void Start()
    {
        wingStrengthButton.onClick.AddListener(UpgradeWingStrength);
        cardioButton.onClick.AddListener(UpgradeCardio);
        magicPowerButton.onClick.AddListener(UpgradeMagicPower);
        cutenessButton.onClick.AddListener(UpgradeCuteness);

        Instance = this;

        UpdateUI();
        StartCoroutine(RegenerateEnergy());

        LoadPlayerData();
    }


    void UpdateUI()
    {
        energyText.text = "Energy: " + energy;
        crystalShardsText.text = "Crystal Shards: " + crystalShards;
        MCRTText.text = "MCRT: " + MCRT;
    }
    IEnumerator RegenerateEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenInterval - cardio * 60);
            energy = Mathf.Min(energy + regenRate, 5000);
            UpdateUI();
        }
    }

    public void OnClick()
    {
        if (energy > 0)
        {
            energy--;
            int shardsEarned = 1 + wingStrength / 50;

            if (UnityEngine.Random.value < magicPower)
            {
                shardsEarned += 5;
            }

            crystalShards += shardsEarned;
            UpdateUI();
        }
    }
    public void BuyMCRT()
    {
        if (crystalShards >= 5000)
        {
            crystalShards -= 5000;
            MCRT += 150;
            UpdateUI();
        }
    }

    public void UpgradeWingStrength()
    {
        int cost = 50 - cuteness;
        if (crystalShards >= cost)
        {
            crystalShards -= cost;
            wingStrength++;
            UpdateUI();
        }
    }

    public void UpgradeCardio()
    {
        int cost = 50 - cuteness;
        if (crystalShards >= cost)
        {
            crystalShards -= cost;
            cardio++;
            UpdateUI();
        }
    }

    public void UpgradeMagicPower()
    {
        int cost = 50 - cuteness;
        if (crystalShards >= cost)
        {
            crystalShards -= cost;
            magicPower += 0.1f;
            UpdateUI();
        }
    }

    public void UpgradeCuteness()
    {
        int cost = 50;
        if (crystalShards >= cost && cuteness < 10)
        {
            crystalShards -= cost;
            cuteness++;
            UpdateUI();
        }
    }
    void LoadPlayerData()
    {
        energy = PlayerPrefs.GetInt("Energy", 5000);
        crystalShards = PlayerPrefs.GetInt("CrystalShards", 0);
        MCRT = PlayerPrefs.GetFloat("MCRT", 0);

        wingStrength = PlayerPrefs.GetInt("WingStrength", 0);
        cardio = PlayerPrefs.GetInt("Cardio", 0);
        magicPower = PlayerPrefs.GetFloat("MagicPower", 0f);
        cuteness = PlayerPrefs.GetInt("Cuteness", 0);

        // Calculate offline energy regeneration
        string lastSaveTimeStr = PlayerPrefs.GetString("LastSaveTime", null);
        if (!string.IsNullOrEmpty(lastSaveTimeStr))
        {
            DateTime lastSaveTime = DateTime.Parse(lastSaveTimeStr);
            TimeSpan timeElapsed = DateTime.Now - lastSaveTime;
            int minutesElapsed = (int)timeElapsed.TotalMinutes;
            int clicksRegened = minutesElapsed / 15 * (regenRate + cardio);
            energy = Mathf.Min(energy + clicksRegened, 5000);
        }
    }
}
