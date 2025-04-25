using TMPro;
using UnityEngine;

public class InteractionSystemMessageDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageTMP;
    [SerializeField] private GameObject _interactionIcon;
    void OnEnable()
    {
        EventManager.Instance.OnMessageSent += DisplayMessage;
    }
    void OnDisable()
    {
        EventManager.Instance.OnMessageSent -= DisplayMessage;
    }

    void Start()
    {
        DisplayMessage("");
    }
    public void DisplayMessage(string message)
    {
        var isMessageEmpty = message == "";
        _messageTMP.gameObject.SetActive(!isMessageEmpty);
        _interactionIcon.SetActive(!isMessageEmpty);
        _messageTMP.text = message;
    }



}