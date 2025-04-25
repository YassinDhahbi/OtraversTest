using System.Collections.Generic;
using UnityEngine;

public class StatsManager : Singelton<StatsManager>
{
    [SerializeField] private List<StatDisplayDetails> _listOfStatsDetails;
    List<StatDisplayer> _listOfDisplayers = new List<StatDisplayer>();
    [SerializeField] private StatDisplayer _displayerPrefab;
    [SerializeField] private Transform _displayerParent;
    [SerializeField] private float _currentTimer;


    void SpawnDisplayer(StatDisplayDetails statDisplayDetails)
    {
        var displayer = Instantiate(_displayerPrefab, _displayerParent);
        displayer.DisplayStat(statDisplayDetails);
        _listOfDisplayers.Add(displayer);
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


    void OnDisable()
    {
        foreach (var statData in _listOfStatsDetails)
        {
            statData.SaveData();
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
