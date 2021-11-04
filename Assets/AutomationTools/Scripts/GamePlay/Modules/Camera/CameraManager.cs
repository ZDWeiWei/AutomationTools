using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraManager : IGameWorld {
        private CameraSystem cameraSystem;

        public void Register() {
        }

        public void Init() {
            AddSystem();
        }

        public void Clear() {
            RemoveSystem();
        }

        private void AddSystem() {
            cameraSystem = new CameraSystem();
            cameraSystem.Init();
        }

        private void RemoveSystem() {
            if (cameraSystem == null) {
                cameraSystem.Clear();
                cameraSystem = null;
            }
        }
    }
}