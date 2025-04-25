using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameTMP;
    [SerializeField] private TextMeshProUGUI _statValueTMP;
    [SerializeField] private Image _fillableBar;
    private StatDisplayDetails _statDisplayDetails;

    public void DisplayStat(StatDisplayDetails statDisplayDetails)
    {
        statDisplayDetails.Initialize();
        _statNameTMP.text = statDisplayDetails.Stat.ToString();
        _statDisplayDetails = statDisplayDetails;
        _statValueTMP.text = $"{statDisplayDetails.Current}/{statDisplayDetails.Max}";
        _fillableBar.fillAmount = statDisplayDetails.Current / statDisplayDetails.Max;
        _fillableBar.color = statDisplayDetails.RepresentationColor;
        _statNameTMP.color = statDisplayDetails.RepresentationColor;

    }
    public void SyncCurrentValue()
    {
        _statDisplayDetails.UpdateTimer();
        var percentage = CurrentValue / Max;
        _fillableBar.fillAmount = percentage;
        _statValueTMP.text = $"{CurrentValue}/{Max}";
    }

    float Max => _statDisplayDetails.Max;
    float CurrentValue => _statDisplayDetails.Current;
}
