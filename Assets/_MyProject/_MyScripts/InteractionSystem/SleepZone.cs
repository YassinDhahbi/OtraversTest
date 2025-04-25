using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SleepZone : InteractableStatZone
{
    [SerializeField] private GameObject _sleepingPlayerCopy;
    Vector3 _sleepingPosition;
    Quaternion _sleepingRotation;
    [SerializeField] private AudioSource _sleepingSound;
    [SerializeField] private float _sleepingDuration = 5f;
    [SerializeField] private Image _sleepingVignetteOverlay;
    void Start()
    {
        _sleepingPosition = _sleepingPlayerCopy.transform.position;
        _sleepingRotation = _sleepingPlayerCopy.transform.rotation;
        _sleepingPlayerCopy.SetActive(false);
        _sleepingVignetteOverlay.DOFade(0f, 0f);
    }
    protected override void Interact()
    {
        // base.Interact();
        StartCoroutine(SleepingBehaviour());

    }

    IEnumerator SleepingBehaviour()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        StatsManager.Instance.StopStatSimulation(Stats.Sleep);
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
        _sleepingVignetteOverlay.DOFade(0f, 0.5f);
    }

    private void AnimateBodySleepBehaviour()
    {
        _sleepingVignetteOverlay.DOFade(0.8f, 1f);
        _sleepingPlayerCopy.SetActive(true);
        _sleepingPlayerCopy.transform.position = PlayerController.Instance.transform.position;
        _sleepingPlayerCopy.transform.rotation = PlayerController.Instance.transform.rotation;
        _sleepingPlayerCopy.transform.DOMove(_sleepingPosition, 0.5f);
        _sleepingPlayerCopy.transform.DORotateQuaternion(_sleepingRotation, 0.5f);
    }

    // Make a vignette fade in and out function

    void FadeVignetteToAlpha(float alpha, float duration)
    {

    }

}





