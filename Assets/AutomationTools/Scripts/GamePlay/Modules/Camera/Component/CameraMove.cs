using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraMove : SystemBase.IComponent {
        private CameraSystem system;
        private float speed = 10f;

        public void Init(SystemBase system) {
            this.system = (CameraSystem) system;
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            this.system = null;
        }

        public void OnUpdate(float delta) {
            var data = this.system.Data;
            var point = data.Entity.GetPoint(CameraEntity.Root);
            var distance = Vector3.Distance(point, data.TargetPoint);
            if (distance > 10f) {
                data.Entity.SetPoint(CameraEntity.Root, data.TargetPoint);
            } else {
                data.Entity.SetPoint(CameraEntity.Root, Vector3.Lerp(point, data.TargetPoint, speed * delta));
            }
        }
    }
}