using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.GamePlay;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class WeaponFire : SystemBase.IComponent {
        private WeaponSystem system;
        private float fireDeltaTime = 0.5f;
        private float startFireTime = 0.0f;
        private bool isFire = false;
        
        public void Init(SystemBase systemBase) {
            this.system = (WeaponSystem)systemBase;
            this.system.Register<bool>(WeaponSystem.Event_Fire, OnFireCallBack);
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            this.system = null;
            this.system.UnRegister<bool>(WeaponSystem.Event_Fire, OnFireCallBack);
        }

        public void OnUpdate(float delta) {
            if (isFire == false) {
                return;
            }
            if (Time.time - startFireTime > fireDeltaTime) {
                Fire();
                startFireTime = Time.time;
            }
        }
        
        public void OnFireCallBack(bool isFire) {
            this.isFire = isFire;
        }

        private void Fire() {
            GameProtoManager.Send(new GameProtoDoc_Bullet.CreateBullet {
                BulletType = 1,
                Sign = "",
                StartPoint = this.system.Entity.GetPoint(CharacterEntity.Root),
                StartRota = this.system.Entity.GetRota(CharacterEntity.Root)
            });
        }
    }
}
