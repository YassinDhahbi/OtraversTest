using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SleepZone : InteractableStatZone
{
    [SerializeField] private GameObject _sleepingPlayerCopy;
    Vector3 _sleepingPosition;
    Quaternion _sleepingRotation;
    [SerializeField] private AudioSource _sleepingSound;
    [SerializeField] private float _sleepingDuration = 5f;
    void Start()
    {
        _sleepingPosition = _sleepingPlayerCopy.transform.position;
        _sleepingRotation = _sleepingPlayerCopy.transform.rotation;
        _sleepingPlayerCopy.SetActive(false);
    }
    protected override void Interact()
    {
        // base.Interact();
        StartCoroutine(SleepingBehaviour());

    }

    IEnumerator SleepingBehaviour()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        AnimateBodySleepBehaviour();
        _sleepingSound.Play();
        yield return new WaitForSeconds(5f);
        AnimateBodyWakeUp();
        yield return new WaitForSeconds(0.5f);
        _sleepingPlayerCopy.SetActive(false);
        base.Interact();
        PlayerController.Instance.gameObject.SetActive(true);
    }

    private void AnimateBodyWakeUp()
    {
        _sleepingSound.Stop();
        _sleepingPlayerCopy.transform.DOMove(PlayerController.Instance.transform.position, 0.5f);
        _sleepingPlayerCopy.transform.DORotateQuaternion(PlayerController.Instance.transform.rotation, 0.5f);

    }

    private void AnimateBodySleepBehaviour()
    {
        _sleepingPlayerCopy.SetActive(true);
        _sleepingPlayerCopy.transform.position = PlayerController.Instance.transform.position;
        _sleepingPlayerCopy.transform.rotation = PlayerController.Instance.transform.rotation;
        _sleepingPlayerCopy.transform.DOMove(_sleepingPosition, 0.5f);
        _sleepingPlayerCopy.transform.DORotateQuaternion(_sleepingRotation, 0.5f);
    }
}





