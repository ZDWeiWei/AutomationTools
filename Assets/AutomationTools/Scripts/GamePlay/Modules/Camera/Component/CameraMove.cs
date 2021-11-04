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
            if (data.Entity.IsLoadEntityObj == false) {
                return;
            }
            
            var distance = Vector3.Distance(data.Entity.Point, data.TargetPoint);
            if (distance > 10f) {
                data.Entity.SetPoint(data.TargetPoint);
            } else {
                data.Entity.SetPoint(Vector3.Lerp(data.Entity.Point, data.TargetPoint, speed * delta));
            }
        }
    }
}