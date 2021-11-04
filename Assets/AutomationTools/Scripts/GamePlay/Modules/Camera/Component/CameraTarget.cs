using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraTarget : SystemBase.IComponent {
        private CameraSystem system;
        public void Init(SystemBase system) {
            this.system = (CameraSystem)system;
            GameProtoManager.AddListener(GameProtoDoc_Camera.SetPoint.ID, OnSetPointCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Camera.SetRotation.ID, OnSetRotationCallBack);
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Camera.SetPoint.ID, OnSetPointCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Camera.SetRotation.ID, OnSetRotationCallBack);
            this.system = null;
        }

        private void OnSetPointCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Camera.SetPoint) message;
            this.system.Data.SetTargetPoint(data.point);
        }

        private void OnSetRotationCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Camera.SetRotation) message;
            this.system.Data.SetTargetRotation(data.rota);
        }
    }
}