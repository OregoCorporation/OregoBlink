using System;
using UnityEngine;

namespace OregoBlink.util.unity.gameObject
{
    public static class OregoGameObjectUtils
    {
        public static void SetTransform(this GameObject myObject, GameObject original)
        {
            if (original != null)
            {
                var transform = myObject.transform;
                var originalTransform = original.transform;
                transform.position = originalTransform.position;
                transform.rotation = originalTransform.rotation;
            }
        }

        public static void SetPosition(this GameObject myObject, Vector3 position)
        {
            var transform = myObject.transform;
            transform.position = position;
        }

        public static bool Contains<T>(this GameObject myObject) where T : Component =>
            myObject.GetComponent<T>() != null;

        public static bool Contains(this GameObject myObject, Type type) =>
            myObject.GetComponent(type) != null;

        public static T Get<T>(this GameObject gameObject) where T : Component =>
            gameObject.GetComponent<T>();

        public static T Set<T>(this GameObject gameObject) where T : Component =>
            gameObject.AddComponent<T>();

        public static void Set(this GameObject gameObject, Type type) =>
            gameObject.AddComponent(type);
    }
}