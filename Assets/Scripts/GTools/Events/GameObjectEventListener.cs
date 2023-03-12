using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GTools.Events
{

    [System.Serializable]
    public class GameObjectResponse : UnityEvent<GameObject, GameObject> { }

    public class GameObjectEventListener : ListenerTemplate<GameObject>
    {
        [SerializeField] private GameObjectEvent Event;
        [SerializeField] private GameObjectResponse Response;

        public override EventTemplate<GameObject> gameEvent => Event;
        public override UnityEvent<GameObject, GameObject> unityResponse => Response;
    }
}