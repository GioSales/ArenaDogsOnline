using MachinaBlade.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace GTools.Events
{
    public abstract class ListenerTemplate<T> : MonoBehaviour, IEventListener<T>
    {
        public bool acceptCallsFromParent = false;

        [FormerlySerializedAs("needsMatchingGameObject")] [Tooltip("Prevents listener from calling functions if event came from a different Game Object")]
        public bool needMatchingGameObject = false;

        protected GameObject gameObjectToMatch;
        
        // These have to be provided by the inheritor
        public abstract EventTemplate<T> gameEvent { get; }
        public abstract UnityEvent<GameObject, T> unityResponse { get; }

        protected virtual void OnEnable()
        {
            if (gameEvent != null)
                SubscribeEvent();
        }

        protected virtual void OnDisable()
        {
            if (gameEvent != null)
                UnsubscribeEvent();
        }


        public virtual void SubscribeEvent()
        {
            gameEvent.Subscribe(this);
        }
        public virtual void UnsubscribeEvent()
        {
            gameEvent.Unsubscribe(this);
        }

        public void OnEvent(GameObject callerGameObj, T info)
        {
            if (ShouldRespond(callerGameObj, info))
            {
                if (unityResponse != null)
                    unityResponse.Invoke(callerGameObj, info);
                else
                    Debug.Log("No response defined for " + gameObject.name + "'s " + unityResponse);
            }
        }

        protected virtual bool ShouldRespond(GameObject callerGameObj, T info)
        {
            if(needMatchingGameObject)
            {
                if (isChildOfCallerGameObject(callerGameObj))
                    return true;
                if (ReferenceEquals(callerGameObj, GetGameObjectToCompare()))
                    return true;
                else
                    return false;
            }

            return true;
        }

        protected bool isChildOfCallerGameObject(GameObject callerGameObj)
        {
            if (!acceptCallsFromParent)
                return false;

            return transform.IsChildOf(callerGameObj.transform);
        }

        protected virtual GameObject GetGameObjectToCompare()
        {
            return gameObjectToMatch == null ? gameObject : gameObjectToMatch;
        }
    }
}