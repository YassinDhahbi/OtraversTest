using UnityEngine;

[System.Serializable]
public class StatDisplayDetails
{
    public Stats Stat;
    public float Max;
    public float Current;
    public bool IsActive;
    public float DedecutionPerSecond;
    public Color RepresentationColor;
    const string _savingPrefix = "StatDisplayDetails_";
    public void Initialize()
    {
        Current = Max;
    }

    public void UpdateTimer()
    {
        if (!IsActive) return;
        Current -= DedecutionPerSecond;
        Current = Mathf.Clamp(Current, 0, Max);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(SaveingCode))
        {
            Current = PlayerPrefs.GetFloat(SaveingCode);
        }
        else
        {
            Current = Max;
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat(SaveingCode, Current);
    }

    string SaveingCode => _savingPrefix + Stat.ToString();

}
