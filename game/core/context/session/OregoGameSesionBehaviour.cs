using System.Collections.Generic;
using System.Threading.Tasks;
using OregoBlink.game.core.level;
using UnityEngine;

namespace OregoBlink.game.core.context.session
{
    public abstract class OregoGameSesionBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            this.Players = new HashSet<GameObject>();
        }

        #region Player
        
        /**
         * Property.
         */

        public HashSet<GameObject> Players { get; private set; }

        public uint MaxPlayers { get; set; }

        public uint TeamCount { get; set; }

        /**
         * Util.
         */
        
        public uint NextTeamNumber() => (uint) this.Players.Count % this.TeamCount;

        #endregion

        #region Lifecycle
        
        public abstract Task Load();

        #endregion

        #region Level

        /**
         * Property.
         */

        public string LevelPrefabPath { get; set; }

        public OregoLevelBehaviour CurrentLevel { get; set; }

        /**
         * Util.
         */

        public async Task<GameObject> GetLevelPrefabAsync() =>
            await Resources.LoadAsync(this.LevelPrefabPath) as GameObject;

        public async Task<GameObject> InflateLevelAsync()
        {
            //Load level:
            var prefab = await Resources.LoadAsync(this.LevelPrefabPath);
            var levelObject = Instantiate(prefab) as GameObject;
            return levelObject;
        }

        #endregion
    }
}