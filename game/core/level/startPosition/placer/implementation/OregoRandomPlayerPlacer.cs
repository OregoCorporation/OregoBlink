using System.Collections.Generic;
using System.Linq;
using OregoBlink.game.core.level.startPosition.behaviour;
using UnityEngine;

namespace OregoBlink.game.core.level.startPosition.placer.implementation
{
    public class OregoRandomPlayerPlacer : OregoPlayerPlacer
    {
        public override void PlacePlayers(List<GameObject> players)
        {
            //Find start positions:
            var startPositions = this.FindStartPositions();

            //Place players:
            this.PlacePlayers(players, startPositions);
        }

        public override void PlacePlayers(List<GameObject> players, OregoStartPositionBehaviour[] startPositions)
        {
            //Check player count:
            if (players.Count > startPositions.Length)
            {
                return;
            }

            //Map player positions:
            var remainingPlayerTransform = players
                .Select(it => it.transform)
                .ToList();

            //Map start positions:
            var remainingPositions = startPositions
                .Select(it => it.transform.position)
                .ToList();

            //Place player randomly:
            var random = new System.Random();
            while (remainingPlayerTransform.Count > 0)
            {
                //Get random player & position:
                var randomPlayerIndex = random.Next(0, remainingPlayerTransform.Count);
                var randomPositionIndex = random.Next(0, remainingPositions.Count);
                var playerTransform = remainingPlayerTransform[randomPlayerIndex];
                var randomPosition = remainingPositions[randomPositionIndex];

                //Attach player position to start position:
                playerTransform.position = randomPosition;

                //Remove elements from lists:
                remainingPlayerTransform.Remove(playerTransform);
                remainingPositions.Remove(randomPosition);
            }
        }

        /**
         * Find start positions.
         */

        public virtual OregoStartPositionBehaviour[] FindStartPositions() =>
            Object.FindObjectsOfType<OregoStartPositionBehaviour>();
    }
}