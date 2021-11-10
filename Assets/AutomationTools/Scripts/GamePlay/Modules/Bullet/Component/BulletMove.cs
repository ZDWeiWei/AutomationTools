using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class BulletMove : SystemBase.IComponent {
        private BulletSystem system;
        private Vector3 startPoint;
        private Vector3 point;
        private Vector3 forward;
        private Quaternion startRota;
        private float bulletSpeed = 20f;
        private float lifeTime = 2f;
        private bool isClear = false;

        public void Init(SystemBase system) {
            this.system = (BulletSystem) system;
            ATUpdateRegister.AddFixedUpdate(OnFixedUpdate);
            ATUpdateRegister.AddUpdate(OnUpdate);
            system.Register<GameProtoDoc_Bullet.CreateBullet>(BulletSystem.Event_CreateBullet, OnCreateBulletCallBack);
        }

        public void Clear() {
            system.UnRegister<GameProtoDoc_Bullet.CreateBullet>(BulletSystem.Event_CreateBullet, OnCreateBulletCallBack);
            ATUpdateRegister.RemoveFixedUpdate(OnFixedUpdate);
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            this.system = null;
        }
        
        private void OnUpdate(float delta) {
            CheckLifeTime(delta);
            Move(delta);
        }

        private void OnFixedUpdate(float delta) {
          //  Move(delta);
        }

        private void Move(float delta) {
            if (isClear) {
                return;
            }
            point += (forward * bulletSpeed) * delta;
            var dir = (point - startPoint).normalized;
            var rota = Quaternion.LookRotation(dir);
            this.system.Entity.SetPoint(BulletEntity.Root, point);
            this.system.Entity.SetRota(BulletEntity.Root, rota);
        }

        private void CheckLifeTime(float delta) {
            if (lifeTime <= 0) {
                return;
            }
            lifeTime -= delta;
            if (lifeTime <= 0) {
                isClear = true;
                GameProtoManager.Send(new GameProtoDoc_Bullet.RemoveBullet {
                    BulletId = this.system.Data.BulletId
                });
            }
        }

        private void OnCreateBulletCallBack(GameProtoDoc_Bullet.CreateBullet bullet) {
            startPoint = bullet.StartPoint;
            point = startPoint;
            startRota = bullet.StartRota;
            this.system.Entity.SetPoint(BulletEntity.Root, startPoint);
            this.system.Entity.SetRota(BulletEntity.Root, startRota);
            forward = startRota * Vector3.forward;
        } 
    }
}