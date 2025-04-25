using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsManager : Singelton<StatsManager>
{
    [SerializeField] private List<StatDisplayDetails> _listOfStatsDetails;
    List<StatDisplayer> _listOfDisplayers = new List<StatDisplayer>();
    [SerializeField] private StatDisplayer _displayerPrefab;
    [SerializeField] private Transform _displayerParent;
    [Header("Death Settings")]
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private TextMeshProUGUI _survivalTimeTMP;
    [SerializeField] private TextMeshProUGUI _deathCauseTMP;
    [SerializeField] private Button _restartButton;
    private bool _isReloading;
    void SpawnDisplayer(StatDisplayDetails statDisplayDetails)
    {
        var displayer = Instantiate(_displayerPrefab, _displayerParent);
        displayer.DisplayStat(statDisplayDetails);
        _listOfDisplayers.Add(displayer);
    }
    void OnEnable()
    {
        _restartButton.onClick.AddListener(ReloadBehaviour);
        _deathScreen.SetActive(false);

    }

    private void ReloadBehaviour()
    {
        Debug.Log("Reloading");
        SceneManager.LoadScene(0);
        _isReloading = true;
        foreach (var statToStop in _listOfStatsDetails)
        {
            StopStatSimulation(statToStop.Stat);
            PlayerPrefs.DeleteKey(statToStop.SaveingCode);
        }
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveAllListeners();
        if (_isReloading) return;
        foreach (var statData in _listOfStatsDetails)
        {
            statData.SaveData();
        }

    }
    void Start()
    {
        InitializeStatDisplayers();
        InvokeRepeating(nameof(UpdateStatValues), 0, 1);
    }

    void InitializeStatDisplayers()
    {
        foreach (var stat in _listOfStatsDetails)
        {
            SpawnDisplayer(stat);
        }
    }



    void UpdateStatValues()
    {
        foreach (var displayer in _listOfDisplayers)
        {
            displayer.SyncCurrentValue();
        }
        HanldeDeath();
    }

    public void ResetStat(Stats statToReset)
    {
        var targetStat = _listOfStatsDetails.Find(x => x.Stat == statToReset);
        targetStat.IsActive = true;
        targetStat.Initialize();
    }
    public void StopStatSimulation(Stats statToReset)
    {
        var targetStat = _listOfStatsDetails.Find(x => x.Stat == statToReset);
        targetStat.IsActive = false;
    }




    internal void AddToStat(Stats targetStat, float energyGain)
    {
        var target = _listOfStatsDetails.Find(x => x.Stat == targetStat);
        target.Current += energyGain;
    }

    void HanldeDeath()
    {
        foreach (var stat in _listOfStatsDetails)
        {
            if (stat.Current == 0)
            {
                _deathScreen.SetActive(true);
                _survivalTimeTMP.text = TimeSpan.FromSeconds(Time.time).ToString(@"hh\:mm\:ss");
                _deathCauseTMP.text = stat.Stat.ToString() + " Depleted";
                // gameObject.SetActive(false);
                CancelInvoke();

            }
        }

    }
}



public enum Stats
{
    Energy,
    Sleep,
    Hunger,
    Thirst
}
