using System;
using System.Threading.Tasks;
using UnityEngine;

namespace OregoBlink.util.unity.coroutine
{
    public static class OregoCoroutineUtils
    {
        public static Coroutine StartCoroutine(this MonoBehaviour behaviour, Task task)
        {
            var coroutine = task.AsIEnumerator();
            return behaviour.StartCoroutine(coroutine);
        }

        public static Coroutine StartCoroutine(this MonoBehaviour behaviour, Func<Task> function)
        {
            var task = function.Invoke();
            var coroutine = task.AsIEnumerator();
            return behaviour.StartCoroutine(coroutine);
        }        
    }
}