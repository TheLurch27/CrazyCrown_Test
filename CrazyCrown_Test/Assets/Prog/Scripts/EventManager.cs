using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public delegate void OnPlayerEnterEventArea();
    public event OnPlayerEnterEventArea PlayerEnterEventArea;

    public delegate void OnPlayerSaluteEvent();
    public event OnPlayerSaluteEvent PlayerSaluteEvent;

    public void TriggerPlayerEnterEventArea()
    {
        PlayerEnterEventArea?.Invoke();
    }

    public void TriggerPlayerSaluteEvent()
    {
        PlayerSaluteEvent?.Invoke();
    }
}
