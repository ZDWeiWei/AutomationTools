using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraMove : SystemBase.IComponent {
        private CameraSystem system;
        private float speed = 20f;
        private Vector3 targetPoint = Vector3.zero;

        public void Init(SystemBase system) {
            this.system = (CameraSystem) system;
            ATUpdateRegister.AddLateUpdate(OnUpdate);
            GameProtoManager.AddListener(GameProtoDoc_Camera.SetPoint.ID, OnSetPointCallBack);
        }

        public void Clear() {
            ATUpdateRegister.AddLateUpdate(OnUpdate);
            GameProtoManager.RemoveListener(GameProtoDoc_Camera.SetPoint.ID, OnSetPointCallBack);
            this.system = null;
        }
        
        private void OnSetPointCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Camera.SetPoint) message;
            targetPoint = data.point;
        }

        public void OnUpdate(float delta) {
            var entity = this.system.Entity;
            var point = entity.GetPoint(CameraEntity.Root);
            var distance = Vector3.Distance(point, entity.GetPoint(CameraEntity.Root));
            if (distance > 10f) {
                entity.SetPoint(CameraEntity.Root, targetPoint);
            } else {
                entity.SetPoint(CameraEntity.Root, Vector3.Lerp(point, targetPoint, speed * delta));
            }
        }
    }
}