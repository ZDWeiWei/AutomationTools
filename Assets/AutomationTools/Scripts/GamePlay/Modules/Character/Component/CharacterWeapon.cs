using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterWeapon : SystemBase.IComponent {
        private CharacterSystem system;
        private Vector3 scale;
        private bool isDown = false;
        private bool isRotaDown = false;
        private float fireDeltaTime = 0.0f;

        public void Init(SystemBase systemBase) {
            system = (CharacterSystem) systemBase;
            // system.Register<bool>(CharacterSystem.Event_Fire, OnFireCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Character.OnFire.ID, OnFireCallBack);
            TouchSystem.Register(OnRota);
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OnFire.ID, OnFireCallBack);
            // system.Register<bool>(CharacterSystem.Event_Jump, OnFireCallBack);
            TouchSystem.Unregister(OnRota);
        }

        private void OnUpdate(float delta) {
            if (isDown == false && isRotaDown == false) {
                return;
            }
            fireDeltaTime -= delta;
            if (fireDeltaTime < 0) {
                Fire();
                fireDeltaTime = 0.5f;
            }
        }

        private void OnRota(float h, float v, bool isDown) {
            this.isRotaDown = isDown;
        }

        // private void OnFireCallBack(bool isDown) {
        //     this.isDown = isDown;
        // }
        
        private void OnFireCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnFire) message;
            this.isDown = data.isDown;
        }

        private void Fire() {
            GameProtoManager.Send(new GameProtoDoc_Bullet.CreateBullet {
                BulletType = 1,
                Sign = "",
                StartPoint = this.system.Entity.GetPoint(CharacterEntity.WP1),
                StartRota = this.system.Entity.GetRota(CharacterEntity.WP1)
            });
            GameProtoManager.Send(new GameProtoDoc_Bullet.CreateBullet {
                BulletType = 1,
                Sign = "",
                StartPoint = this.system.Entity.GetPoint(CharacterEntity.WP2),
                StartRota = this.system.Entity.GetRota(CharacterEntity.WP2)
            });
        }
    }
}