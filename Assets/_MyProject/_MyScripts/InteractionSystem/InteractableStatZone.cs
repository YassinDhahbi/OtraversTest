using UnityEngine;

public abstract class InteractableStatZone : MonoBehaviour
{
    [SerializeField] private string _interactionText = "Interact";
    protected bool _isInteractable;

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
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isInteractable)
        {
            Interact();
        }
    }

}





