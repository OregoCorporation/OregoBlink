using OregoBlink.game.core;
using OregoBlink.game.core.context.behaviour;
using UnityEngine;

namespace OregoBlink.util.samples.environment
{
    public sealed class OregoRotationBehaviour : OregoCoreBehaviour
    {
        [SerializeField] private float rotationSpeed;

        public override void HandleUpdate()
        {
            this.transform.Rotate(Vector3.back, this.rotationSpeed);
        }
    }
}