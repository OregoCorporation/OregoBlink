using System.Collections.Generic;
using OregoBlink.game.core.level.startPosition.behaviour;
using UnityEngine;

namespace OregoBlink.game.core.level.startPosition.placer
{
    public abstract class OregoPlayerPlacer
    {
        public virtual void PlacePlayers(List<GameObject> players)
        {
        }

        public virtual void PlacePlayers(List<GameObject> players, OregoStartPositionBehaviour[] startPositions)
        {
        }
    }
}