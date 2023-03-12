using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GTools.Events
{
    [System.Serializable]
    public class IntEventResponse : UnityEvent<GameObject, int> { }

    public class IntEventListener : ListenerTemplate<int>
    {
        [SerializeField] private IntEvent Event;
        [SerializeField] private IntEventResponse Response;

        public override EventTemplate<int> gameEvent => Event;
        public override UnityEvent<GameObject, int> unityResponse => Response;
    }
}
