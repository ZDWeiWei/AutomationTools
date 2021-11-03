using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public interface IGameWorldManager {
        void Init();
        void Clear();
    }

    public class GameWorldManager {
        private List<GameWorld> gameWorlds = new List<GameWorld>();

        public void Init() {
            var gameWorld = new GameWorld();
            gameWorld.Init();
            gameWorlds.Add(gameWorld);
        }

        public void Clear() {
            for (int i = 0; i < gameWorlds.Count; i++) {
                var gameWorld = gameWorlds[i];
                gameWorld.Clear();
            }
        }
    }
}