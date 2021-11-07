using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraEntity : EntityBase {
        public const string Root = "Root";
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Distance = "Distance";
        public const string Camera = "Camera";

        private Camera useCamera;

        public override void OnInit() {
            var system = (CameraSystem) this.system;
            system.Data.SetEntity(this);
            system.CreateEntity(URI.Camera);
            SetCameraDistance(10);
            ATUpdateRegister.AddLateUpdate(OnUpdate);
        }

        public override void OnClear() {
            ATUpdateRegister.RemoveLateUpdate(OnUpdate);
            this.useCamera = null;
        }

        public void SetCameraDistance(float distance) {
            SetLocalPoint(Distance, new Vector3(0, 0, distance * -1f));
        }
    }
}