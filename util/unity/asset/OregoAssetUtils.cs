using System.Threading.Tasks;
using UnityEngine;

namespace OregoBlink.util.unity.asset
{
    public static class OregoAssetUtils
    {
        internal static async Task<GameObject> LoadAsync(string prefabPath) =>
            await Resources.LoadAsync(prefabPath) as GameObject;

        internal static async Task<GameObject> InstantiateAsync(string prefabPath)
        {
            //Load player:
            var prefab = await LoadAsync(prefabPath);
            //Instantiate player:
            return Object.Instantiate(prefab);
        }
    }
}