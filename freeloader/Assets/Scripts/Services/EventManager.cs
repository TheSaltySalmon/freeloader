﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AvailableEvents {
    // Player Health
    PLAYER_LOST_HEALTH,
    PLAYER_GAINED_HEALTH,
    PLAYER_DIED,

    // Player Fuel
    PLAYER_GAINED_FUEL,
    PLAYER_LOST_FUEL,
    PLAYER_OUT_OF_FUEL
}

namespace FreeLoader.Services
{
    public class EventManager : IEventManager
    {
        private Dictionary<string, UnityEvent<object>> _eventDictionary;

        public EventManager()
        {
            _eventDictionary = new Dictionary<string, UnityEvent<object>>();
        }

        public void StartListening(AvailableEvents eventName, UnityAction<object> listener, string objectId = "")
        {
            Debug.Log("StartListening");

            UnityEvent<object> thisEvent = null;
            string eventNameIWithObjectId = objectId + eventName;

            if (_eventDictionary.TryGetValue(eventNameIWithObjectId, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = CreateNewEvent(eventNameIWithObjectId, listener, thisEvent);
            }
        }

        public void StopListening(AvailableEvents eventName, UnityAction<object> listener, string objectId = "")
        {
            Debug.Log("StopListening");

            UnityEvent<object> thisEvent = null;
            string eventNameIWithObjectId = objectId + eventName;

            if (_eventDictionary.TryGetValue(eventNameIWithObjectId, out thisEvent))
            {
                try {
                    thisEvent.RemoveListener(listener);
                } catch { }
            }
        }

        public void TriggerEvent(AvailableEvents eventName, object dataSentWithEvent, string objectId = "")
        {
            Debug.Log("TriggerEvent - EventName = " + eventName);

            UnityEvent<object> thisEvent = null;
            string eventNameIWithObjectId = objectId + eventName;

            if (_eventDictionary.TryGetValue(eventNameIWithObjectId, out thisEvent))
            {
                // If event exists, run/invoke it.
                thisEvent.Invoke((object)dataSentWithEvent);
            }
            else
            {
                Debug.Log("No listeners attached to event '" + eventNameIWithObjectId + "'.");
            }
        }

        #region Private methods

        private UnityEvent<object> CreateNewEvent(string eventName, UnityAction<object> listener, UnityEvent<object> thisEvent)
        {
            thisEvent = new MyEvent();
            thisEvent.AddListener(listener);
            _eventDictionary.Add(eventName.ToString(), thisEvent);
            return thisEvent;
        }

        #endregion

        [System.Serializable]
        public class MyEvent : UnityEvent<object>
        {

        }
    }
}

