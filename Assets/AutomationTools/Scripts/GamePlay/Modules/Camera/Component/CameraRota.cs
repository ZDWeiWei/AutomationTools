using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CameraRota : SystemBase.IComponent {
        private CameraSystem system;
        private float hSpeed = 6f;
        private float vSpeed = 3f;

        public void Init(SystemBase system) {
            this.system = (CameraSystem) system;
            TouchSystem.Register(OnRota);
        }

        public void Clear() {
            TouchSystem.Unregister(OnRota);
            this.system = null;
        }

        private void OnRota(float h, float v) {
            var entity = this.system.Data.Entity;
            var hRota = entity.GetLocalRota(CameraEntity.Horizontal).eulerAngles;
            var vRota = entity.GetLocalRota(CameraEntity.Vertical).eulerAngles;
            hRota.y += h * hSpeed * Time.deltaTime;
            hRota.x = 0f;
            hRota.z = 0f;
            vRota.x += v * vSpeed * Time.deltaTime;
            vRota.y = 0f;
            vRota.z = 0f;
            var HQuaternion = Quaternion.Euler(hRota);
            var VQuaternion = ClampVertical(Quaternion.Euler(vRota));
            entity.SetLocalRota(CameraEntity.Horizontal, HQuaternion);
            entity.SetLocalRota(CameraEntity.Vertical, VQuaternion);
            GameProtoManager.Send(new GameProtoDoc_Camera.SendHVRotation {
                hRota = HQuaternion, vRota = VQuaternion,
            });
        }

        private Quaternion ClampVertical(Quaternion rota) {
            rota.x /= rota.w;
            rota.y /= rota.w;
            rota.z /= rota.w;
            rota.w = 1.0f;
            float angle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rota.x);
            angle = Mathf.Clamp(angle, -90f, 90f);
            rota.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
            return rota;
        }
    }
}