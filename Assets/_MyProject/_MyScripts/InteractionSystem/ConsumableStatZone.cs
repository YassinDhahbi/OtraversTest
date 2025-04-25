using System;
using DG.Tweening;
using UnityEngine;

public class ConsumableStatZone : InteractableStatZone
{
    [SerializeField] private GameObject _consumableObject;
    [SerializeField] private ParticleSystem _consumableParticle;
    [SerializeField] private AudioSource _consumableSound;
    [SerializeField] private float _consumptionTime;
    private const float TRAVEL_TIME = 0.2f;
    private Vector3 _initialConsumableObjectPosition;
    void Start()
    {
        _initialConsumableObjectPosition = _consumableObject.transform.position;
    }
    protected override void Interact()
    {
        var target = PlayerController.Instance.ConsumingPoint;
        _consumableObject.transform.DOMove(target.position, TRAVEL_TIME).OnComplete(() => ConsumingBehaviour(target));
        EventManager.Instance.OnMessageSent?.Invoke("");

    }

    private void ConsumingBehaviour(Transform targetTransform)
    {
        _consumableParticle?.Play();
        _consumableObject.transform.parent = targetTransform;
        DOVirtual.DelayedCall(_consumptionTime, StopConsumption);
        _consumableObject.transform.DOScale(0, _consumptionTime);
        _consumableSound?.Play();

    }

    void StopConsumption()
    {
        _consumableObject.transform.parent = transform;
        _consumableObject.transform.position = _initialConsumableObjectPosition;
        _consumableParticle?.Stop();
        PlayerController.Instance.IsConsuming = false;
        _consumableObject.transform.DOScale(2, 0.5f).SetEase(Ease.InOutBack);
        _consumableSound?.Stop();
    }
}





