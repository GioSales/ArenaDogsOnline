using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTools.Events
{

    [CreateAssetMenu(fileName = "GameObjectEvent", menuName = "Events/Send Game Object", order = 1)]
    public class GameObjectEvent : EventTemplate<GameObject>
    {

    }

}