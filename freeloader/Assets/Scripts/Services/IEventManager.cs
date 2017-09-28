using System;

namespace FreeLoader.Services
{
    public interface IEventManager
    {
        void StartListening(AvailableEvents eventName, global::UnityEngine.Events.UnityAction<object> listener, string objectId = "");
        void StopListening(AvailableEvents eventName, global::UnityEngine.Events.UnityAction<object> listener, string objectId = "");
        void TriggerEvent(AvailableEvents eventName, object dataSentWithEvent, string objectId = "");
    }
}
