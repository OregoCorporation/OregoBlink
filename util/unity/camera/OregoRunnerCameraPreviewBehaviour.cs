using UnityEngine;

namespace OregoBlink.util.unity.camera
{
    public sealed class OregoRunnerCameraPreviewBehaviour : MonoBehaviour
    {
        private void FixedUpdate()
        {
            var position = this.transform.position;
            this.transform.position = new Vector3(
                position.x,
                position.y,
                position.z + 0.3f * Time.fixedDeltaTime
            );
        }
    }
}