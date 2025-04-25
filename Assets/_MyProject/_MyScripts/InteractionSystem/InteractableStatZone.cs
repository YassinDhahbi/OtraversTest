using UnityEngine;

public abstract class InteractableStatZone : MonoBehaviour
{
    [SerializeField] private string _interactionText = "Interact";

    void OnTriggerEnter(Collider other)
    {
        if (PlayerController.Instance.IsConsuming) return;
        EventManager.Instance.OnMessageSent?.Invoke(_interactionText);
    }
    void OnTriggerExit(Collider other)
    {
        EventManager.Instance.OnMessageSent?.Invoke("");
    }
    [SerializeField] protected Stats _targetStat;
    protected abstract void Interact();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerController.Instance.IsConsuming == false)
        {
            Interact();
        }
    }

}





