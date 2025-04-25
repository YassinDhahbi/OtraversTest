using UnityEngine;

public abstract class InteractableStatZone : MonoBehaviour
{
    [SerializeField] private string _interactionText = "Interact";
    protected bool _isInteractable;
    [SerializeField] private float _energyGain;

    void OnTriggerEnter(Collider other)
    {
        EventManager.Instance.OnMessageSent?.Invoke(_interactionText);
        _isInteractable = true;

    }
    void OnTriggerExit(Collider other)
    {
        EventManager.Instance.OnMessageSent?.Invoke("");
        _isInteractable = false;
    }
    [SerializeField] protected Stats _targetStat;
    protected virtual void Interact()
    {
        StatsManager.Instance.ResetStat(_targetStat);
        StatsManager.Instance.AddToStat(Stats.Energy, _energyGain);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isInteractable)
        {
            Interact();
        }
    }

}





