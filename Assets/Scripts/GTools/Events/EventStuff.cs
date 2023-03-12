using UnityEngine;
using UnityEngine.Events;

namespace MachinaBlade.Events
{
    public interface IEventListener<in T>
    {
        void OnEvent(GameObject callerGameObj, T value);
    }


    [System.Serializable]
    public class EventResponse : UnityEvent<GameObject> { }
}