using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraData {
        public CameraEntity Entity {
            get;
            private set;
        }
        public Vector3 TargetPoint {
            get;
            private set;
        }
        public Quaternion TargetRotation {
            get;
            private set;
        }

        public void SetEntity(CameraEntity entity) {
            Entity = entity;
        }

        public void SetTargetPoint(Vector3 point) {
            TargetPoint = point;
        }

        public void SetTargetRotation(Quaternion rota) {
            TargetRotation = rota;
        }
    }
}