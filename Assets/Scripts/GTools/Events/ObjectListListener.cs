using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GTools.Events
{

    [System.Serializable]
    public class ObjectListResponse : UnityEvent<GameObject, List<GameObject>> { }

    public class ObjectListListener : ListenerTemplate<List<GameObject>>
    {
        [SerializeField] private ObjectListEvent Event;
        [SerializeField] private ObjectListResponse Response;

        public override EventTemplate<List<GameObject>> gameEvent => Event;
        public override UnityEvent<GameObject, List<GameObject>> unityResponse => Response;
    }
}