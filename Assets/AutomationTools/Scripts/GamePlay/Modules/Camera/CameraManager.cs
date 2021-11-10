using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraManager : ManagerBase {
        private CameraSystem cameraSystem;
        
        override protected void OnInit() {
            cameraSystem = AddSystem<CameraSystem>();
        }

        override protected void OnClear() {
            cameraSystem.Clear();
            cameraSystem = null;
        }
    }
}