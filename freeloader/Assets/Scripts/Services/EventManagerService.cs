using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AvailableEvents {
    PLAYER_LOST_HEALTH,
    PLAYER_GAINED_HEALTH,
    PLAYER_DIED
}

public class EventManagerService {

    private Dictionary<string, UnityEvent> _eventDictionary;

    public EventManagerService()
    {
        _eventDictionary = new Dictionary<string, UnityEvent>();
    }

    public void StartListening(string eventName, UnityAction listener, string objectId = "")
    {
        UnityEvent thisEvent = null;
        string eventNameIWithObjectId = objectId + eventName;

        if (_eventDictionary.TryGetValue(eventNameIWithObjectId, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = CreateNewEvent(eventName, listener, thisEvent);
        }
    }

    public void StopListening(string eventName, UnityAction listener, string objectId = "")
    {
        UnityEvent thisEvent = null;
        string eventNameIWithObjectId = objectId + eventName;

        if (_eventDictionary.TryGetValue(eventNameIWithObjectId, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void TriggerEvent(string eventName, string objectId = "", params object[] arguments)
    {
        UnityEvent thisEvent = null;
        string eventNameIWithObjectId = objectId + eventName;

        if (_eventDictionary.TryGetValue(eventNameIWithObjectId, out thisEvent))
        {
            // If event exists, run/invoke it.
            thisEvent.Invoke((object) arguments );
        }
        else
        {
            Debug.Log("No listeners attached to event '" + eventNameIWithObjectId + "'.");
        }
    }

    #region Private methods

    private UnityEvent CreateNewEvent(string eventName, UnityAction listener, UnityEvent thisEvent)
    {
        thisEvent = new UnityEvent();
        thisEvent.AddListener(listener);
        _eventDictionary.Add(eventName, thisEvent);
        return thisEvent;
    }

    #endregion
}
