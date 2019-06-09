using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OregoBlink.util.unity.scene
{
    public static class OregoSceneUtils
    {
        public static void LoadSceneAsync(this MonoBehaviour behaviour, string sceneName) =>
            behaviour.StartCoroutine(LoadSceneCoroutine(sceneName));
        
        public static void LoadSceneAsync(this MonoBehaviour behaviour, int sceneIndex) =>
            behaviour.StartCoroutine(LoadSceneCoroutine(sceneIndex));

        private static IEnumerator LoadSceneCoroutine(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

        private static IEnumerator LoadSceneCoroutine(int sceneIndex)
        {
            yield return SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}