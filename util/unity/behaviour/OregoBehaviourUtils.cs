using UnityEngine;

namespace OregoBlink.util.unity.behaviour
{
    public static class OregoBehaviourUtils
    {
        public static T Get<T>(this MonoBehaviour behaviour) where T : Component =>
            behaviour.GetComponent<T>();
    }
}