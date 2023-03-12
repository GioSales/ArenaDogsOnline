using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GTools.Events
{

    public class EventTemplate<T> : ScriptableObject
    {
        [SerializeField]
        protected bool debugPrints;
        private List<ListenerTemplate<T>> _eventListeners = new List<ListenerTemplate<T>>();

        virtual public void Call(GameObject callerGameObj, T info)
        {
            DebugPrints();

            for (int i = _eventListeners.Count - 1; i >= 0; i--)
                _eventListeners[i].OnEvent(callerGameObj, info);
        }

        public static void Call(string eventName, GameObject callerGameObj, T info)
        {
            EventTemplate<T> gameEvent = Search(eventName);

            if (gameEvent) gameEvent.Call(callerGameObj, info);
        }

        public void Subscribe(ListenerTemplate<T> listener)
        {
            if(debugPrints)
                Debug.Log("subscribed listener " + listener.name + " to event: " + this.name);

            _eventListeners.Add(listener);
        }

        public void Unsubscribe(ListenerTemplate<T> listener)
        {
            if(debugPrints)
                Debug.Log("UNSUBSCRIBED listener: " + listener.name + " from event: " + this.name, this);

            _eventListeners.Remove(listener);
        }

        private void DebugPrints()
        {
            if (debugPrints)
                Debug.Log("Event Call: " + this.name + " with " + _eventListeners.Count + " listeners.", this);
        }

        protected static EventTemplate<T> Search(string eventName)
        {
            List<EventTemplate<T>> AllEvents = Resources.LoadAll("Events", typeof(EventTemplate<T>)).Cast<EventTemplate<T>>().ToList();

            EventTemplate<T> gameEvent = AllEvents.Find(x => x.name.Contains(eventName));


            if (gameEvent != null)
                return gameEvent;
            else
            {
                Debug.Log("GameEvent " + eventName + " of type " + typeof(T) + " not found on events folder.");
                return gameEvent;
            }
        }

        public void EnableDebugPrints()
        {
            debugPrints = true;
        }

        public void DisableDebugPrints()
        {
            debugPrints = false;
        }

        public static EventTemplate<T> GetEvent(string eventName)
        {
            return Search(eventName);
        }
    }
}