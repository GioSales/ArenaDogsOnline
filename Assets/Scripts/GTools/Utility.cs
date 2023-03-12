using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTools
{
    public static class Utility
    {
        public static bool IsOnLayerMask(Transform obj, LayerMask layerMask)
        {
            return ((1 << obj.gameObject.layer) & layerMask) != 0;
        }

        public static bool NearlyEqual(float a, float b, float epsilon)
        {
            float absA = Mathf.Abs(a);
            float absB = Mathf.Abs(b);
            float diff = Mathf.Abs(a - b);

            if (a == b)
            {
                return true;
            }
            else if (a == 0 || b == 0 || absA + absB < float.MinValue)
            {
                return diff < (epsilon * float.MinValue);
            }
            else
            {
                return diff / (absA + absB) < epsilon;
            }
        }
    }
}

