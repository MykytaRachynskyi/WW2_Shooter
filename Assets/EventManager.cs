using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatStartEvent : UnityEvent
{ }

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public CombatStartEvent combatStartEvent = new CombatStartEvent();
    public Dictionary<EVENT_TYPES, UnityEvent> events = new Dictionary<EVENT_TYPES, UnityEvent>();

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        events.Add(EVENT_TYPES.COMBAT_START, combatStartEvent);
    }
}

public enum EVENT_TYPES
{
    COMBAT_START
}
