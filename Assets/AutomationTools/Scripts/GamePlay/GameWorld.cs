using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public interface IGameWorld {
        void Register();
        void Init();
        void Clear();
    }

    public class GameWorld : IGameWorldManager {
        private List<IGameWorld> managers = new List<IGameWorld>();

        public void Init() {
            InitManager();
        }

        public void Clear() {
            RemoveManager();
        }

        public void InitManager() {
            AddManager<GameWorldLayerManager>();
            AddManager<GameWorldMapManager>();
            AddManager<CharacterManager>();
            AddManager<CameraManager>();
            
            foreach (var manager in managers) {
                manager.Init();
            }
        }

        public void AddManager<T>() where T : IGameWorld, new() {
            IGameWorld manager = new T();
            managers.Add(manager);
            manager.Register();
        }

        public void RemoveManager() {
            foreach (var manager in managers) {
                manager.Clear();
            }
            managers.Clear();
        }
    }
}