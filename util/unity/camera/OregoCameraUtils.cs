using UnityEngine;

namespace OregoBlink.util.unity.camera
{
    public static class OregoCameraUtils
    {
        public static void EnableMainCamera(bool isEnable)
        {
            var mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(isEnable);
            }
        }        
    }
}