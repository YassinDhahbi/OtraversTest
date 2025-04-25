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



}
