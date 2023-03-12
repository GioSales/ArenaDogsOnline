using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTools.Events
{

    [CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Game Object List", order = 1)]
    public class ObjectListEvent : EventTemplate<List<GameObject>>
    {

    }

}
